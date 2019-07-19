using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanoutSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "fanout-exch", type: ExchangeType.Fanout, durable: false, autoDelete: false, arguments: null);

            channel.QueueDeclare(args[0], durable: false, exclusive: false, autoDelete: false);

            channel.QueueBind(args[0], "fanout-exch", "", null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                Console.WriteLine($"Message received:{message}");

            };
            channel.BasicConsume(args[0], true, consumer);

            Console.WriteLine("Writing for messages...Please Enter to exit");
            Console.ReadLine();

            channel.Dispose();
            connection.Dispose();


        }
    }
}
