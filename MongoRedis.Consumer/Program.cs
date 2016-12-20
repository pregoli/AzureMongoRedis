using System;
using System.Configuration;
using Microsoft.ServiceBus.Messaging;

namespace MongoRedis.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            var queueName = "queuetest1";

            var client = QueueClient.CreateFromConnectionString(connectionString, queueName);
            //
            client.OnMessage(message =>
            {
                Console.WriteLine(String.Format("Message body: {0}", message.GetBody<String>()));
                Console.WriteLine(String.Format("Message id: {0}", message.MessageId));
            });

            Console.ReadLine();
        }
    }
}
