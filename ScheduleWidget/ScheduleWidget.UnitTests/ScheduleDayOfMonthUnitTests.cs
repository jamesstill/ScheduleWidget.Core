using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.Common;
using ScheduleWidget.Schedule;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class ScheduleDayOfMonthUnitTests
    {
        [TestMethod]
        public void TwentyFourthOfEveryMonthTest()
        {
var builder = new ScheduleBuilder();

var schedule = builder
    .OnDayOfMonth(24)
    .HavingFrequency(FrequencyType.DayOfMonth)
    .Create();

var during = new DateRange(DateTime.Today, DateTime.Today.AddMonths(6));

foreach (var date in schedule.Occurrences(during))
{
    Console.WriteLine(date);
}

            var d1 = new DateTime(2030, 2, 24);
            var d2 = new DateTime(2030, 2, 25);
            var d3 = new DateTime(2030, 4, 24);
            var d4 = new DateTime(2029, 9, 1);

            Assert.IsTrue(schedule.IsOccurring(d1));
            Assert.IsTrue(schedule.IsOccurring(d3));

            Assert.IsFalse(schedule.IsOccurring(d2));            
            Assert.IsFalse(schedule.IsOccurring(d4));
        }
    }
}
