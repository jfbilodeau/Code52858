using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FuncDemo
{
    public class Echo
    {
        private readonly ILogger _logger;

        public Echo(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Echo>();
        }

        [Function("echo")]
        public HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req
        )
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // var name = req.Url.Query.FirstOrDefault(n => n == "name") ?? "Anonymous";

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"Welcome to Azure Functions! It is now {System.DateTime.Now}");

            return response;
        }
    }
}
