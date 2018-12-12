using ScheduleWidget.Common;
using ScheduleWidget.TemporalExpressions;
using ScheduleWidget.TemporalExpressions.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleWidget.Schedule
{
    public class Schedule : ISchedule
    {
        private TemporalExpression _expression;

        public FrequencyType FrequencyType { get; private set; }

        public int FrequencyTypeValue => (int) FrequencyType;

        public int DayOfMonth { get; private set; }
        
        public DayInterval DayInterval { get; private set; }

        public int DayIntervalValue => (int) DayInterval;

        public WeekInterval WeekInterval { get; private set; }

        public int WeeklyIntervalValue => (int) WeekInterval;

        public QuarterInterval QuarterInterval { get; private set; }

        public int QuarterlyIntervalValue => (int) QuarterInterval;

        public MonthOfQuarterInterval MonthOfQuarterInterval { get; private set; }

        public int MonthOfQuarterIntervalValue => (int) MonthOfQuarterInterval;

        public ScheduleRangeInYear RangeInYear { get; private set; }

        public ScheduleAnnual Annual { get; private set; }

        public TemporalExpressionUnion ExcludedDates { get; private set; }

        public ScheduleDayOfMonth Monthly { get; private set; }

        public void SetFrequencyType(FrequencyType type)
        {
            FrequencyType = type;
        }

        public void SetDaysOfWeek(DayInterval interval)
        {
            DayInterval = interval;
        }

        public void SetDayOfMonth(int dayValue)
        {
            DayOfMonth = dayValue;
        }

        public void SetMonthlyInterval(WeekInterval interval)
        {
            WeekInterval = interval;
        }

        public void SetQuarterlyInterval(QuarterInterval interval)
        {
            QuarterInterval = interval;
        }

        public void SetMonthOfQuarterInterval(MonthOfQuarterInterval interval)
        {
            MonthOfQuarterInterval = interval;
        }

        public void SetRangeInYear(ScheduleRangeInYear range)
        {
            RangeInYear = range;
        }

        public void SetAnniversary(ScheduleAnnual annual)
        {
            Annual = annual;
        }

        public void SetMonthly(ScheduleDayOfMonth monthly)
        {
            Monthly = monthly;
        }

        public void SetExcludedDates(TemporalExpressionUnion union)
        {
            ExcludedDates = union;
        }
    
        public void SetSchedule(TemporalExpression expression)
        {
            _expression = expression;
        }

        /// <summary>
        /// Returns true if the passed in date is an occurrence in the schedule.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public bool IsOccurring(DateTime aDate)
        {
            return _expression.Includes(aDate);
        }

        /// <summary>
        /// Returns the next occurrence in the schedule from the passed in date
        /// up to a default of one year from now. If there is no occurrence then
        /// the data will be null.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public DateTime? NextOccurrence(DateTime aDate)
        {
            var during = new DateRange(aDate, DateTime.Today.AddYears(1));
            return NextOccurrence(aDate, during);
        }

        /// <summary>
        /// Returns the next occurrence in the schedule from the passed in date
        /// over the duration of the passed in date range. If there is no occurrence
        /// then the date will be null.
        /// </summary>
        /// <param name="aDate"></param>
        /// <param name="during"></param>
        /// <returns></returns>
        public DateTime? NextOccurrence(DateTime aDate, DateRange during)
        {
            var dates = Occurrences(during);
            return dates.SkipWhile(o => o.Date <= aDate.Date).FirstOrDefault();
        }

        /// <summary>
        /// Returns the previous occurrence in the schedule from the passed in
        /// date up to a default of one year ago from today. If there is no
        /// occurrence then the date will be null.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public DateTime? PreviousOccurrence(DateTime aDate)
        {
            var during = new DateRange(DateTime.Today.AddYears(-1), aDate);
            return PreviousOccurrence(aDate, during);
        }
        
        /// <summary>
        /// Returns the previous occurrence in the schedule from the passed in
        /// date over the duration of the passed in date range. If there is no
        /// occurrence then the date will be null.
        /// </summary>
        /// <param name="aDate"></param>
        /// <param name="during"></param>
        /// <returns></returns>
        public DateTime? PreviousOccurrence(DateTime aDate, DateRange during)
        {
            var dates = Occurrences(during).OrderByDescending(o => o.Date);
            return dates.SkipWhile(o => o >= aDate.Date).FirstOrDefault();
        }

        /// <summary>
        /// Returns a list of all dates in the schedule that occur during a default
        /// two-year period of time from one year ago to one year from now.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DateTime> Occurrences()
        {
            return EachDay(DateTime.Today.AddYears(-1), DateTime.Today.AddYears(1)).Where(IsOccurring);
        }

        /// <summary>
        /// Returns a list of all dates in the schedule that occur during the passed in 
        /// duration of time.
        /// </summary>
        /// <param name="during"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> Occurrences(DateRange during)
        {
            return EachDay(during.StartDateTime, during.EndDateTime).Where(IsOccurring);
        }

        /// <summary>
        /// Return each calendar day in the date range in ascending order
        /// </summary>
        /// <param name="from"></param>
        /// <param name="through"></param>
        /// <returns></returns>
        private static IEnumerable<DateTime> EachDay(DateTime from, DateTime through)
        {
            for (var day = from.Date; day.Date <= through.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
