using KikiCourierService.Services.CostCalculator;
using Microsoft.Extensions.Configuration;
using Moq;

namespace KikiCourierService.Test.ServiceTests
{
    [TestFixture]
    public class OfferCalculatorTest
    {
        private OfferCalculator _offerCalculator;
        private Mock<IConfiguration> _configurationMock;
        [SetUp]
        public void SetUp()
        {
            _configurationMock = new Mock<IConfiguration>();
            _offerCalculator = new OfferCalculator(_configurationMock.Object);
        }

        [Test]
        public async Task Should_LoadOffersAsync_ValidFile_ReturnsOffers()
        {
            string filePath = "testOffers.json";
            string offersJson = @"{
      ""offers"": [
        {
          ""offerCode"": ""OFR001"",
          ""minDistance"": 0,
          ""maxDistance"": 200,
          ""minWeight"": 70,
          ""maxWeight"": 200,
          ""discountPercent"": 10,
          ""isActive"": true
        }
      ]
    }"; 
            await File.WriteAllTextAsync(filePath, offersJson);

            var offers = await _offerCalculator.LoadOffersAsync(filePath);

            Assert.That(offers, Is.Not.Null);
            Assert.That(offers.Count, Is.EqualTo(1));
            Assert.That(offers[0].OfferCode, Is.EqualTo("OFR001"));

            File.Delete(filePath);

        }

        [Test]
        public async Task Should_LoadOffersAsync_FileDoesNotExist_ReturnsEmptyList()
        {
            string filePath = "nonexistent.json";

            var offers = await _offerCalculator.LoadOffersAsync(filePath);

            Assert.That(offers, Is.Not.Null);
            Assert.That(offers.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task Should_LoadOffersAsync_InvalidJson_ReturnsEmptyList()
        {
            string filePath = "invalidOffers.json";
            await File.WriteAllTextAsync(filePath, "not a json");

            var offers = await _offerCalculator.LoadOffersAsync(filePath);

            Assert.That(offers, Is.Not.Null);
            Assert.That(offers.Count, Is.EqualTo(0));

            File.Delete(filePath);
        }
    }
}

