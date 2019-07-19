using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicSubscriber
{
    class Program
    {
        //Command line args eg: dotnet TopicSubscriber <queuename> <key-pattern>
        //eg: dotnet TopicSubscriber Queue1 #
        //eg: dotnet TopicSubscriber Queue2  *.error
        //eg: dotnet TopicSubscriber Queue3 sonu.*
        //eg: dotnet TopicSubscriber Queue4 rahul.info
        //eg: dotnet TopicSubscriber Queue5 *.warning *.info
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topic-exch", type: ExchangeType.Topic, durable: false, autoDelete: false, arguments: null);

            channel.QueueDeclare(args[0], durable: false, exclusive: false, autoDelete: false);

            var keys = args.Skip(1).Take(args.Length - 1);
            foreach(var key in keys)
            {
                channel.QueueBind(args[0], "topic-exch",key, null);
            }
 

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
