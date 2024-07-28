using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using JMS_Producer_Function_0.Utils.MQFunctions;
using System.Net.Http;

namespace JMS_Producer_Function_0
{
    public static class JMSProducerFunction
    {
        [FunctionName("JMSProducerFunction")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "JMSProducerFunction/publishMessage")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                // string name = req.Query["name"];

                // string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                // dynamic data = JsonConvert.DeserializeObject(requestBody);
                // name = name ?? data?.name;

                // string responseMessage = string.IsNullOrEmpty(name)
                //     ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                //     : $"Hello, {name}. This HTTP triggered function executed successfully.";

                MQUtils.SendMessage(new StreamReader(req.Body).ReadToEnd());

                return new HttpResponseMessage
                {
                    Content = new StringContent("Message Sent Successfully"),
                    StatusCode = System.Net.HttpStatusCode.OK
                };

                // log.LogInformation(MQUtils.ReceiveMessage());
            }
            catch (System.Exception e)
            {
                log.LogError(e.Message);
                log.LogError(e.StackTrace);
                log.LogError(e.InnerException.Message);
                log.LogError(e.InnerException.StackTrace);

                return new HttpResponseMessage
                {
                    Content = new StringContent("e.Message"),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
