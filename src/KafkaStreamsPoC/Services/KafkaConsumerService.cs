using Confluent.Kafka;
using KafkaStreamsPoC.Models;
using Newtonsoft.Json;

namespace KafkaStreamsPoC.Services
{
    public class KafkaConsumerService
    {
        private readonly IConsumer<Null, string> _consumer;

        public KafkaConsumerService(string bootstrapServers, string groupId)
        {
            var config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Null, string>(config).Build();
        }

        public void Consume(string topic, Action<ProductData> processMessage)
        {
            _consumer.Subscribe(topic);

            while (true)
            {
                var result = _consumer.Consume();
                var data = JsonConvert.DeserializeObject<ProductData>(result.Message.Value);
                processMessage(data);
            }
        }
    }
}
