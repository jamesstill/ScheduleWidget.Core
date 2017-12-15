using System;
using ScheduleWidget.Common;
using ScheduleWidget.TemporalExpressions;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.Schedule.ConcreteFrequencyBuilders
{
    public class QuarterlyEventFrequencyBuilder : IEventFrequencyBuilder
    {
        /// <summary>
        /// Given a schedule with a quarterly frequency build out a union of temporal expressions
        /// to describe schedules that include day(s), week(s), and month(s) within a quarter. 
        /// For example: 
        /// 
        /// (1) last Fri of the last week of the last month in the 1st and 3rd quarters.
        /// (2) first Mon of every week of the first month in every quarter.
        /// </summary>
        /// <returns></returns>
        public TemporalExpressionUnion Create(ISchedule schedule)
        {
            if (schedule.QuarterInterval == QuarterInterval.None)
                throw new ArgumentException("QuarterInterval must be set for schedules with a quarterly frequency.");

            if (schedule.MonthOfQuarterInterval == MonthOfQuarterInterval.None)
                throw new ArgumentException("MonthOfQuarterInterval must be set for schedules with a quarterly frequency.");

            if (schedule.WeekInterval == WeekInterval.None)
                throw new ArgumentException("WeekInterval must be set for schedules with a quarterly frequency.");

            if (schedule.DayInterval == DayInterval.None)
                throw new ArgumentException("DayInterval must be set for schedules with a quarterly frequency.");

            var union = new TemporalExpressionUnion();
            var quarterlyIntervals = union.GetFlags(schedule.QuarterInterval);
            foreach (var quarter in quarterlyIntervals)
            {
                var monthOfQuarterIntervals = union.GetFlags(schedule.MonthOfQuarterInterval);
                foreach (var monthOfQuarter in monthOfQuarterIntervals)
                {
                    var weeklyIntervals = union.GetFlags(schedule.WeekInterval);
                    foreach (var week in weeklyIntervals)
                    {
                        var daysOfWeek = union.GetFlags(schedule.DayInterval);
                        foreach (var day in daysOfWeek)
                        {
                            var dayInQuarter = new ScheduleDayInQuarter(
                                (DayInterval)day,
                                (WeekInterval)week,
                                (QuarterInterval)quarter,
                                (MonthOfQuarterInterval)monthOfQuarter
                            );

                            union.Add(dayInQuarter);
                        }
                    }
                }
            }

            return union;
        }
    }
}
