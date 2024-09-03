using Xunit;
using KafkaStreamsPoC.Services;
using KafkaStreamsPoC.Models;

namespace KafkaStreamsPoC.Tests
{
    public class DataValidationServiceTests
    {
        private readonly DataValidationService _service;

        public DataValidationServiceTests()
        {
            _service = new DataValidationService();
        }

        [Fact]
        public void IsValidProductData_ValidData_ReturnsTrue()
        {
            var data = new ProductData { ProductId = "1", Name = "Test", Price = 10, Stock = 100 };
            var result = _service.IsValidProductData(data);
            Assert.True(result);
        }

        [Fact]
        public void IsValidProductData_InvalidData_ReturnsFalse()
        {
            var data = new ProductData { ProductId = "1", Name = "Test", Price = -10, Stock = 100 };
            var result = _service.IsValidProductData(data);
            Assert.False(result);
        }
    }
}
