using System;
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

                consumer.Subscribe("users-string");

                while (true)
                {
                    var consumeResult = consumer.Consume();
                    Console.WriteLine($"Received User: {consumeResult.Message.Value}");
                }            
            }
        }
    }
}
