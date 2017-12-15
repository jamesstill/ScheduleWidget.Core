using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.TemporalExpressions;
using ScheduleWidget.Schedule;
using ScheduleWidget.Common;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class ScheduleStreetCleaningUnitTests
    {
        /// <summary>
        /// Street cleaning occurs from April to October on the first and third Monday of each
        /// month, excluding holidays. In this test there are two holidays: July 4 and Labor
        /// Day (first Monday in Sep). The street cleaning example is taken directly from 
        /// Martin Fowler's white paper "Recurring Events for Calendars".
        /// </summary>
        [TestMethod]
        public void BuildStreetCleaningScheduleTest1()
        {
            var observedHolidays = GetObservedHolidays();
            var rangeInYear = new ScheduleRangeInYear(4, 10);
            var builder = new ScheduleBuilder();

            var schedule = builder
                .DuringYear(rangeInYear)
                .DuringMonth(WeekInterval.First | WeekInterval.Third)
                .OnDaysOfWeek(DayInterval.Mon)
                .Excluding(observedHolidays)
                .HavingFrequency(FrequencyType.Monthly)
                .Create();

            var firstMondayInOctober = new DateTime(2040, 10, 1);
            var thirdMondayInJuly = new DateTime(2040, 7, 16);
            var nextTuesday = TestHelper.GetNextWeekDate(DateTime.Today, DayOfWeek.Tuesday);
            var futureFriday = new DateTime(2040, 8, 17);

            Assert.IsTrue(schedule.IsOccurring(firstMondayInOctober));
            Assert.IsTrue(schedule.IsOccurring(thirdMondayInJuly));
            Assert.IsFalse(schedule.IsOccurring(nextTuesday));
            Assert.IsFalse(schedule.IsOccurring(futureFriday));
        }

        /// <summary>
        /// Street cleaning schedule 
        /// </summary>
        [TestMethod]
        public void BuildStreetCleaningScheduleTest2()
        {
            var observedHolidays = GetObservedHolidays();
            var rangeInYear = new ScheduleRangeInYear(4, 10);
            var builder = new ScheduleBuilder();

            var schedule = builder
                .HavingFrequency(3) // monthly
                .DuringYear(rangeInYear)
                .DuringMonth(5) // 1st and 3rd weeks of month
                .OnDaysOfWeek(2) // Mon
                .Excluding(observedHolidays)
                .Create();

            var firstMondayInOctober = new DateTime(2040, 10, 1);
            var thirdMondayInJuly = new DateTime(2040, 7, 16);
            var nextTuesday = TestHelper.GetNextWeekDate(DateTime.Today, DayOfWeek.Tuesday);
            var futureFriday = new DateTime(2040, 8, 17);

            Assert.IsTrue(schedule.IsOccurring(firstMondayInOctober));
            Assert.IsTrue(schedule.IsOccurring(thirdMondayInJuly));
            Assert.IsFalse(schedule.IsOccurring(nextTuesday));
            Assert.IsFalse(schedule.IsOccurring(futureFriday));
        }

        private static TemporalExpressionUnion GetObservedHolidays()
        {
            var observedHolidays = new TemporalExpressionUnion();
            observedHolidays.Add(new ScheduleFixedHoliday(7, 4));
            observedHolidays.Add(new ScheduleFloatingHoliday(MonthOfYear.Sep, DayOfWeek.Monday, WeekInterval.First));
            return observedHolidays;
        }
    }
}
