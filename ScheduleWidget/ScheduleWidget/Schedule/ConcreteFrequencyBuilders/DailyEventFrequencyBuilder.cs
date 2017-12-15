using System;
using ScheduleWidget.TemporalExpressions.Base;
using ScheduleWidget.Common;
using ScheduleWidget.TemporalExpressions;

namespace ScheduleWidget.Schedule.ConcreteFrequencyBuilders
{
    public class DailyEventFrequencyBuilder : IEventFrequencyBuilder
    {
        public TemporalExpressionUnion Create(ISchedule schedule)
        {
            var union = new TemporalExpressionUnion();
            foreach (DayInterval day in Enum.GetValues(typeof(DayInterval)))
            {
                union.Add(new ScheduleDayOfWeek(day));
            }

            return union;
        }
    }
}
