namespace KikiCourierService.Helpers
{
    public static class TimeHelper
    {
        public static decimal getTime(decimal speed, decimal distance)
        {
            return RoundOffHelper.roundOff(distance / speed);
        }

    }
}
