using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.TemporalExpressions;
using ScheduleWidget.Common;
using ScheduleWidget.Schedule;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class ScheduleAnniversaryUnitTests
    {
        [TestMethod]
        public void ScheduleAnniversaryDateTest1()
        {
            var anniversary = new ScheduleAnnual(8, 1);
            var builder = new ScheduleBuilder();

            var schedule = builder
                .OnAnniversary(anniversary)
                .HavingFrequency(FrequencyType.Yearly)
                .Create();

            var augustOne = new DateTime(2040, 8, 1);
            var julyThirtyOne = new DateTime(2040, 7, 31);
            var nextTuesday = TestHelper.GetNextWeekDate(DateTime.Today, DayOfWeek.Tuesday);

            Assert.IsTrue(schedule.IsOccurring(augustOne));
            Assert.IsFalse(schedule.IsOccurring(julyThirtyOne));
            Assert.IsFalse(schedule.IsOccurring(nextTuesday));
        }
    }
}
