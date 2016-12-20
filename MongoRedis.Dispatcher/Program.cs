using System.Configuration;
using Microsoft.ServiceBus.Messaging;

namespace MongoRedis.Dispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            var queueName = "queuetest1";

            var client = QueueClient.CreateFromConnectionString(connectionString, queueName);
            var message = new BrokeredMessage("This is a test message!");
            client.Send(message);
        }
    }
}
