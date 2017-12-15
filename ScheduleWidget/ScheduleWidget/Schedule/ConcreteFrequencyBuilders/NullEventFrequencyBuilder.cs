using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.Schedule.ConcreteFrequencyBuilders
{
    public class NullEventFrequencyBuilder : IEventFrequencyBuilder
    {
        public TemporalExpressionUnion Create(ISchedule schedule)
        {
            // no expressions
            return new TemporalExpressionUnion();
        }
    }
}
