using System;

namespace ScheduleWidget.UnitTests
{
    public static class TestHelper
    {
        public static DateTime GetNextWeekDate(DateTime from, DayOfWeek nextDayOfWeek)
        {
            var daysToAdd = ((int)nextDayOfWeek - (int)from.DayOfWeek + 7) % 7;
            return from.AddDays(daysToAdd);
        }
    }
}
