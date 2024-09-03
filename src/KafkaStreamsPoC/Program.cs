using KafkaStreamsPoC.Models;
using KafkaStreamsPoC.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Configure services
builder.Services.AddSingleton<DataValidationService>();
builder.Services.AddSingleton<KafkaProducerService>(sp => 
    new KafkaProducerService("localhost:9092"));
builder.Services.AddSingleton<KafkaConsumerService>(sp => 
    new KafkaConsumerService("localhost:9092", "kafka-streams-group"));
builder.Services.AddSingleton<SyncEngineService>();

app.MapGet("/", () => "Kafka Streams PoC running!");

// Initialize the consumer and processing
var kafkaConsumer = app.Services.GetRequiredService<KafkaConsumerService>();
var dataValidationService = app.Services.GetRequiredService<DataValidationService>();
var syncEngineService = app.Services.GetRequiredService<SyncEngineService>();

kafkaConsumer.Consume("ProductDataIngestion", data =>
{
    if (dataValidationService.IsValidProductData(data))
    {
        var validatedData = dataValidationService.MapToStandardFormat(data);
        syncEngineService.PushToMarketplaceBackend(validatedData);
    }
});

app.Run();
