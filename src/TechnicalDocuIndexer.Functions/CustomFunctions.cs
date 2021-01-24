using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TechnicalDocuIndexer.Functions
{
    public static class CustomFunctions
    {
        [FunctionName("word-counting")]
        public static IActionResult WordCounting(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext executionContext)
        {
            log.LogInformation("Hello World Custom Skill: C# HTTP trigger function processed a request.");

            string skillName = executionContext.FunctionName;
            IEnumerable<WebApiRequestRecord> requestRecords = WebApiSkillHelpers.GetRequestRecords(req);
            if (requestRecords == null)
            {
                return new BadRequestObjectResult($"{skillName} - Invalid request record array.");
            }

            string[] articleExtensions = { ".md", ".txt", ".pdf", ".doc", ".docx", ".rft" };

            WebApiSkillResponse response = WebApiSkillHelpers.ProcessRequestRecords(skillName, requestRecords,
                (inRecord, outRecord) =>
                {
                    var extension = inRecord.Data["extension"] as string;
                    var content = inRecord.Data["content"] as string;
                    var wordCount = 0;
                    var timeToRead = 0.0;

                    if (articleExtensions.Contains(extension.ToLower()))
                    {
                        content = content.Replace("\n", " ").Replace("\t", " ");
                        content = content.Replace("\t", " ");

                        string pattern = "\\w+";
                        Regex regex = new Regex(pattern);

                        wordCount = regex.Matches(content).Count;

                        timeToRead = Math.Round(wordCount / 250.0);
                    }


                    outRecord.Data["wordCount"] = wordCount;
                    outRecord.Data["timeToRead"] = timeToRead;
                    return outRecord;
                });

            return new OkObjectResult(response);
        }


        [FunctionName("template-recognizing")]
        public static IActionResult ARMTemplateRecognizing(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext executionContext)
        {
            log.LogInformation("Hello World Custom Skill: C# HTTP trigger function processed a request.");

            string skillName = executionContext.FunctionName;
            IEnumerable<WebApiRequestRecord> requestRecords = WebApiSkillHelpers.GetRequestRecords(req);
            if (requestRecords == null)
            {
                return new BadRequestObjectResult($"{skillName} - Invalid request record array.");
            }

            Templates templates = new Templates();

            using (StreamReader r = new StreamReader(Path.Combine(executionContext.FunctionDirectory, "../services.json")))
            {
                string json = r.ReadToEnd();
                templates = JsonConvert.DeserializeObject<Templates>(json);
            }

            WebApiSkillResponse response = WebApiSkillHelpers.ProcessRequestRecords(skillName, requestRecords,
                (inRecord, outRecord) =>
                {
                    var extension = inRecord.Data["extension"] as string;
                    var content = inRecord.Data["content"] as string;
                    List<string> foundServices = new List<string>();

                    if (extension.ToLower().Equals(".json"))
                    {
                        foundServices = templates.Services.FindAll(x => content.Contains($"\"type\": \"{x}\""));
                    }

                    outRecord.Data["foundServices"] = foundServices;
                    return outRecord;
                });

            return new OkObjectResult(response);
        }
    }

    public class Templates
    {
        public List<string> Services { get; set; }
    }
}
