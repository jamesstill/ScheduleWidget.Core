using System;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.TemporalExpressions
{
    /// <summary>
    /// Fixed holiday is one where the month and day never change
    /// from year to year, e.g., Jul 4 (Independence Day) in U.S.
    /// </summary>
    public class ScheduleFixedHoliday : TemporalExpression
    {
        private readonly int _month;
        private readonly int _day;

        /// <summary>
        /// The holiday month and day, e.g., "July 4 (Independence Day)":
        /// var independenceDay = new FixedHolidayTE(7, 4);
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public ScheduleFixedHoliday(int month, int day)
        {
            _month = month;
            _day = day;
        }

        /// <summary>
        /// Returns true if the date falls on this fixed holiday
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public override bool Includes(DateTime aDate)
        {
            return (aDate.Month == _month && aDate.Day == _day);
        }
    }
}
