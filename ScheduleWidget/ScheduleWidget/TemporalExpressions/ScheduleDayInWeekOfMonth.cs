using System;
using ScheduleWidget.Common;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.TemporalExpressions
{
    /// <summary>
    /// Expression for day in week number of month. Implements support for temporal 
    /// expressions of the form: "every Friday in the first week of the month" or 
    /// every "Mon, Wed, and Fri of the first and third weeks of the month". This is
    /// a subtle difference from expressions in ScheduleDayInMonth that are of the 
    /// form "first and third Mon of the month."
    /// </summary>
    public class ScheduleDayInWeekOfMonth : TemporalExpression
    {
        private readonly DayInterval _dayInterval;
        private readonly WeekInterval _weekInterval;

        /// <summary>
        /// Creates a temporaral expression using day(s) of the week as well as a WeekOfMonth.
        /// 
        /// var example1 = new ScheduleDayInWeekOfMonth(DayInterval.Sunday, WeekInterval.First);
        /// var example2 = new ScheduleDayInWeekOfMonth(DayInterval.Tuesday, WeekInterval.Second);
        /// var example3 = new ScheduleDayInWeekOfMonth(DayInterval.Friday, WeekInterval.Last);
        /// 
        /// </summary>
        /// <param name="dayInterval">day of week</param>
        /// <param name="weekInterval">week interval</param>
        public ScheduleDayInWeekOfMonth(DayInterval dayInterval, WeekInterval weekInterval)
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
                var nthDate = aDate.OccurrenceNthWeekInMonth(n);
                if (nthDate.Equals(aDate))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
