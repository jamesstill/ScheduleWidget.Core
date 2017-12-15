using System;
using ScheduleWidget.Common;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.TemporalExpressions
{
    /// <summary>
    /// Expression for day in month. Implements support for temporal expressions of
    /// the form: "last Friday of the month" or "first Tuesday of the month".
    /// </summary>
    public class ScheduleDayInMonth : TemporalExpression
    {
        private readonly DayInterval _dayInterval;
        private readonly WeekInterval _weekInterval;

        /// <summary>
        /// Creates a temporaral expression using day(s) of the week as well as a WeekOfMonth.
        /// 
        /// var example1 = new ScheduleDayInMonth(DayInterval.Sunday, WeekInterval.First);
        /// var example2 = new ScheduleDayInMonth(DayInterval.Tuesday, WeekInterval.Second);
        /// var example3 = new ScheduleDayInMonth(DayInterval.Friday, WeekInterval.Last);
        /// 
        /// </summary>
        /// <param name="dayInterval">day of week</param>
        /// <param name="weekInterval">week interval</param>
        public ScheduleDayInMonth(DayInterval dayInterval, WeekInterval weekInterval)
        {
            _dayInterval = dayInterval;
            _weekInterval = weekInterval;
        }

        public override bool Includes(DateTime aDate)
        {
            return DayMatches(aDate) && WeekMatches(aDate);
        }

        private bool DayMatches(DateTime aDate)
        {
            var interval = aDate.ToDayInterval();
            return (_dayInterval & interval) != DayInterval.None;
        }

        private bool WeekMatches(DateTime aDate)
        {
            var weeklyIntervals = GetFlags(_weekInterval);
            foreach (var week in weeklyIntervals)
            {
                var interval = (WeekInterval) week;
                if (interval == WeekInterval.None)
                {
                    continue;
                }

                var  n = aDate.ToNthFromWeekInterval(interval);
                var nthDate = aDate.NthOccurrenceInMonth(n);
                if (nthDate.Equals(aDate))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
