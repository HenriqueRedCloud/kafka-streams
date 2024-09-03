using Confluent.Kafka.Streams;
using KafkaStreamsPoC.Services;
using KafkaStreamsPoC.Streams;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DataValidationService>();
builder.Services.AddSingleton<SyncEngineService>();
builder.Services.AddSingleton<KafkaProducerService>(sp =>
    new KafkaProducerService("localhost:9092"));

var app = builder.Build();

app.MapGet("/", () => "Kafka Streams PoC running!");

// Initialize Kafka Streams
var config = new StreamConfig<StringSerDes, StringSerDes>
{
    ApplicationId = "kafka-streams-poc",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var streamBuilder = new StreamBuilder();
var validationService = app.Services.GetRequiredService<DataValidationService>();
var syncEngineService = app.Services.GetRequiredService<SyncEngineService>();

var validationStream = new DataValidationStream(streamBuilder, validationService);
validationStream.BuildStream();

var syncEngineStream = new SyncEngineStream(streamBuilder, syncEngineService);
syncEngineStream.BuildStream();

using var stream = new KafkaStream(streamBuilder.Build(), config);
await stream.StartAsync();

// Simulate Bulk Upload
var kafkaProducerService = app.Services.GetRequiredService<KafkaProducerService>();

await kafkaProducerService.BulkProduceAsync("ProductDataIngestion", "path/to/your/products.csv");

Console.WriteLine("Bulk data upload completed.");

app.Run();
