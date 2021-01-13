using Azure;
using Azure.Search.Documents;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
            indexName = config.GetSection("Search").GetSection("IndexName").Value;
            endpoint = new Uri(config.GetSection("Search").GetSection("SearchEndpoint").Value);
            key = config.GetSection("Search").GetSection("APIKey").Value;
        }

        public async Task<DocumentDetails> FetchDocument(string id)
        {
            var searchClient = CreateSearchClient();

            var documentResponse = await searchClient.GetDocumentAsync<DocumentDetails>(id);
            var document = documentResponse.Value;
            document.StorageUrl = ConvertFromBase64String(document.metadata_storage_path);
            return document;
        }

        private SearchClient CreateSearchClient()
        {
            var searchClient = new SearchClient(endpoint, indexName, new AzureKeyCredential(key));
            return searchClient;
        }

        private string ConvertFromBase64String(string input)
        {
            var encodedStringWithoutTrailingCharacter = input.Substring(0, input.Length - 1);
            var encodedBytes =
                Microsoft.AspNetCore.WebUtilities.WebEncoders.Base64UrlDecode(encodedStringWithoutTrailingCharacter);
            return HttpUtility.UrlDecode(encodedBytes, Encoding.UTF8);
        }
    }
}