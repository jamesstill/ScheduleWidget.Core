using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.TemporalExpressions;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class OneTimeOnlyUnitTests
    {
        [TestMethod]
        public void OneTimeOnlyDateTest1()
        {
            // no need for a schedule
            var date = new DateTime(2030, 6, 5);
            var d1 = new ScheduleDate(date);
            
            Assert.IsTrue(d1.Includes(date));
            Assert.IsFalse(d1.Includes(new DateTime(2030, 6, 6)));
        }

        [TestMethod]
        public void OneTimeOnlyDateTest2()
        {
            // no need for a schedule
            var d1 = new ScheduleDate(DateTime.Today);
            Assert.IsTrue(d1.Includes(DateTime.Today));
            Assert.IsFalse(d1.Includes(DateTime.Today.AddDays(1)));
        }
    }
}
