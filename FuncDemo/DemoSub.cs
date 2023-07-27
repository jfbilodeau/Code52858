using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FuncDemo
{
    public class DemoSub
    {
        private readonly ILogger _logger;

        public DemoSub(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DemoSub>();
        }

        [Function("DemoSub")]
        public void Run([ServiceBusTrigger("DemoTopic", "DemoSub", Connection = "Endpoint=sb://jfservicebus.servicebus.windows.net/;SharedAccessKeyName=DemoSub;SharedAccessKey=xaC/o0D+ymcjeFnh0oZ8MUMimGy3s8pX5+ASbK40DAY=;EntityPath=demotopic")] string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
