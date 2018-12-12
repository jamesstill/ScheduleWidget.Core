using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.Schedule;
using ScheduleWidget.Common;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class ScheduleQuarterlyUnitTests
    {
        /// <summary>
        /// The event recurs on the first and third Monday of the first month
        /// of each quarter in a year. Passing in enums as args.
        /// </summary>
        [TestMethod]
        public void BuildQuarterlyScheduleTest1()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .DuringQuarter(QuarterInterval.All)
                .DuringMonthOfQuarter(MonthOfQuarterInterval.First)
                .DuringMonth(WeekInterval.First | WeekInterval.Third)
                .OnDaysOfWeek(DayInterval.Mon)
                .HavingFrequency(FrequencyType.Quarterly)
                .Create();

            var startDate = new DateTime(2030, 1, 1);
            // display all dates in the schedule for the next 20 years
            var range = new DateRange(startDate, startDate.AddYears(20));
            foreach (var date in schedule.Occurrences(range))
            {
                Debug.WriteLine(date.ToShortDateString());
            }

            var firstMonOfJanQ1 = new DateTime(2030, 1, 7);
            var thirdMonOfJulQ3 = new DateTime(2033, 7, 18);
            var firstMonOfOctQ4 = new DateTime(2035, 10, 1);

            var date1 = TestHelper.GetNextWeekDate(DateTime.Today, DayOfWeek.Tuesday);
            var date2 = new DateTime(2030, 8, 17);
            var date3 = new DateTime(2050, 1, 1);

            Assert.IsTrue(schedule.IsOccurring(firstMonOfJanQ1));
            Assert.IsTrue(schedule.IsOccurring(thirdMonOfJulQ3));
            Assert.IsTrue(schedule.IsOccurring(firstMonOfOctQ4));

            Assert.IsFalse(schedule.IsOccurring(date1));
            Assert.IsFalse(schedule.IsOccurring(date2));
            Assert.IsFalse(schedule.IsOccurring(date3));
        }

        /// <summary>
        /// The event recurs on the first and third Monday of the first month
        /// of each quarter in a year. Passing in int bit flag values as args
        /// just like it might come out of a database.
        /// </summary>
        [TestMethod]
        public void BuildQuarterlyScheduleTest2()
        {
            var builder = new ScheduleBuilder();

            // the first and third Mon of the first month of each quarter
            var schedule = builder
                .DuringQuarter(15)
                .DuringMonthOfQuarter(1)
                .DuringMonth(5)
                .OnDaysOfWeek(2)
                .HavingFrequency(2)
                .Create();

            var firstMonOfJanQ1 = new DateTime(2030, 1, 7);
            var thirdMonOfJulQ3 = new DateTime(2033, 7, 18);
            var firstMonOfOctQ4 = new DateTime(2035, 10, 1);

            var date1 = TestHelper.GetNextWeekDate(DateTime.Today, DayOfWeek.Tuesday);
            var date2 = new DateTime(2030, 8, 17);
            var date3 = new DateTime(2050, 1, 1);

            Assert.IsTrue(schedule.IsOccurring(firstMonOfJanQ1));
            Assert.IsTrue(schedule.IsOccurring(thirdMonOfJulQ3));
            Assert.IsTrue(schedule.IsOccurring(firstMonOfOctQ4));

            Assert.IsFalse(schedule.IsOccurring(date1));
            Assert.IsFalse(schedule.IsOccurring(date2));
            Assert.IsFalse(schedule.IsOccurring(date3));
        }

        [TestMethod]
        public void BuildQuarterlyScheduleTest3()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .DuringQuarter(QuarterInterval.First | QuarterInterval.Third)
                .DuringMonthOfQuarter(MonthOfQuarterInterval.Third)
                .DuringMonth(WeekInterval.Last)
                .OnDaysOfWeek(DayInterval.Fri)
                .Create();
        }
    }
}
