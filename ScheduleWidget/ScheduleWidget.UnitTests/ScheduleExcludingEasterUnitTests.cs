using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.Common;
using ScheduleWidget.Schedule;
using ScheduleWidget.TemporalExpressions;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.UnitTests
{
    [TestClass]
    public class ScheduleExcludingEasterUnitTests
    {
        [TestMethod]
        public void ScheduleExcludingEasterUnitTest1()
        {
            var observedHolidays = GetEasterHolidays(DateTime.Today.Year, DateTime.Today.Year + 40);

            // schedule for the third and last Sunday of each month expect for Easter
            var builder = new ScheduleBuilder();
            var schedule = builder
                .DuringMonth(WeekInterval.Third | WeekInterval.Last)
                .OnDaysOfWeek(DayInterval.Sun)
                .Excluding(observedHolidays)
                .HavingFrequency(FrequencyType.MonthlyByDayInMonth)
                .Create();

            // display all dates in the schedule for the next 40 years
            var range = new DateRange(DateTime.Today, DateTime.Today.AddYears(10));
            foreach (var date in schedule.Occurrences(range))
            {
                Debug.WriteLine(date.ToShortDateString());
            }

            var easterDate1 = new DateTime(2018, 4, 1);
            var easterDate2 = new DateTime(2025, 4, 20);
            var easterDate3 = new DateTime(2031, 4, 13);
            var easterDate4 = new DateTime(2035, 3, 25);
            var easterDate5 = new DateTime(2049, 4, 18);

            var regularSunday1 = new DateTime(2018, 12, 30);
            var regularSunday2 = new DateTime(2020, 3, 29);
            var regularSunday3 = new DateTime(2024, 3, 17);
            var regularSunday4 = new DateTime(2030, 4, 28);
            var regularSunday5 = new DateTime(2039, 3, 20);
            var regularSunday6 = new DateTime(2039, 3, 27);

            Assert.IsFalse(schedule.IsOccurring(easterDate1));
            Assert.IsFalse(schedule.IsOccurring(easterDate2));
            Assert.IsFalse(schedule.IsOccurring(easterDate3));
            Assert.IsFalse(schedule.IsOccurring(easterDate4));
            Assert.IsFalse(schedule.IsOccurring(easterDate5));

            Assert.IsTrue(schedule.IsOccurring(regularSunday1));
            Assert.IsTrue(schedule.IsOccurring(regularSunday2));
            Assert.IsTrue(schedule.IsOccurring(regularSunday3));
            Assert.IsTrue(schedule.IsOccurring(regularSunday4));
            Assert.IsTrue(schedule.IsOccurring(regularSunday5));
            Assert.IsTrue(schedule.IsOccurring(regularSunday6));
        }

        private static TemporalExpressionUnion GetEasterHolidays(int startYear, int endYear)
        {
            Debug.Assert(endYear >= startYear);
            var list = Easter.GetEasterSundays(startYear, endYear);
            var union = new TemporalExpressionUnion();
            foreach (var date in list)
            {
                union.Add(new ScheduleDate(date));
            }
            return union;
        }
    }
}
