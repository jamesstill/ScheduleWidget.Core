using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.Schedule
{
    public interface IEventFrequencyBuilder
    {
        TemporalExpressionUnion Create(ISchedule schedule);
    }
}
