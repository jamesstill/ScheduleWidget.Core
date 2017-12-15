using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.Common;
using ScheduleWidget.Schedule;
using ScheduleWidget.TemporalExpressions;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class ScheduleHolidayUnitTests
    {
        [TestMethod]
        public void LowLevelHolidayTest1()
        {
            var holidays = GetHolidays();
            var builder = new ScheduleBuilder();

            // build a simple daily schedule excluding holidays
            var schedule = builder
                .Excluding(holidays)
                .HavingFrequency(FrequencyType.Daily)
                .Create();

            var excluded1 = new DateTime(2052, 12, 25); // Christmas
            var excluded2 = new DateTime(2034, 7, 4);   // Independence Day
            var excluded3 = new DateTime(2051, 9, 4);   // Labor Day
            var excluded4 = new DateTime(2024, 4, 26);  // Wittgenstein Birthday

            var ok1 = new DateTime(2038, 2, 12);
            var ok2 = new DateTime(2019, 12, 26);
            var ok3 = new DateTime(2045, 1, 1);
            var ok4 = new DateTime(2056, 8, 2);

            Assert.IsTrue(schedule.IsOccurring(ok1));
            Assert.IsTrue(schedule.IsOccurring(ok2));
            Assert.IsTrue(schedule.IsOccurring(ok3));
            Assert.IsTrue(schedule.IsOccurring(ok4));

            Assert.IsFalse(schedule.IsOccurring(excluded1));
            Assert.IsFalse(schedule.IsOccurring(excluded2));
            Assert.IsFalse(schedule.IsOccurring(excluded3));
            Assert.IsFalse(schedule.IsOccurring(excluded4));
        }

        private static TemporalExpressionUnion GetHolidays()
        {
            var union = new TemporalExpressionUnion();
            var independenceDayUnitedStates = new ScheduleFixedHoliday(7, 4);
            var laborDayUnitedStates = new ScheduleFloatingHoliday(MonthOfYear.Sep, DayOfWeek.Monday, WeekInterval.First);
            var christmasDay = new ScheduleFixedHoliday(12, 25);
            var ludwigWittgensteinBirthday = new ScheduleFixedHoliday(4, 26);

            union.Add(independenceDayUnitedStates);
            union.Add(laborDayUnitedStates);
            union.Add(christmasDay);
            union.Add(ludwigWittgensteinBirthday);

            return union;
        }
    }
}
