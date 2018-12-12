using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.Common;
using ScheduleWidget.Schedule;
using ScheduleWidget.TemporalExpressions;
using System;
using System.Diagnostics;

namespace ScheduleWidget.UnitTests
{
    /// <summary>
    /// Test concepts like "Mon, Wed, and Fri of the 1st and 3rd weeks of the month."
    /// </summary>
    [TestClass]
    public class ScheduleMonthlyDayOfMonthUnitTests
    {
        [TestMethod]
        public void MonthlyIntervalDayOfMonthTest()
        {
            var monthly = new ScheduleDayOfMonth(2);
            var builder = new ScheduleBuilder();

            var schedule = builder
                .OnMonthly(monthly)
                .HavingFrequency(FrequencyType.MonthlyByDayOfMonth)
                .Create();

            var during = new DateRange(new DateTime(2030, 1, 1), new DateTime(2030, 6, 1));
            foreach (var date in schedule.Occurrences(during))
            {
                Debug.WriteLine(date.ToShortDateString());
            }

            var augustSecond = new DateTime(2040, 8, 2);
            var novemberSecond = new DateTime(2050, 11, 2);
            var julyThirtyOne = new DateTime(2040, 7, 31);
            var juneThird = new DateTime(2050, 6, 3);
            
            Assert.IsTrue(schedule.IsOccurring(augustSecond));
            Assert.IsTrue(schedule.IsOccurring(novemberSecond));

            Assert.IsFalse(schedule.IsOccurring(julyThirtyOne));
            Assert.IsFalse(schedule.IsOccurring(juneThird));
        }

    }
}
