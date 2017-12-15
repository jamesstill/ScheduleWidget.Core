using System;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.Schedule.ConcreteFrequencyBuilders
{
    public class AnnualEventFrequencyBuilder : IEventFrequencyBuilder
    {
        public TemporalExpressionUnion Create(ISchedule schedule)
        {
            if (schedule.Annual == null)
                throw new ArgumentException("ScheduleAnnual must be set for schedules with an annual frequency.");

            var annual = schedule.Annual;
            var union = new TemporalExpressionUnion();
            union.Add(annual);
            return union;
        }
    }
}
