using KafkaStreamsPoC.Models;

namespace KafkaStreamsPoC.Services
{
    public class DataValidationService
    {
        public bool IsValidProductData(ProductData data)
        {
            return data != null && !string.IsNullOrWhiteSpace(data.ProductId) &&
                   data.Price > 0 && data.Stock >= 0;
        }

        public ProductData MapToStandardFormat(ProductData data)
        {
            // Implement mapping logic if needed
            data.Name = data.Name?.ToUpper();
            return data;
        }
    }
}
