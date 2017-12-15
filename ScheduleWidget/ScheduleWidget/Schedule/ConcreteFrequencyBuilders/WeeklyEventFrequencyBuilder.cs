using System;
using ScheduleWidget.TemporalExpressions.Base;
using ScheduleWidget.Common;
using ScheduleWidget.TemporalExpressions;

namespace ScheduleWidget.Schedule.ConcreteFrequencyBuilders
{
    public class WeeklyEventFrequencyBuilder : IEventFrequencyBuilder
    {
        public TemporalExpressionUnion Create(ISchedule schedule)
        {
            if (schedule.DayInterval == DayInterval.None)
                throw new ArgumentException("DayInterval must be set for schedules with a weekly frequency.");

            var union = new TemporalExpressionUnion();
            var daysOfWeek = union.GetFlags(schedule.DayInterval);
            foreach(var day in daysOfWeek)
            {
                var dayOfWeek = new ScheduleDayOfWeek((DayInterval)day);
                union.Add(dayOfWeek);
            }

            return union;
        }
    }
}
