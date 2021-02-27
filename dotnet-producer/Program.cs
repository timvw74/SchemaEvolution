using Confluent.Kafka;
using System;
using System.Threading;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using timvw.avro;
using System.Threading.Tasks;

namespace dotnet_producer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = "localhost:8081"
            };

            var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
            var producer = new ProducerBuilder<Null, user>(config)
                .SetValueSerializer(new AvroSerializer<user>(schemaRegistry))
                .Build();

            while (true)
            {
                var user = new user()
                {
                    firstname = Faker.Name.First(),
                    lastname = Faker.Name.Last()
                };

                Console.WriteLine($"Creating user {user.firstname}");

                await producer.ProduceAsync("users-avro", new Message<Null, user>()
                {
                    Value = user
                });

                Thread.Sleep(1000);
            }
        }
    }
}
