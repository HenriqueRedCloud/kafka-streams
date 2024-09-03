using KafkaStreamsPoC.Models;

namespace KafkaStreamsPoC.Services
{
    public class SyncEngineService
    {
        public void PushToMarketplaceBackend(ProductData data)
        {
            // Implement logic to push data to the marketplace backend
            Console.WriteLine($"Data synced to marketplace: {data.ProductId}");
        }
    }
}
