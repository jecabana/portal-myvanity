using System;

namespace MyVanity.Common.Helpers
{
    public static class Helper
    {
        public static bool EqualDates(DateTime date1, DateTime date2)
        {
            return date1.Day == date2.Day && date1.Month == date2.Month && date1.Year == date2.Year;
        }
    }
}
