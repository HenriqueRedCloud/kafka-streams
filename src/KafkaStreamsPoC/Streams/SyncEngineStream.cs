using Confluent.Kafka.Streams;
using KafkaStreamsPoC.Models;
using KafkaStreamsPoC.Services;

namespace KafkaStreamsPoC.Streams
{
    public class SyncEngineStream
    {
        private readonly StreamBuilder _builder;
        private readonly SyncEngineService _syncEngineService;

        public SyncEngineStream(StreamBuilder builder, SyncEngineService syncEngineService)
        {
            _builder = builder;
            _syncEngineService = syncEngineService;
        }

        public void BuildStream()
        {
            _builder.Stream<string, string>("ValidatedProductData")
                .MapValues(value => 
                {
                    var data = JsonConvert.DeserializeObject<ProductData>(value);
                    _syncEngineService.PushToMarketplaceBackend(data);
                    return value;
                })
                .To("SyncEngineUpdates");
        }
    }
}
