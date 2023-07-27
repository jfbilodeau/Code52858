// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using System.Text;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FuncDemo
{
    public class ManageBlob
    {
        private readonly ILogger _logger;

        public ManageBlob(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ManageBlob>();
        }

        [Function("ManageBlob")]
        public void Run([EventGridTrigger] MyEvent input)
        {
            try
            {
                _logger.LogInformation(input.Data.ToString());
                _logger.LogInformation($"input.Id: {input.Id}");

                var connectionString = "DefaultEndpointsProtocol=https;AccountName=jfbdemo;AccountKey=zCf3vIUNzFDxX4vOcrRqrZP4MnWyGQIy1bzhxltRNRfgw2ynbDcje5yne4wud280iy4hA9d+6pBN+AStPAK62w==;EndpointSuffix=core.windows.net";
                var client = new BlobClient(connectionString, "manage", input.Id);

                var stream = new MemoryStream(Encoding.UTF8.GetBytes(input.Data.ToString()));

                client.Upload(stream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }

    public class MyEvent
    {
        public string Id { get; set; }

        public string Topic { get; set; }

        public string Subject { get; set; }

        public string EventType { get; set; }

        public DateTime EventTime { get; set; }

        public object Data { get; set; }
    }
}
