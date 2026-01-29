using KikiCourierService.Helpers;

namespace KikiCourierService.Test.HelpersTest
{
    internal class RoundOffHelperTest
    {
        [TestFixture]
        public class RoundOffHelperTests
        {
            [Test]
            public void Should_RoundOff_ReturnsRoundedDown()
            {
                decimal value = 12.347m;

                var result = RoundOffHelper.roundOff(value);

                Assert.That(result, Is.EqualTo(12.34m));
            }

            
            [Test]
            public void Should_RoundOff_ExactTwoDecimals_ReturnsSameValue()
            {
                decimal value = 10.12m;

                var result = RoundOffHelper.roundOff(value);

                Assert.That(result, Is.EqualTo(10.12m));
            }
        }
    }
}
