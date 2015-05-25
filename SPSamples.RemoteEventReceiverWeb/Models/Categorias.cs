using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPSamples.RemoteEventReceiverWeb.Models
{
    public class Categorias
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}