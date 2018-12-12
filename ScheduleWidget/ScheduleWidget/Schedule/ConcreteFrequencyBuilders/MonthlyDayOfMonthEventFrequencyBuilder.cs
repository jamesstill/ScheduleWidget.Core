using ScheduleWidget.TemporalExpressions.Base;
using System;

namespace ScheduleWidget.Schedule.ConcreteFrequencyBuilders
{
    public class MonthlyDayOfMonthEventFrequencyBuilder : IEventFrequencyBuilder
    {
        public TemporalExpressionUnion Create(ISchedule schedule)
        {
           if (schedule.Monthly == null)
                throw new ArgumentException("ScheduleDayOfMonth must be set for schedules with a " +
                                             "monthly by day of month frequency.");

            var monthly = schedule.Monthly;
            var union = new TemporalExpressionUnion();
            union.Add(monthly);
            return union;
        }
    }
}
