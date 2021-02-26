using System;
using System.Text.Json;
using Confluent.Kafka;

namespace dotnet_consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig()
            {
                BootstrapServers = "localhost:9092",
                GroupId = "dotnet-users-string",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build()) {

                consumer.Subscribe("users-json");

                while (true)
                {
                    var consumeResult = consumer.Consume();
                    var user = JsonSerializer.Deserialize<User>(consumeResult.Message.Value);
                    Console.WriteLine($"Received User: Firstname:{user.FirstName} Lastname:{user.LastName}");
                }            
            }
        }
    }
}
