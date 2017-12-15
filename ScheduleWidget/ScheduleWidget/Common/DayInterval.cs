using System;

namespace ScheduleWidget.Common
{
    /// <summary>
    /// The day(s) in which a schedule recurs. The enum types are bit
    /// flags so that they can be combined to model constructs like
    /// var d = DayInterval.Mon | DayInterval.Wed | DayInterval.Fri
    /// which is to say "every Mon, Wed, and Fri."
    /// </summary>
    [Flags]
    public enum DayInterval
    {
        None = 0,
        Sun = 1,
        Mon = 2,
        Tue = 4,
        Wed = 8,
        Thu = 16,
        Fri = 32,
        Sat = 64,
        All = Sun | Mon | Tue | Wed | Thu | Fri | Sat
    }
}
