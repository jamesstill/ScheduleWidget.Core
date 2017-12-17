using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.Common;
using ScheduleWidget.Schedule;
using System.Diagnostics;

namespace ScheduleWidget.UnitTests
{
    /// <summary>
    /// Test concepts like "Mon, Wed, and Fri of the 1st and 3rd weeks of the month."
    /// </summary>
    [TestClass]
    public class ScheduleMonthlyDayInWeekOfMonthUnitTests
    {
        [TestMethod]
        public void MonthlyIntervalFriInFourthWeekOfMonthTest()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .DuringMonth(WeekInterval.Fourth)
                .OnDaysOfWeek(DayInterval.Fri)
                .HavingFrequency(FrequencyType.MonthlyByDayInWeekOfMonth)
                .Create();

            var during = new DateRange(new DateTime(2030, 1, 1), new DateTime(2030, 6, 1));
            foreach (var date in schedule.Occurrences(during))
            {
                Debug.WriteLine(date.ToShortDateString());
            }

            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 1, 25)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 2, 22)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 3, 22)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 4, 26)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 5, 24)));

            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 1, 18)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 2, 1)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 3, 29)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 4, 19)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 5, 31)));
        }

        [TestMethod]
        public void MonthlyIntervalFriInLastWeekOfMonthTest()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .DuringMonth(WeekInterval.Last)
                .OnDaysOfWeek(DayInterval.Fri)
                .HavingFrequency(FrequencyType.MonthlyByDayInWeekOfMonth)
                .Create();

            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 2, 22)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 3, 29)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 5, 31)));

            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 1, 25)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 2, 1)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 3, 22)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 4, 26)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 5, 24)));
        }

        /// <summary>
        /// Mon, Wed, and Fri falling on the first and third weeks of the month
        /// </summary>
        [TestMethod]
        public void MonthlyIntervalMonWedFriInFirstAndThirdWeekOfMonthTest()
        {
            var builder = new ScheduleBuilder();

            // first and third Mon, Wed, and Fri of every month
            var schedule = builder
                .DuringMonth(WeekInterval.First | WeekInterval.Third)
                .OnDaysOfWeek(DayInterval.Mon | DayInterval.Wed | DayInterval.Fri)
                .HavingFrequency(FrequencyType.MonthlyByDayInWeekOfMonth)
                .Create();

            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 1, 2)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 1, 4)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 1, 14)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 1, 16)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 1, 18)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 2, 1)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 2, 11)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 2, 13)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 2, 15)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 3, 1)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 3, 11)));

            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 1, 7)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 1, 9)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 1, 11)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 1, 23)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 1, 28)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 2, 25)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 2, 27)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 3, 6)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 3, 22)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 3, 29)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 4, 22)));
        }
    }
}
