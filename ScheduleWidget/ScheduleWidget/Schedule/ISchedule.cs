using ScheduleWidget.Common;
using System;
using System.Collections.Generic;
using ScheduleWidget.TemporalExpressions;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.Schedule
{
    public interface ISchedule
    {
        FrequencyType FrequencyType { get; }

        int FrequencyTypeValue { get; }
        int DayOfMonth { get; }

        DayInterval DayInterval { get; }

        int DayIntervalValue { get; }

        WeekInterval WeekInterval { get; }

        int WeeklyIntervalValue { get; }

        QuarterInterval QuarterInterval { get; }

        int QuarterlyIntervalValue { get; }

        MonthOfQuarterInterval MonthOfQuarterInterval { get; }
        int MonthOfQuarterIntervalValue { get; }

        ScheduleRangeInYear RangeInYear { get; }

        ScheduleAnnual Annual { get; }

        TemporalExpressionUnion ExcludedDates { get; }

        bool IsOccurring(DateTime aDate);

        DateTime? PreviousOccurrence(DateTime aDate);

        DateTime? PreviousOccurrence(DateTime aDate, DateRange during);

        DateTime? NextOccurrence(DateTime aDate);

        DateTime? NextOccurrence(DateTime aDate, DateRange during);

        IEnumerable<DateTime> Occurrences();

        IEnumerable<DateTime> Occurrences(DateRange during);
    }
}
