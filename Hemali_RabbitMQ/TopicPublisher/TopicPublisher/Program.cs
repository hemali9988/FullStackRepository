using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topic-exch",
                type: ExchangeType.Topic,
                durable: false,
                             autoDelete: false,
                arguments: null
                );

            while (true)
            {
                Console.Write("Enter the routing key (eg: <facility>.<severity>):");
                var routingKey = Console.ReadLine();
                Console.Write("Enter the message (Empty to exit):");
                var message = Console.ReadLine();
                if (string.IsNullOrEmpty(message))
                {
                    break;
                }
                var payload = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "topic-exch",
                    routingKey: routingKey,
                    basicProperties: null,
                    body: payload
                    );
            }
        }
    }
    
}
