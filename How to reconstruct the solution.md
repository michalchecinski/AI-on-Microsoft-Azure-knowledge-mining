# Reconstruct the solution



## Functionalities of the system:

- Custom Skill - Bing search for entities
- Custom Skill - estimated reading time
- Custom Skill - template recognizing
- Display documents with immaculate preview of files of type:
  - png, jpg
  - markdown
  - json
- Searching big datasets of documents
  - filtering by parameters
  - paging
  - display document details
- Aggregate information regarding files
- Uploading new documents through the webpage
- Role Based Access to the system using Auth0

## Prerequisities

- microsoft account
- active subscription
- installed [.Net Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- IDE for launching application locally if preferred [Visual Studio 2019](https://visualstudio.microsoft.com/pl/vs/)

## Instructions

### Auth0

1. Go to [Auth0]([Auth0: Secure access for everyone. But not just anyone.](https://auth0.com/)) website. If you do not have an account, sign up. You can log in using your microsoft account also

2. Create a **Machine To Machine application**. Name it *technicaldocuindexer*.

3. Click on your newly created application and from the settings subpage copy:

   - Domain

   - Client ID

   - Client Secret

   - Go to *APIs* subpage and copy API IDENTIFIER

     ![audience](https://github.com/michalchecinski/AI-on-Microsoft-Azure-knowledge-mining/blob/master/images/audience.PNG?raw=true)

4. Go back to settings subpage, scroll down to **Allowed Callback URLs** and paste there following values:

   https://localhost:44378/callback, https://TechnicalDocuIndexerWeb.azurewebsites.net/callback

5. In Allowed Logout URLs paste following values:

   https://localhost:44378, https://TechnicalDocuIndexerWeb.azurewebsites.net

6. Go to **Rules** subtab and *Create new Rule*.

7. In new rule paste following code and save it.

   - >  function (user, context, callback) {
     >   const assignedRoles = (context.authorization || {}).roles;
     >   const idTokenClaims = context.idToken || {};
     >
     >   idTokenClaims['https://schemas.quickstarts.com/roles'] = assignedRoles;
     >
     >   callback(null, user, context);
     > }

8. In *Users & Roles* subtab, go to *Roles*:

   - Create Role **FileUploader** and **SearchReader**

9. Go to *Users* and create 2 new users:

   - searchreader@<yourchoiceofdomain>
   - fileuploader@<yourchoiceofdomain>

10. After creating both users, Click on their details, go to subtab Roles and add:

    - SearchReader for searchreader@ user
    - FileUploader for fileuploader@ user

11. Go to APIs subtab on the left and select your *technicaldocuindexer* API.

12. In settings tab, make sure you have enabled **RBAC** Settings and **Add Permissions in the Access Token**.

### Azure portal 

1. Log in to [Azure portal]([Home - Microsoft Azure](https://portal.azure.com/#home)) and create a new resource group.
2. Create Function App resource "**TDICustomSkills**" , where we will store our serverless functions (custom skills).
3. Create an Azure Cognitive Search Resource and name it *cogni-technical-docu-indexer01*.
4. In newly created resource, create a new index.
5. Copy from newly created search service:
   - From *Keys* section, Primary admin key
   - Search Service name, which should be *cogsearchtechnicaldocuindexer01*
   - Search index name *azureblob-index*
6. Copy from Cognitive Services resource:
   - From keys and endpoint copy *KEY 1*

### Azure DevOps 

1. Go to [Azure DevOps](https://dev.azure.com/) portal, log in using your Microsoft account and create a new project.
2. Decide whether your code will be hosted in Github or Azure Repos - we assume the code will be in Azure Repos.
3. Click on ![Repos](https://github.com/michalchecinski/AI-on-Microsoft-Azure-knowledge-mining/blob/master/images/Repos.PNG?raw=true) tab and create a new repository.
4. In newly created repository cloned [Azure Knowledge mining repository]([michalchecinski/AI-on-Microsoft-Azure-knowledge-mining (github.com)](https://github.com/michalchecinski/AI-on-Microsoft-Azure-knowledge-mining)) and push it to master branch.
5. Next, go to *Pipelines* and create a new variable group *tdi-base* in your repository.
6. Fill up variables:
   - Auth0Settings.Audience - API Identifier
   - Auth0Settings.ClientId
   - Auth0Settings.ClientSecret
   - Auth0Settings.Domain
   - Resource.Group.Name
   - Search.APIKey
   - Search.IndexName
   - Search.QueryKey
   - Search.Service - search service name
7. Save them.
8. Go to *Pipelines* subtab in *Azure Pipelines*.
9. Create a new pipeline, with code in Azure Repos Git. It should auto-select an existing *azure-pipelines.yml* file from your repo.
10. Submit creation of new pipeline and run it.
11. Pipeline should have created all necessary resources for the application to work.

### Deploy Azure Functions

1. In your local repository, launch *CustomFunctions* solution.
2. Right click on the project, and *Publish* the code to the cloud, to *TDICustomSkills* FunctionApp.
3. Repeat the proces for *BingFunction* solution.

### Add skillset to Search Service

1. In your *cogsearchtechnicaldocuindexer01* click on *skillsets* and create a new skillset.

2.  In skill definition Templates, select *Custom Web API Skill - Azure Functions*

   - For Template recognizing function paste 

     ```json
      "inputs": [
             {
               "name": "extension",
               "source": "/document/metadata_storage_file_extension"
             },
             {
               "name": "content",
               "source": "/document/content"
             }
           ],
           "outputs": [
             {
               "name": "foundServices",
               "targetName": "foundServices"
             }
     ```

   - For word count paste:

     ```json
     "inputs": [
             {
               "name": "extension",
               "source": "/document/metadata_storage_file_extension"
             },
             {
               "name": "content",
               "source": "/document/content"
             }
           ],
           "outputs": [
             {
               "name": "wordCount",
               "targetName": "wordCount"
             },
             {
               "name": "timeToRead",
               "targetName": "timeToRead"
             }
           ]
     ```

3. Save new skillset.

### Upload Files for Indexer

1. Go to your resource group.
2. Click on *sttechnicaldocuindexer* Storage Account.
3. In Containers subtab, create a new container *initial-load* and paste there files you would like to be searched upon (pdf, json, markdown files, png).
4. Go to your *cogsearchtechnicaldocuindexer01* and create new Indexer in *Indexer* tab.
5. Fill out neccesary parameters. Choose an existing connection for *Storage Connection string* and pinpoint initial-load container.

### Create Index

1. Go to Indexes tab and create a new Index.
2. After creating new Index, remember to click on it and set **CORS** allowed origin type to **All**.

Navigate to https://technicaldocuindexerweb.azurewebsites.net, login using searchreader user and test your app!



