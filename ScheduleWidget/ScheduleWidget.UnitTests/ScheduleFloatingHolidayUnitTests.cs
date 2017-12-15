using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.Common;
using ScheduleWidget.TemporalExpressions;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class ScheduleFloatingHolidayUnitTests
    {
        [TestMethod]
        public void LowLevelFloatingHolidayTest1()
        {
            var laborDayUnitedStates = new ScheduleFloatingHoliday(MonthOfYear.Sep, DayOfWeek.Monday, WeekInterval.First);
            var bannedBookWeekStartDate = new ScheduleFloatingHoliday(MonthOfYear.Sep, DayOfWeek.Sunday, WeekInterval.Last);

            var laborDayIn2032 = new DateTime(2032, 9, 6);
            var ludwigWittgensteinBirthday = new DateTime(1889, 4, 26);

            Assert.IsTrue(laborDayUnitedStates.Includes(laborDayIn2032));
            Assert.IsFalse(laborDayUnitedStates.Includes(ludwigWittgensteinBirthday));
        }
    }
}
