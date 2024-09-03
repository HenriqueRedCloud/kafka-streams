using Confluent.Kafka.Streams;
using KafkaStreamsPoC.Models;
using KafkaStreamsPoC.Services;

namespace KafkaStreamsPoC.Streams
{
    public class DataValidationStream
    {
        private readonly StreamBuilder _builder;
        private readonly DataValidationService _validationService;

        public DataValidationStream(StreamBuilder builder, DataValidationService validationService)
        {
            _builder = builder;
            _validationService = validationService;
        }

        public void BuildStream()
        {
            _builder.Stream<string, string>("ProductDataIngestion")
                .Filter((key, value) => 
                    _validationService.IsValidProductData(JsonConvert.DeserializeObject<ProductData>(value)))
                .MapValues(value => 
                    JsonConvert.SerializeObject(_validationService.MapToStandardFormat(JsonConvert.DeserializeObject<ProductData>(value))))
                .To("ValidatedProductData");
        }
    }
}
