using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "demo-exch",
                type: ExchangeType.Fanout,
                durable: false,
                             autoDelete: false,
                arguments: null
                );
            var arguments = new Dictionary<string, object>()
            {
                {"x-message-ttl",60000 },//TTL for the Queue messages (all messages)
                {"x-expires",30*60*1000 }//Query expiry after idle timeout,in milliseconds
            };
            channel.QueueDeclare("demoq", durable: true, exclusive: false, autoDelete: false, arguments: arguments);
            channel.QueueBind("demoq", "demo-exch", "", null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                Console.WriteLine($"Message Received:{message}");
                channel.BasicAck(ea.DeliveryTag, multiple: false);//expli
            };
            channel.BasicConsume(queue: "demoq", autoAck: false, consumer: consumer);//disable autoack


            Console.WriteLine("Enter the message (Empty to exit):");
            Console.ReadLine();
            channel.Dispose();
            connection.Dispose();
        }
    }
}
