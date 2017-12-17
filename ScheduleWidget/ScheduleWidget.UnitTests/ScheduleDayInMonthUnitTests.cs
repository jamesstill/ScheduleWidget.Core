using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.TemporalExpressions;
using ScheduleWidget.Common;
using ScheduleWidget.Schedule;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class ScheduleDayInMonthUnitTests
    {
        [TestMethod]
        public void FirstSundayOfMonthTest()
        {
            var sunday = new ScheduleDayInMonth(DayInterval.Sun, WeekInterval.First);

            Assert.IsTrue(sunday.Includes(new DateTime(2035, 1, 7)));
            Assert.IsTrue(sunday.Includes(new DateTime(2035, 4, 1)));
            Assert.IsTrue(sunday.Includes(new DateTime(2035, 11, 4)));

            Assert.IsFalse(sunday.Includes(new DateTime(2035, 3, 11)));
            Assert.IsFalse(sunday.Includes(new DateTime(2035, 5, 27)));
            Assert.IsFalse(sunday.Includes(new DateTime(2035, 9, 9)));
        }

        [TestMethod]
        public void SecondTuesdayOfMonthTest()
        {
            var secondTuesday = new ScheduleDayInMonth(DayInterval.Tue, WeekInterval.Second);

            Assert.IsTrue(secondTuesday.Includes(new DateTime(2028, 2, 8)));
            Assert.IsTrue(secondTuesday.Includes(new DateTime(2028, 4, 11)));
            Assert.IsTrue(secondTuesday.Includes(new DateTime(2028, 5, 9)));
            Assert.IsTrue(secondTuesday.Includes(new DateTime(2028, 6, 13)));
            Assert.IsTrue(secondTuesday.Includes(new DateTime(2028, 8, 8)));
            Assert.IsTrue(secondTuesday.Includes(new DateTime(2028, 10, 10)));

            Assert.IsFalse(secondTuesday.Includes(new DateTime(2028, 6, 20)));
            Assert.IsFalse(secondTuesday.Includes(new DateTime(2028, 8, 1)));
            Assert.IsFalse(secondTuesday.Includes(new DateTime(2028, 11, 7)));
            Assert.IsFalse(secondTuesday.Includes(new DateTime(2028, 12, 19)));
        }

        [TestMethod]
        public void LastWednesdayOfMonthTest()
        {
            var day = new ScheduleDayInMonth(DayInterval.Wed, WeekInterval.Last);

            Assert.IsTrue(day.Includes(new DateTime(2037, 1, 28)));
            Assert.IsTrue(day.Includes(new DateTime(2037, 2, 25)));
            Assert.IsTrue(day.Includes(new DateTime(2037, 6, 24)));

            Assert.IsFalse(day.Includes(new DateTime(2037, 5, 20)));
            Assert.IsFalse(day.Includes(new DateTime(2037, 9, 23)));
            Assert.IsFalse(day.Includes(new DateTime(2037, 11, 18)));
        }

        [TestMethod]
        public void FirstAndThirdSaturdayEveryMonthTest()
        {
            var day = new ScheduleDayInMonth(DayInterval.Sat, WeekInterval.First | WeekInterval.Third);

            Assert.IsTrue(day.Includes(new DateTime(2019, 9, 7)));
            Assert.IsTrue(day.Includes(new DateTime(2019, 9, 21)));
            Assert.IsTrue(day.Includes(new DateTime(2040, 9, 1)));
            Assert.IsTrue(day.Includes(new DateTime(2040, 9, 15)));
            
            Assert.IsFalse(day.Includes(new DateTime(2040, 8, 25)));
            Assert.IsFalse(day.Includes(new DateTime(2040, 10, 13)));
            Assert.IsFalse(day.Includes(new DateTime(2040, 12, 8)));
            Assert.IsFalse(day.Includes(new DateTime(2040, 12, 29)));
        }

        [TestMethod]
        public void FirstAndThirdSaturdayEveryMonthTestWithSchedule()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .DuringMonth(WeekInterval.First | WeekInterval.Third)
                .OnDaysOfWeek(DayInterval.Sat)
                .HavingFrequency(FrequencyType.MonthlyByDayInMonth)
                .Create();

            var during = new DateRange(DateTime.Today, DateTime.Today.AddMonths(6));

            foreach (var date in schedule.Occurrences(during))
            {
                Console.WriteLine(date);
            }
        }

        [TestMethod]
        public void FirstMondayEveryMonthTestWithSchedule()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .DuringMonth(WeekInterval.First)
                .OnDaysOfWeek(DayInterval.Mon)
                .HavingFrequency(FrequencyType.MonthlyByDayInMonth)
                .Create();


            var today = new DateTime(2030, 12, 20);
            var during = new DateRange(today, today.AddMonths(6));

            foreach (var date in schedule.Occurrences(during))
            {
                Debug.WriteLine(date.ToShortDateString());
            }
        }
    }
}
