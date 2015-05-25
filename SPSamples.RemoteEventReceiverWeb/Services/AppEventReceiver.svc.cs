using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;
using System.ServiceModel;
using System.Net.Http;
using System.Web.Configuration;
using SPSamples.RemoteEventReceiverWeb.Models;

namespace SPSamples.RemoteEventReceiverWeb.Services
{
    public class AppEventReceiver : IRemoteEventService
    {
        /// <summary>
        /// Handles app events that occur after the app is installed or upgraded, or when app is being uninstalled.
        /// </summary>
        /// <param name="properties">Holds information about the app event.</param>
        /// <returns>Holds information returned from the app event.</returns>
        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            SPRemoteEventResult result = new SPRemoteEventResult();



            switch (properties.EventType)
            {
                case SPRemoteEventType.AppInstalled:
                    HandleAppInstalled(properties);
                    break;

                case SPRemoteEventType.ItemAdded:
                    HandleItemAdded(properties);
                    break;

            }
            return result;


        }

        private void HandleItemAdded(SPRemoteEventProperties properties)
        {
            using (var clientContext =
                  TokenHelper.CreateRemoteEventReceiverClientContext(properties))
            {
                var web = clientContext.Web;
                var list = web.Lists.GetById(properties.ItemEventProperties.ListId);
                var item = list.GetItemById(properties.ItemEventProperties.ListItemId);
                clientContext.Load(item, a => a.File);
                clientContext.ExecuteQuery();

                var file = web.GetFileById(item.File.UniqueId);
                var content = file.OpenBinaryStream();
                clientContext.Load(file);
                clientContext.ExecuteQuery();

                var subscription = WebConfigurationManager.AppSettings["Subscription"];

                var client = new HttpClient();
                var uri = "https://api.projectoxford.ai/vision/v1/analyses?subscription-key=" + subscription;

                var streamContent = new StreamContent(content.Value);
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                var result = client.PostAsync(uri, streamContent).Result;
                var parsedResult = Newtonsoft.Json.JsonConvert.DeserializeObject<JSONResult>(result.Content.ReadAsStringAsync().Result);

                var categoria = parsedResult.Categorias.FirstOrDefault();
                if (categoria != null)
                {
                    item["Categoria"] = categoria.Name;
                    item.Update();
                    clientContext.ExecuteQuery();
                }
            }
        }

        private void HandleAppInstalled(SPRemoteEventProperties properties)
        {
            using (var clientContext =
                   TokenHelper.CreateAppEventClientContext(properties, false))
            {
                var myList = clientContext.Web.Lists.GetByTitle("Pictures");
                clientContext.Load(myList);
                clientContext.ExecuteQuery();

                var receiver =
                    new EventReceiverDefinitionCreationInformation
                    {
                        EventType = EventReceiverType.ItemAdded
                    };

                //Get WCF URL where this message was handled                
                var msg = OperationContext.Current.RequestContext.RequestMessage;

                receiver.ReceiverUrl = msg.Headers.To.ToString();

                receiver.ReceiverName = "ItemAdded";
                receiver.Synchronization = EventReceiverSynchronization.Synchronous;
                myList.EventReceivers.Add(receiver);

                clientContext.ExecuteQuery();

            }
        }

        /// <summary>
        /// This method is a required placeholder, but is not used by app events.
        /// </summary>
        /// <param name="properties">Unused.</param>
        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {
            throw new NotImplementedException();
        }

    }
}
