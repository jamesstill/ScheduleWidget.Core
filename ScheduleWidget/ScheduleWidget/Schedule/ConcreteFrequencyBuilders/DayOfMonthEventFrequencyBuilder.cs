using ScheduleWidget.TemporalExpressions;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.Schedule.ConcreteFrequencyBuilders
{
    public class DayOfMonthEventFrequencyBuilder : IEventFrequencyBuilder
    {
        public TemporalExpressionUnion Create(ISchedule schedule)
        {
            var union = new TemporalExpressionUnion();
            var dayOfMonth = new ScheduleDayOfMonth(schedule.DayOfMonth);
            union.Add(dayOfMonth);
            return union;
        }
    }
}
