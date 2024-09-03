using KafkaStreamsPoC.Models;

namespace KafkaStreamsPoC.Services
{
    public class DataValidationService
    {
        public bool IsValidProductData(ProductData data)
        {
            // Implement validation logic
            return data != null && data.Price > 0 && data.Stock >= 0;
        }

        public ProductData MapToStandardFormat(ProductData data)
        {
            // Implement mapping logic if needed
            return data;
        }
    }
}
