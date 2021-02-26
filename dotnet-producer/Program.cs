using Confluent.Kafka;
using System;
using System.Threading;

namespace dotnet_producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            var producer = new ProducerBuilder<Null, string>(config).Build();

            while (true)
            {
                var username = Faker.Name.First(); ;

                Console.WriteLine($"Creating user {username}");

                producer.Produce("users-string", new Message<Null, string>()
                {
                    Value = username
                });

                Thread.Sleep(100);
            }
        }
    }
}
