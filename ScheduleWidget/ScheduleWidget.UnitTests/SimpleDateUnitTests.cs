using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.Common;
using ScheduleWidget.Schedule;
using ScheduleWidget.TemporalExpressions;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class SimpleDateUnitTests
    {
        [TestMethod]
        public void LowLevelSimpleDateTest1()
        {
            var aDate = new DateTime(2040, 7, 4);
            var scheduleDate = new ScheduleDate(aDate);

            var ludwigWittgensteinBirthday = new DateTime(1889, 4, 26);

            Assert.IsTrue(scheduleDate.Includes(new DateTime(2040, 7, 4)));
            Assert.IsFalse(scheduleDate.Includes(ludwigWittgensteinBirthday));
        }

        [TestMethod]
        public void SimpleDailyTest2()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .OnDaysOfWeek(DayInterval.All)
                .HavingFrequency(FrequencyType.Daily)
                .Create();

            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var forever = tomorrow.AddYears(1000);

            Assert.IsTrue(schedule.IsOccurring(today));
            Assert.IsTrue(schedule.IsOccurring(tomorrow));
            Assert.IsTrue(schedule.IsOccurring(forever));
        }
    }
}
