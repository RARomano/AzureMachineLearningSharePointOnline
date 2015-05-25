# AzureMachineLearningSharePointOnline
Esse exemplo mostra como utilizar o Projeto Oxford para categorizar imagens no SharePoint

Para rodar esse exemplo você precisará:
- Visual Studio 2013
- Azure Subscription para hostear a Web Application do Remote Event Receiver


## Clonar ou fazer o download do Repositório

Rode o comando abaixo no Git Shell:

`git clone https://github.com/RARomano/RemoteEventReceiver.git`


## Criar a solução

Para criar uma nova solução, vá em **File** e depois em **New Project...**
Escolha **SharePoint App**. 

Nesse exemplo, eu criei uma provider-hosted app.
![URL](https://cloud.githubusercontent.com/assets/12012898/7779900/4349c9cc-00af-11e5-95f5-f65accdb40ba.png)

Escolha o tipo de projeto
![Project Type](https://cloud.githubusercontent.com/assets/12012898/7779904/4cd008a8-00af-11e5-83d6-4baff811901e.png)

Escolha a opção **Use Windows Azure Access Control Service**

![AzureACS](https://cloud.githubusercontent.com/assets/12012898/7779911/594bc5a4-00af-11e5-9dbd-dbce2a01abc4.png)

Clique em **Server Explorer**, **Azure** e depois em **WebSites**. Clique com o botão direito em WebSites e escolha a opção **Create new site**.

![CreateSite](https://cloud.githubusercontent.com/assets/12012898/7780147/c4a7f956-00b1-11e5-9cdc-c76f15b1faf4.png)

Digite um endereço, escolha a região e clique em **Create**.


### Publicar Solução

Para publicar a solução, clique no projeto referente ao MVC com o botão direito e depois em **Publish...**

![Publish](https://cloud.githubusercontent.com/assets/12012898/7780179/10b4ac4a-00b2-11e5-89b0-993812a6e6a7.png)

Clique em **Microsoft Azure WebSites**.

![AzureWebSites](https://cloud.githubusercontent.com/assets/12012898/7780186/2198bbb4-00b2-11e5-870e-548579f140ed.png)

Escolha o site que você criou na etapa anterior e depois clique em **Publish**.

### Registrar um novo App no SharePoint

Abrir a URL **"_layouts/AppRegNew.aspx"** no seu SharePoint 

Clicar no botão gerar do ID do Cliente e do Segredo do Cliente. Digitar o Título da App preencher um domínio para a APP (colocar a url que você criou no Azure WebSite) e uma URL de redirecionamento (colocar a Url que você criou no Azure WebSites com https)

![Criar um App](https://cloud.githubusercontent.com/assets/12012898/7780232/a248662e-00b2-11e5-9f27-934fc8152a1f.png)

Altere o web.config do projeto MVC e coloque o ClientID que foi gerado na etapa anterior na Tag ClientId.

### Publicar App

Para publicar a app, clique no projeto referente ao App com o botão direito e depois em **Publish...**

![Publish](https://cloud.githubusercontent.com/assets/12012898/7780410/f9788198-00b4-11e5-9c05-71a1ca8b3019.png)

Clique no botão Edit

![Publish](https://cloud.githubusercontent.com/assets/12012898/7780255/c29aa0ea-00b2-11e5-91ff-9137ca49adcd.png)

Preencher o Client Id e Client Secret gerados na etapa anterior e clicar em Finish.

![AppSecret](https://cloud.githubusercontent.com/assets/12012898/7780265/cfda7820-00b2-11e5-8e43-55d5824382dc.png)

Nas propriedades do projeto, mude **Handle App Installed** para **true**, com isso será gerado um Serviço no projeto MVC.

Faça o upload do App no SharePoint

### Preparando o Ambiente

Para esse ambiente funcionar, crie uma Picture Library chamada Pictures e adicione uma coluna do tipo Texto com o nome de Categoria.


### Instalar Computer Vision APIs no seu Azure Subscription

Clique em **New**, **Compute** e depois em **Marketplace**.

![MarketPlace](https://cloud.githubusercontent.com/assets/12012898/7801729/d4dc1852-02ff-11e5-9a1a-162955971375.png)

Escolha Computer Vision APIs.

![Choose API](https://cloud.githubusercontent.com/assets/12012898/7801742/097fa4f2-0300-11e5-97dc-63f6d44e2b66.png)

Escolha um nome.

![Choose API](https://cloud.githubusercontent.com/assets/12012898/7801745/13396a8c-0300-11e5-8e2f-2bbffb945ead.png)

Confirme a compra

![compra](https://cloud.githubusercontent.com/assets/12012898/7801804/bf04f714-0300-11e5-9b2c-71fea7148dcb.png)

Depois de pronto, clique me Manage

![Manage](https://cloud.githubusercontent.com/assets/12012898/7801818/e8a162e2-0300-11e5-809a-679da17afdf7.png)

No item Primary key, clique em show e anote essa key. Utilizaremos essa key para chamar a API.

![Manage](https://cloud.githubusercontent.com/assets/12012898/7801833/2bc3a6a2-0301-11e5-95c6-34ac73aeeba7.png)


### Alterar o Web.Config do Projeto MVC

Abra o Web.Config e altera a tag com a propriedade **Subscription** com o valor obtido na etapa anterior

### Colocar a biblioteca Newtonsoft.Json do Projeto MVC para copiar localmente

![Manage](https://cloud.githubusercontent.com/assets/12012898/7802357/3fa918b0-030a-11e5-90a8-bc7ea0b2afa8.png)


Dessa forma, ao fazer o upload de uma imagem, o campo Categoria será preenchido com o valor extraído do serviço automaticamente.

![Result](https://cloud.githubusercontent.com/assets/12012898/7802371/7eface46-030a-11e5-847c-59684114f96b.png)


