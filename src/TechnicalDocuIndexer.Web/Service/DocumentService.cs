using Azure.Search.Documents.Indexes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<String> FetchDocument(string key)
        {
            var searchClient = new SearchIndexClient(serviceName, indexName, new SearchCredentials(queryApiKey));

            var document = searchClient.Documents.Get<YourDocument>(id);
            return document;
        }
    }
}
