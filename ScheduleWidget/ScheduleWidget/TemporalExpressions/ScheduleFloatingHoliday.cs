using ScheduleWidget.Common;
using System;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.TemporalExpressions
{
    /// <summary>
    /// A floating holiday is one where the month never changes from year to year but the day does,
    /// e.g., the first Monday of September (U.S. Labor Day). So Sep = 9, Mon = 1, and first week = 1
    /// Note that this is different from a "moveable feast" like Easter in the Christian tradition 
    /// where both the month and day change relative to the first full moon on or after 21 March.
    /// </summary>
    public class ScheduleFloatingHoliday : TemporalExpression
    {
        private readonly MonthOfYear _monthOfYear;
        private readonly DayOfWeek _dayOfWeek;
        private readonly WeekInterval _weekInterval;

        /// <summary>
        /// The holiday month, day, and count where the holiday falls on a different
        /// date every year, e.g., first Monday of September (Labor Day):
        /// 
        /// var laborDay = new ScheduleFloatingHoliday(MonthOfYear.Sep, DayOfWeek.Monday, WeekInterval.First);
        /// </summary>
        /// <param name="monthOfYear"></param>
        /// <param name="dayOfWeek">Sun through Sat</param>
        /// <param name="weekInterval">First, Second, etc.</param>
        public ScheduleFloatingHoliday(MonthOfYear monthOfYear, DayOfWeek dayOfWeek, WeekInterval weekInterval)
        {
            _monthOfYear = monthOfYear;
            _dayOfWeek = dayOfWeek;
            _weekInterval = weekInterval;
        }

        /// <summary>
        /// Returns true if the date falls on the floating holiday
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public override bool Includes(DateTime aDate)
        {
            return MonthMatches(aDate) && DayMatches(aDate) && WeekMatches(aDate);
        }

        private bool MonthMatches(DateTime aDate)
        {
            return aDate.Month == (int)_monthOfYear;
        }

        private bool WeekMatches(DateTime aDate)
        {
            var n = aDate.ToNthFromWeekInterval(_weekInterval);
            var nthDate = aDate.NthOccurrenceInMonth(n);
            return nthDate.Equals(aDate);
        }

        private bool DayMatches(DateTime aDate)
        {
            return aDate.DayOfWeek == _dayOfWeek;
        }
    }
}
