using Confluent.Kafka;
using System;
using System.Text.Json;
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
                var user = new User()
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last()
                };

                Console.WriteLine($"Creating user {user.FirstName}");

                producer.Produce("users-json", new Message<Null, string>()
                {
                    Value = JsonSerializer.Serialize(user)
                });

                Thread.Sleep(1000);
            }
        }
    }
}
