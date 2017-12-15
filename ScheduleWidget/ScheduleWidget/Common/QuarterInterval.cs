using System;

namespace ScheduleWidget.Common
{
    /// <summary>
    /// The quarter(s) in which a schedule recurs.
    /// </summary>
    [Flags]
    public enum QuarterInterval
    {
        None = 0,
        First = 1,
        Second = 2,
        Third = 4,
        Fourth = 8,
        All = First | Second | Third | Fourth
    }
}
