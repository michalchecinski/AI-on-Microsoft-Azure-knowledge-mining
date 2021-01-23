# Semantive - Knowledge Mining with Cognitive Services

## The Team :family:

1. Michał Chęciński 
2. Bartosz Strachowski
3. Michał Kolendo
4. Maciej Wasik
5. Jakub Rejent


## Use Case :mag:

Service to gather technical documentation for project or whole organization. The documentation includes:

- code
- scripts
- presentation
- project description, READMEs
- configuration files (eg. ARM templates)

In terms of the file types many filetypes can be used:

- Markdown
- json
- pdf
- Microsoft Office files (docx, pptx)

## The solution
### Functionalities of the system :artificial_satellite:

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
- Preview document on the page

### Used Azure services :desktop_computer:

The solution uses the following Azure Services:

- Azure Storage Account - to keep files with documentation.
- Azure Cognitive Search - to create search index on files.
- Azure WebApp - to access search and upload functionality.
- Azure Functions - to host custom skills for the Azure Search Indexer.
- Azure Cognitive Services - to extract additional data for text and images, also another Cognitive Service is used to get data from Bing WebSearch for one of the custom skills.

### Cloud architecture :world_map:

![image](https://github.com/michalchecinski/AI-on-Microsoft-Azure-knowledge-mining/blob/master/images/arch_semantive_azure.png?raw=true)



### Technical stack :hammer_and_wrench:

- Microsoft Azure
- ASP.NET Core MVC
- jQuery + Bootstrap 

## Reconstructing the solution :construction_worker:

To reconstruct the solution you can follow the steps described in the [How to reconstruct the solution document](https://github.com/michalchecinski/AI-on-Microsoft-Azure-knowledge-mining/blob/master/How to reconstruct the solution.md).


