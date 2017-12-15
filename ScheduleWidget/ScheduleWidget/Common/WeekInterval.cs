using System;

namespace ScheduleWidget.Common
{
    /// <summary>
    /// The week(s) in which a schedule recurs
    /// </summary>
    [Flags]
    public enum WeekInterval
    {
        None = 0,
        First = 1,
        Second = 2,
        Third = 4,
        Fourth = 8,
        Last = 16
    }
}
