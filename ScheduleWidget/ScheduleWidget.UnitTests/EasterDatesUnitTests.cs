using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.Common;
using ScheduleWidget.TemporalExpressions;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class EasterDatesUnitTests
    {
        [TestMethod]
        public void EasterOrthodoxDateTest()
        {
            var d2020 = new DateTime(2020, 4, 19);
            var d2021 = new DateTime(2021, 5, 2);
            var d2022 = new DateTime(2022, 4, 24);
            var d2023 = new DateTime(2023, 4, 16);
            var d2024 = new DateTime(2024, 5, 5);
            var d2025 = new DateTime(2025, 4, 20);
            var d2026 = new DateTime(2026, 4, 12);
            var d2027 = new DateTime(2027, 5, 2);
            var d2028 = new DateTime(2028, 4, 16);
            var d2029 = new DateTime(2029, 4, 8);
            var d2030 = new DateTime(2030, 4, 28);

            //var dateList = Easter.GetEasterSundays(2020, 2030, Calendar.Orthodox);
            //foreach (var date in dateList)
            //{
            //    Debug.WriteLine(date.ToShortDateString());
            //}

            var s2020 = new ScheduleDate(Easter.GetEasterSunday(2020, EasterCalendar.Orthodox));
            var s2021 = new ScheduleDate(Easter.GetEasterSunday(2021, EasterCalendar.Orthodox));
            var s2022 = new ScheduleDate(Easter.GetEasterSunday(2022, EasterCalendar.Orthodox));
            var s2023 = new ScheduleDate(Easter.GetEasterSunday(2023, EasterCalendar.Orthodox));
            var s2024 = new ScheduleDate(Easter.GetEasterSunday(2024, EasterCalendar.Orthodox));
            var s2025 = new ScheduleDate(Easter.GetEasterSunday(2025, EasterCalendar.Orthodox));
            var s2026 = new ScheduleDate(Easter.GetEasterSunday(2026, EasterCalendar.Orthodox));
            var s2027 = new ScheduleDate(Easter.GetEasterSunday(2027, EasterCalendar.Orthodox));
            var s2028 = new ScheduleDate(Easter.GetEasterSunday(2028, EasterCalendar.Orthodox));
            var s2029 = new ScheduleDate(Easter.GetEasterSunday(2029, EasterCalendar.Orthodox));
            var s2030 = new ScheduleDate(Easter.GetEasterSunday(2030, EasterCalendar.Orthodox));

            Assert.IsTrue(s2020.Includes(d2020));
            Assert.IsTrue(s2021.Includes(d2021));
            Assert.IsTrue(s2022.Includes(d2022));
            Assert.IsTrue(s2023.Includes(d2023));
            Assert.IsTrue(s2024.Includes(d2024));
            Assert.IsTrue(s2025.Includes(d2025));
            Assert.IsTrue(s2026.Includes(d2026));
            Assert.IsTrue(s2027.Includes(d2027));
            Assert.IsTrue(s2028.Includes(d2028));
            Assert.IsTrue(s2029.Includes(d2029));
            Assert.IsTrue(s2030.Includes(d2030));
        }

        [TestMethod]
        public void EasterWesternDateTest()
        {
            var d2020 = new DateTime(2020, 4, 12);
            var d2021 = new DateTime(2021, 4, 4);
            var d2022 = new DateTime(2022, 4, 17);
            var d2023 = new DateTime(2023, 4, 9);
            var d2024 = new DateTime(2024, 3, 31);
            var d2025 = new DateTime(2025, 4, 20);
            var d2026 = new DateTime(2026, 4, 5);
            var d2027 = new DateTime(2027, 3, 28);
            var d2028 = new DateTime(2028, 4, 16);
            var d2029 = new DateTime(2029, 4, 1);
            var d2030 = new DateTime(2030, 4, 21);

            //var dateList = Easter.GetEasterSundays(2020, 2030);
            //foreach (var date in dateList)
            //{
            //    Debug.WriteLine(date.ToShortDateString());
            //}

            var s2020 = new ScheduleDate(Easter.GetEasterSunday(2020));
            var s2021 = new ScheduleDate(Easter.GetEasterSunday(2021));
            var s2022 = new ScheduleDate(Easter.GetEasterSunday(2022));
            var s2023 = new ScheduleDate(Easter.GetEasterSunday(2023));
            var s2024 = new ScheduleDate(Easter.GetEasterSunday(2024));
            var s2025 = new ScheduleDate(Easter.GetEasterSunday(2025));
            var s2026 = new ScheduleDate(Easter.GetEasterSunday(2026));
            var s2027 = new ScheduleDate(Easter.GetEasterSunday(2027));
            var s2028 = new ScheduleDate(Easter.GetEasterSunday(2028));
            var s2029 = new ScheduleDate(Easter.GetEasterSunday(2029));
            var s2030 = new ScheduleDate(Easter.GetEasterSunday(2030));

            Assert.IsTrue(s2020.Includes(d2020));
            Assert.IsTrue(s2021.Includes(d2021));
            Assert.IsTrue(s2022.Includes(d2022));
            Assert.IsTrue(s2023.Includes(d2023));
            Assert.IsTrue(s2024.Includes(d2024));
            Assert.IsTrue(s2025.Includes(d2025));
            Assert.IsTrue(s2026.Includes(d2026));
            Assert.IsTrue(s2027.Includes(d2027));
            Assert.IsTrue(s2028.Includes(d2028));
            Assert.IsTrue(s2029.Includes(d2029));
            Assert.IsTrue(s2030.Includes(d2030));
        }
    }
}
