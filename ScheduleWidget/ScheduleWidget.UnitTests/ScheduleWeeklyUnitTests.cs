using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleWidget.Schedule;
using ScheduleWidget.Common;

namespace ScheduleWidget.UnitTests
{
    /// <summary>
    /// Test monthly frequencies with week intervals of the type 
    /// "first and third Friday of every month."
    /// </summary>
    [TestClass]
    public class ScheduleWeeklyUnitTests
    {
        [TestMethod]
        public void WeekIntervalScheduleTest1()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .OnDaysOfWeek(DayInterval.Mon | DayInterval.Wed | DayInterval.Fri)
                .HavingFrequency(FrequencyType.Weekly)
                .Create();

            var date1 = new DateTime(2030, 2, 4);
            var date2 = new DateTime(2030, 2, 5);
            var date3 = new DateTime(2030, 2, 6);
            var date4 = new DateTime(2030, 2, 7);
            var date5 = new DateTime(2030, 2, 8);

            Assert.IsTrue(schedule.IsOccurring(date1));
            Assert.IsTrue(schedule.IsOccurring(date3));
            Assert.IsTrue(schedule.IsOccurring(date5));

            Assert.IsFalse(schedule.IsOccurring(date2));
            Assert.IsFalse(schedule.IsOccurring(date4));
        }

        [TestMethod]
        public void WeekIntervalScheduleTest2()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .DuringMonth(WeekInterval.First | WeekInterval.Third)
                .OnDaysOfWeek(DayInterval.Fri)
                .HavingFrequency(FrequencyType.Monthly)
                .Create();

            var date1 = new DateTime(2030, 1, 4);  
            var date2 = new DateTime(2030, 7, 19);

            Assert.IsTrue(schedule.IsOccurring(date1));
            Assert.IsTrue(schedule.IsOccurring(date2));

            var date3 = TestHelper.GetNextWeekDate(DateTime.Today, DayOfWeek.Tuesday);
            var date4 = new DateTime(2030, 9, 13);
            var date5 = new DateTime(2030, 9, 27);

            Assert.IsFalse(schedule.IsOccurring(date3));
            Assert.IsFalse(schedule.IsOccurring(date4));
            Assert.IsFalse(schedule.IsOccurring(date5));
        }

        [TestMethod]
        public void WeekIntervalScheduleTest3()
        {
            var builder = new ScheduleBuilder();

            var schedule = builder
                .DuringMonth(WeekInterval.First | WeekInterval.Third | WeekInterval.Last)
                .OnDaysOfWeek(DayInterval.Sat)
                .HavingFrequency(FrequencyType.Monthly)
                .Create();

            var date1 = new DateTime(2035, 1, 6);
            var date2 = new DateTime(2035, 1, 20);
            var date3 = new DateTime(2035, 1, 27);
            var date4 = new DateTime(2035, 3, 3);
            var date5 = new DateTime(2035, 3, 17);
            var date6 = new DateTime(2035, 3, 31);

            Assert.IsTrue(schedule.IsOccurring(date1));
            Assert.IsTrue(schedule.IsOccurring(date2));
            Assert.IsTrue(schedule.IsOccurring(date3));
            Assert.IsTrue(schedule.IsOccurring(date4));
            Assert.IsTrue(schedule.IsOccurring(date5));
            Assert.IsTrue(schedule.IsOccurring(date6));

            var date100 = new DateTime(2035, 1, 13);
            var date101 = new DateTime(2035, 2, 10);
            var date102 = new DateTime(2035, 3, 24);
            var date103 = new DateTime(2035, 4, 14);
            var date104 = new DateTime(2035, 5, 12);
            var date105 = new DateTime(2035, 6, 23);

            Assert.IsFalse(schedule.IsOccurring(date100));
            Assert.IsFalse(schedule.IsOccurring(date101));
            Assert.IsFalse(schedule.IsOccurring(date102));
            Assert.IsFalse(schedule.IsOccurring(date103));
            Assert.IsFalse(schedule.IsOccurring(date104));
            Assert.IsFalse(schedule.IsOccurring(date105));
        }

        [TestMethod]
        public void WeekIntervalScheduleTest4()
        {
            var builder = new ScheduleBuilder();

            // first and last Mon, Wed, and Fri of every month
            var schedule = builder
                .DuringMonth(WeekInterval.First | WeekInterval.Last)
                .OnDaysOfWeek(DayInterval.Mon | DayInterval.Wed | DayInterval.Fri)
                .HavingFrequency(FrequencyType.Monthly)
                .Create();

            var dayIntervalValue = schedule.DayIntervalValue;
            var weekIntervalValue = schedule.WeeklyIntervalValue;
            var frequencyValue = schedule.FrequencyTypeValue;

            // TODO: Persist these values to a database

            // rehydrate the schedule later
            var rehydratedSchedule = builder
                .DuringMonth(weekIntervalValue)
                .OnDaysOfWeek(dayIntervalValue)
                .HavingFrequency(frequencyValue)
                .Create();


        }
    }
}
