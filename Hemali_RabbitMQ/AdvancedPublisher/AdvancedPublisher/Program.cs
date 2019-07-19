﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace AdvancedPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "fanout-exch",
                type: ExchangeType.Fanout,
                durable: false,
                             autoDelete: false,
                arguments: null
                );

            while (true)
            {

                Console.Write("Enter the message (Empty to exit):");
                var message = Console.ReadLine();
                if (string.IsNullOrEmpty(message))
                {
                    break;
                }
                var props = channel.CreateBasicProperties();
                props.Persistent = true;
                props.ContentType = "text/plain";
                props.Expiration = "15000";

                var payload = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "fanout-exch",
                    routingKey: "",
                    basicProperties: props,
                    body: payload
                    );
            }
        }
    }
}

