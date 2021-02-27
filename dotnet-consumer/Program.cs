using System;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using timvw.avro;

namespace dotnet_consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig()
            {
                BootstrapServers = "localhost:9092",
                GroupId = "dotnet-users-avro",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = "localhost:8081"
            };

            using (var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig))
            {
                using (var consumer = new ConsumerBuilder<Null, user>(config)
                    .SetValueDeserializer(new AvroDeserializer<user>(schemaRegistry).AsSyncOverAsync())
                    .Build())
                {
                    consumer.Subscribe("users-avro");

                    while (true)
                    {
                        var consumeResult = consumer.Consume();
                        var user = consumeResult.Message.Value;
                        Console.WriteLine($"Received User: Firstname:{user.firstname} Lastname:{user.lastname}");
                    }
                }
            }
        }
    }
}
