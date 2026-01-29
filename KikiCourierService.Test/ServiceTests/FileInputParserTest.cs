
using KikiCourierService.Services.InputParsers;

namespace KikiCourierService.Test.ServiceTests
{

    [TestFixture]
    public class FileInputParserTest
    {
        private FileInputParser _fileInputParser;

        [SetUp]
        public void SetUp()
        {
            _fileInputParser = new FileInputParser();
        }

        [Test]
        public async Task Should_ParseAsync_ValidFile_ReturnsInputData()
        {
            string filePath = "validInput.txt";
            string[] lines = {
                "100 2",
                "PKG1 5 5 OFR001",
                "PKG2 15 5 OFR002",
                "2 70 200"
            };
            await File.WriteAllLinesAsync(filePath, lines);

            var result = await _fileInputParser.ParseAsync(filePath);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.BaseDeliveryCost, Is.EqualTo(100));
            Assert.That(result.NumberOfPackages, Is.EqualTo(2));
            Assert.That(result.Packages.Count, Is.EqualTo(2));
            Assert.That(result.Packages[0].Id, Is.EqualTo("PKG1"));
            Assert.That(result.VehicleInfo.NumberOfVehicles, Is.EqualTo(2));

            File.Delete(filePath);
        }

        [Test]
        public async Task Should_ParseAsync_FileDoesNotExist_ReturnsNull()
        {
            string filePath = "nonexistent.txt";

            var result = await _fileInputParser.ParseAsync(filePath);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Should_ParseAsync_EmptyFile_ReturnsNull()
        {
            string filePath = "empty.txt";
            await File.WriteAllTextAsync(filePath, "");

            var result = await _fileInputParser.ParseAsync(filePath);

            Assert.That(result, Is.Null);

            File.Delete(filePath);
        }

        [Test]
        public async Task Should_ParseAsync_InvalidBaseCost_ReturnsNull()
        {
            string filePath = "invalidBaseCost.txt";
            string[] lines = {
                "invalid 2",
                "PKG1 5 5 OFR001",
                "PKG2 15 5 OFR002",
                "2 70 200"
            };
            await File.WriteAllLinesAsync(filePath, lines);

            var result = await _fileInputParser.ParseAsync(filePath);

            Assert.That(result, Is.Null);

            File.Delete(filePath);
        }

        [Test]
        public async Task Should_ParseAsync_InvalidPackageCount_ReturnsNull()
        {
            string filePath = "invalidPackageCount.txt";
            string[] lines = {
                "100 invalid",
                "PKG1 5 5 OFR001",
                "PKG2 15 5 OFR002",
                "2 70 200"
            };
            await File.WriteAllLinesAsync(filePath, lines);

            var result = await _fileInputParser.ParseAsync(filePath);

            Assert.That(result, Is.Null);

            File.Delete(filePath);
        }
    }
}
