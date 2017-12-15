using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.TemporalExpressions;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class ScheduleRangeInYearUnitTests
    {
        [TestMethod]
        public void LowLevelSimpleRangeInYearTest1()
        {
            var nonWinterMonths = new ScheduleRangeInYear(4, 11);
            var winterDate = new DateTime(2040, 1, 17);
            var summerDate = new DateTime(2040, 7, 4);

            Assert.IsTrue(nonWinterMonths.Includes(summerDate));
            Assert.IsFalse(nonWinterMonths.Includes(winterDate));
        }
    }
}
