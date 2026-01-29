using KikiCourierService.Helpers;

namespace KikiCourierService.Test.HelpersTest
{
    [TestFixture]
    public class TimeHelperTest
    {
        [Test]
        public void Should_GetTime_ValidInputs_ReturnsRoundedTime()
        {
            decimal speed = 10m;
            decimal distance = 25m;

            var result = TimeHelper.getTime(speed, distance);

            Assert.That(result, Is.EqualTo(2.5m));
        }

        [Test]
        public void Should_GetTime_ZeroSpeed_ThrowsDivideByZeroException()
        {
            decimal speed = 0m;
            decimal distance = 10m;

            Assert.Throws<System.DivideByZeroException>(() => TimeHelper.getTime(speed, distance));
        }
    }
}
