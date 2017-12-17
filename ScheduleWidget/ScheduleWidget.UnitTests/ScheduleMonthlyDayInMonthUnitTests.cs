using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.Common;
using ScheduleWidget.Schedule;

namespace ScheduleWidget.UnitTests
{
    /// <summary>
    /// Test concepts like "1st and 3rd Mon, Wed, and Fri of the month"
    /// </summary>
    [TestClass]
    public class ScheduleMonthlyDayInMonthUnitTests
    {
        /// <summary>
        /// first Thu of every month
        /// </summary>
        [TestMethod]
        public void MonthlyIntervalFirstThuTest()
        {
            var builder = new ScheduleBuilder();

            // first Thu of every month
            var schedule = builder
                .DuringMonth(WeekInterval.First)
                .OnDaysOfWeek(DayInterval.Thu)
                .HavingFrequency(FrequencyType.MonthlyByDayInMonth)
                .Create();

            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 1, 3)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 2, 7)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 3, 7)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 4, 4)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 5, 2)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 6, 6)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 7, 4)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 8, 1)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 9, 5)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 10, 3)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 11, 7)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 12, 5)));

            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 1, 10)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 2, 28)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 3, 14)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 4, 11)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 5, 9)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 6, 27)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 7, 18)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 8, 8)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 9, 26)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 10, 10)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 11, 28)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 12, 12)));
        }

        /// <summary>
        /// second and third Sat of the month
        /// </summary>
        [TestMethod]
        public void MonthlyIntervalSecondAndThirdSaturdayTest()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .DuringMonth(WeekInterval.Second | WeekInterval.Third)
                .OnDaysOfWeek(DayInterval.Sat)
                .HavingFrequency(FrequencyType.MonthlyByDayInMonth)
                .Create();

            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 2, 9)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 2, 16)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 3, 9)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 3, 16)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 4, 20)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 5, 11)));

            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 2, 2)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 2, 23)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 3, 2)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 3, 23)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 3, 30)));
        }

        /// <summary>
        /// last Mon of every month
        /// </summary>
        [TestMethod]
        public void MonthlyIntervalLastMondayTest()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .DuringMonth(WeekInterval.Last)
                .OnDaysOfWeek(DayInterval.Mon)
                .HavingFrequency(FrequencyType.MonthlyByDayInMonth)
                .Create();

            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 1, 28)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 2, 25)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2030, 3, 25)));

            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 2, 4)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 4, 1)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2030, 5, 20)));
        }

        /// <summary>
        /// first Mon of every month
        /// </summary>
        [TestMethod]
        public void MonthlyIntervalFirstMondayTest()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .DuringMonth(WeekInterval.First)
                .OnDaysOfWeek(DayInterval.Mon)
                .HavingFrequency(FrequencyType.MonthlyByDayInMonth)
                .Create();

            Assert.IsTrue(schedule.IsOccurring(new DateTime(2031, 1, 6)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2031, 2, 3)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2031, 3, 3)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2031, 4, 7)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2031, 5, 5)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2031, 6, 2)));

            Assert.IsFalse(schedule.IsOccurring(new DateTime(2031, 2, 4)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2031, 4, 1)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2031, 5, 20)));
        }
    }
}
