using ScheduleWidget.Common;
using ScheduleWidget.TemporalExpressions;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.Schedule
{
    public class ScheduleBuilder 
    {
        public Schedule Schedule { get; private set; }

        public ScheduleBuilder()
        {
            Schedule = new Schedule();
            OnDaysOfWeek(DayInterval.None);
            DuringMonth(WeekInterval.None);
            DuringMonthOfQuarter(MonthOfQuarterInterval.None);
            DuringQuarter(QuarterInterval.None);
            DuringYear(new ScheduleRangeInYear(1, 12));
            Excluding(new TemporalExpressionUnion());
            HavingFrequency(FrequencyType.None);
        }

        /// <summary>
        /// Set the schedule range if it occurs only during partial periods within a year.
        /// For example, the street cleaning schedule occurs from April (4) to October (10).
        /// </summary>
        /// <param name="during"></param>
        /// <returns></returns>
        public ScheduleBuilder DuringYear(ScheduleRangeInYear during)
        {
            Schedule.SetRangeInYear(during);
            return this;
        }

        public ScheduleBuilder HavingFrequency(FrequencyType type)
        {
            this.HavingFrequency((int)type);
            return this;
        }

        public ScheduleBuilder HavingFrequency(int type)
        {
            Schedule.SetFrequencyType((FrequencyType)type);
            return this;
        }

        public ScheduleBuilder OnDaysOfWeek(DayInterval interval)
        {
            this.OnDaysOfWeek((int)interval);
            return this;
        }

        public ScheduleBuilder OnDaysOfWeek(int interval)
        {
            Schedule.SetDaysOfWeek((DayInterval)interval);
            return this;
        }

        public ScheduleBuilder DuringMonth(WeekInterval interval)
        {
            this.DuringMonth((int)interval);
            return this;
        }

        public ScheduleBuilder DuringMonth(int interval)
        {
            Schedule.SetMonthlyInterval((WeekInterval)interval);
            return this;
        }

        public ScheduleBuilder DuringMonthOfQuarter(MonthOfQuarterInterval interval)
        {
            this.DuringMonthOfQuarter((int)interval);
            return this;
        }

        public ScheduleBuilder DuringMonthOfQuarter(int interval)
        {
            Schedule.SetMonthOfQuarterInterval((MonthOfQuarterInterval)interval);
            return this;
        }

        public ScheduleBuilder DuringQuarter(QuarterInterval interval)
        {
            this.DuringQuarter((int)interval);
            return this;
        }

        public ScheduleBuilder DuringQuarter(int interval)
        {
            Schedule.SetQuarterlyInterval((QuarterInterval)interval);
            return this;
        }

        public ScheduleBuilder OnAnniversary(ScheduleAnnual annual)
        {
            Schedule.SetAnniversary(annual);
            return this;
        }

        public ScheduleBuilder OnMonthly(ScheduleDayOfMonth monthly)
        {
            Schedule.SetMonthly(monthly);
            return this;
        }

        public ScheduleBuilder Excluding(TemporalExpressionUnion excludedDates)
        {
            Schedule.SetExcludedDates(excludedDates);
            return this;
        }

        public ISchedule Create()
        {
            var intersection = new TemporalExpressionIntersection();
            var frequencyBuilder = EventFrequencyBuilder.Create(Schedule.FrequencyType);
            var union = frequencyBuilder.Create(Schedule);
            intersection.Add(union);
            intersection.Add(Schedule.RangeInYear);

            var difference = new TemporalExpressionDifference(intersection, Schedule.ExcludedDates);
            Schedule.SetSchedule(difference);
            return Schedule;
        }
    }
}
