using Confluent.Kafka;
using KafkaStreamsPoC.Models;
using Newtonsoft.Json;

namespace KafkaStreamsPoC.Services
{
    public class KafkaProducerService
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducerService(string bootstrapServers)
        {
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task ProduceAsync(string topic, ProductData data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = jsonData });
        }
    }
}
