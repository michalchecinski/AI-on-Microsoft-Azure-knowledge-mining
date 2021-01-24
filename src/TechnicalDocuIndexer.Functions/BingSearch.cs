using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TechnicalDocuIndexer.Functions.Model;

namespace TechnicalDocuIndexer.Functions
{

    public static class BingSearch
    {

        static readonly string BingEntityApiEndpoint = "https://api.bing.microsoft.com/v7.0/entities/";
        private const string BingSearchApiEndpoint = "https://api.bing.microsoft.com/v7.0/search";
        private const string Key = "<BING-API-KEY>";


        [FunctionName("entity-search")]
        public static IActionResult EntitySearch(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Entity Search function: C# HTTP trigger function processed a request.");

            var response = new WebApiResponse
            {
                Values = new List<OutputRecord>()
            };

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var data = JsonConvert.DeserializeObject<WebApiRequest>(requestBody);

            if (data == null)
            {
                return new BadRequestObjectResult("The request schema does not match expected schema.");
            }
            if (data.Values == null)
            {
                return new BadRequestObjectResult("The request schema does not match expected schema. Could not find values array.");
            }

            foreach (var record in data.Values)
            {
                if (record == null || record.RecordId == null) continue;

                OutputRecord responseRecord = new OutputRecord
                {
                    RecordId = record.RecordId
                };

                try
                {
                    responseRecord.Data =  new OutputRecord.OutputRecordContainer(FetchEntityMetadata(record.Data.Name).Result);
                }
                catch (Exception e)
                {
                    log.LogError(e.Message);
                }
                finally
                {
                    response.Values.Add(responseRecord);
                }
            }

            return new OkObjectResult(response);
        }

        private static async Task<OutputRecord.OutputRecordData> FetchEntityMetadata(string entityName)
        {
            var uriSearch = BingSearchApiEndpoint + "?q=" + entityName + "&mkt=en-us&count=1&answerCount=1&promote=images%2Cvideos";
            var result = new OutputRecord.OutputRecordData();

            var client = new HttpClient();
            var request = new HttpRequestMessage();

            request.Method = HttpMethod.Get;
            request.Headers.Add("Ocp-Apim-Subscription-Key", Key);

            request.RequestUri = new Uri(uriSearch);
            HttpResponseMessage responseSearch = await client.SendAsync(request);
            string responseSearchBody = await responseSearch?.Content?.ReadAsStringAsync();

            BingSearchEntity bingSearchEntity = JsonConvert.DeserializeObject<BingSearchEntity>(responseSearchBody);
            if (bingSearchEntity != null)
            {
                return AddTopEntityData(entityName,  bingSearchEntity);
            }
            return result;
        }

        private static OutputRecord.OutputRecordData AddTopEntityData(string entityName, BingSearchEntity bingSearchEntity)
        {
            if (bingSearchEntity != null && bingSearchEntity.WebPages != null)
            {
                if (bingSearchEntity.Videos != null && bingSearchEntity.Videos?.value.Count > 2)
                {
                    bingSearchEntity.Videos.Name = entityName;
                    bingSearchEntity.Videos.value.RemoveRange(2, bingSearchEntity.Videos.value.Count - 2);
                }

                if (bingSearchEntity.WebPages?.value[0].deepLinks?.Count > 2)
                    bingSearchEntity.WebPages.value[0].deepLinks.RemoveRange(2, bingSearchEntity.WebPages.value[0].deepLinks.Count - 2);

                
                bingSearchEntity.WebPages.Name = entityName;
              
                return new OutputRecord.OutputRecordData()
                {
                    Videos = bingSearchEntity.Videos,
                    WebPages = bingSearchEntity.WebPages
                };
            }
            return new OutputRecord.OutputRecordData();
        }
    }
}