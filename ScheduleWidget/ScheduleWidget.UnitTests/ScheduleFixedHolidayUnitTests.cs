using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.TemporalExpressions;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class ScheduleFixedHolidayUnitTests
    {
        [TestMethod]
        public void LowLevelFixedHolidayTest1()
        {
            var independenceDayUnitedStates = new ScheduleFixedHoliday(7, 4);
            var independenceDay = new DateTime(2040, 7, 4);
            var christmasDay = new DateTime(2040, 12, 25);
            var ludwigWittgensteinBirthday = new DateTime(1889, 4, 26);

            Assert.IsTrue(independenceDayUnitedStates.Includes(independenceDay));
            Assert.IsFalse(independenceDayUnitedStates.Includes(christmasDay));
            Assert.IsFalse(independenceDayUnitedStates.Includes(ludwigWittgensteinBirthday));
        }
    }
}
