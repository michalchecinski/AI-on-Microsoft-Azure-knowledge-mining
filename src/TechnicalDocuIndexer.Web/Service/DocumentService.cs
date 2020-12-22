using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalDocuIndexer.Web.Models;

namespace TechnicalDocuIndexer.Web.Service.Utils
{
    public class DocumentService
    {
        private string indexName;
        private Uri endpoint;
        private string key;

        public DocumentService(IConfiguration config)
        {
            indexName = config.GetSection("IndexName").Value;
            endpoint = new Uri(config.GetSection("SearchEndpoint").Value);
            key = config.GetSection("APIKey").Value;
        }

        public DocumentDetails FetchDocument(string id)
        {
            var searchClient = CreateSearchClient();

            DocumentDetails document = searchClient.GetDocument<DocumentDetails>(id);
            document.metadata_storage_path = ConvertFromBase64String(document.metadata_storage_path); 
            return document;
        }

        private SearchClient CreateSearchClient()
        {
            SearchClient searchClient = new SearchClient(endpoint, indexName, new AzureKeyCredential(key));
            return searchClient;
        }

        private string ConvertFromBase64String(string input)
        {
            if (String.IsNullOrWhiteSpace(input)) return input;
            try
            {
                string working = input.Replace('-', '+').Replace('_', '/'); ;
                while (working.Length % 4 != 0)
                {
                    working += '=';
                }
                return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(working));
            }
            catch (Exception)
            {
                return input;
            }
        }
    }
}
