using System;
using System.ComponentModel.DataAnnotations;
using ScheduleWidget.Common;
using ScheduleWidget.Schedule;

namespace ScheduleWidget.Sandbox.Models
{
    public enum RecurrencePattern
    {
        OneTime,
        Repeat
    };

    public class ScheduleViewModel
    {
        private int _frequencyChoice;
        private DateTime? _endDate;

        public int ID { get; set; }

        [Required(ErrorMessage = @"Please provide a title.")]
        [StringLength(50)]
        public string  Title { get; set; }

        public string UserId { get; set; }

        public RecurrencePattern ScheduleRecurrence { get; set; }

        [Display(Name = "Schedule")]
        public int FrequencyChoice
        {
            get => _frequencyChoice;
            set
            {
                _frequencyChoice = value;
                CalculateFrequency();
                CalculateRecurrencePattern();
            }
        }

        public int Frequency { get; set; }

        [Display(Name = "Days Of Week")]
        public int Days { get; set; }

        public int Weeks { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = @"Please provide a start date.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public DateTime StartDateTime
        {
            get => StartDate + StartTime;
            set
            {
                StartDate = value.Date;
                StartTime = value.TimeOfDay;
            }
        }

        public DateTime? EndDateTime
        {
            get
            {
                if (Frequency == 0) // one-time only 
                    return (StartDate + EndTime);

                return _endDate;
            }
            set
            {
                _endDate = value;

                var ts = (EndDateTime - StartDate);

                if (ts?.Days == 0)
                    Frequency = 0;
            }
        }

        [Display(Name = "Start Time")]
        [Required(ErrorMessage = @"Please provide a start time.")]
        [RegularExpression(@"(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})", ErrorMessage = @"Please provide a valid time.")]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End Time")]
        [Required(ErrorMessage = @"Please provide an end time.")]
        [RegularExpression(@"(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})", ErrorMessage = @"Please provide a valid time.")]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public bool IsSundaySelected
        {
            get => DayIntervalOptions.HasFlag(DayInterval.Sun);
            set
            {
                if (!value) return;

                if (!DayIntervalOptions.HasFlag(DayInterval.Sun))
                {
                    DayIntervalOptions |= DayInterval.Sun;
                }
            }
        }

        public bool IsMondaySelected
        {
            get => DayIntervalOptions.HasFlag(DayInterval.Mon);
            set
            {
                if (!value) return;

                if (!DayIntervalOptions.HasFlag(DayInterval.Mon))
                {
                    DayIntervalOptions |= DayInterval.Mon;
                }
            }
        }

        public bool IsTuesdaySelected
        {
            get => DayIntervalOptions.HasFlag(DayInterval.Tue);
            set
            {
                if (!value) return;

                if (!DayIntervalOptions.HasFlag(DayInterval.Tue))
                {
                    DayIntervalOptions |= DayInterval.Tue;
                }
            }
        }

        public bool IsWednesdaySelected
        {
            get => DayIntervalOptions.HasFlag(DayInterval.Wed);
            set
            {
                if (!value) return;

                if (!DayIntervalOptions.HasFlag(DayInterval.Wed))
                {
                    DayIntervalOptions |= DayInterval.Wed;
                }
            }
        }

        public bool IsThursdaySelected
        {
            get => DayIntervalOptions.HasFlag(DayInterval.Thu);
            set
            {
                if (!value) return;

                if (!DayIntervalOptions.HasFlag(DayInterval.Thu))
                {
                    DayIntervalOptions |= DayInterval.Thu;
                }
            }
        }

        public bool IsFridaySelected
        {
            get => DayIntervalOptions.HasFlag(DayInterval.Fri);
            set
            {
                if (!value) return;

                if (!DayIntervalOptions.HasFlag(DayInterval.Fri))
                {
                    DayIntervalOptions |= DayInterval.Fri;
                }
            }
        }

        public bool IsSaturdaySelected
        {
            get => DayIntervalOptions.HasFlag(DayInterval.Sat);
            set
            {
                if (!value) return;

                if (!DayIntervalOptions.HasFlag(DayInterval.Sat))
                {
                    DayIntervalOptions |= DayInterval.Sat;
                }
            }
        }

        public bool IsFirstWeekOfMonthSelected
        {
            get => WeekIntervalOptions.HasFlag(WeekInterval.First);
            set
            {
                if (!value) return;

                if (!WeekIntervalOptions.HasFlag(WeekInterval.First))
                {
                    WeekIntervalOptions |= WeekInterval.First;
                }
            }
        }

        public bool IsSecondWeekOfMonthSelected
        {
            get => WeekIntervalOptions.HasFlag(WeekInterval.Second);
            set
            {
                if (!value) return;

                if (!WeekIntervalOptions.HasFlag(WeekInterval.Second))
                {
                    WeekIntervalOptions |= WeekInterval.Second;
                }
            }
        }

        public bool IsThirdWeekOfMonthSelected
        {
            get => WeekIntervalOptions.HasFlag(WeekInterval.Third);
            set
            {
                if (!value) return;

                if (!WeekIntervalOptions.HasFlag(WeekInterval.Third))
                {
                    WeekIntervalOptions |= WeekInterval.Third;
                }
            }
        }

        public bool IsFourthWeekOfMonthSelected
        {
            get => WeekIntervalOptions.HasFlag(WeekInterval.Fourth);
            set
            {
                if (!value) return;

                if (!WeekIntervalOptions.HasFlag(WeekInterval.Fourth))
                {
                    WeekIntervalOptions |= WeekInterval.Fourth;
                }
            }
        }

        public bool IsLastWeekOfMonthSelected
        {
            get => WeekIntervalOptions.HasFlag(WeekInterval.Last);
            set
            {
                if (!value) return;

                if (!WeekIntervalOptions.HasFlag(WeekInterval.Last))
                {
                    WeekIntervalOptions |= WeekInterval.Last;
                }
            }
        }

        public FrequencyType FrequencyTypeOptions
        {
            get => (FrequencyType)Frequency;
            set => Frequency = (int)value;
        }

        public WeekInterval WeekIntervalOptions
        {
            get => (WeekInterval)Weeks;
            set => Weeks = (int)value;
        }

        public DayInterval DayIntervalOptions
        {
            get => (DayInterval)Days;
            set => Days = (int)value;
        }

        public ISchedule BuildSchedule()
        {
            var builder = new ScheduleBuilder();
            return builder
                .DuringMonth(Weeks)
                .OnDaysOfWeek(Days)
                .HavingFrequency(Frequency)
                .Create();
        } 

        private void CalculateFrequency()
        {
            switch (_frequencyChoice)
            {
                case 1:
                    Frequency = (int) FrequencyType.Daily;
                    break;

                case 2:
                    Frequency = (int) FrequencyType.Weekly;
                    break;

                case 3:
                    Frequency = (int)FrequencyType.MonthlyByDayOfMonth;
                    break;

                case 4:
                    Frequency = (int)FrequencyType.MonthlyByDayInMonth;
                    break;

                case 5:
                    Frequency = (int)FrequencyType.MonthlyByDayInWeekOfMonth;
                    break;

                case 6:
                    Frequency = (int)FrequencyType.Quarterly;
                    break;

                case 7:
                    Frequency = (int)FrequencyType.Yearly;
                    break;

                default:
                    Frequency = (int)FrequencyType.None; // one-time only
                    break;
            }
        }

        private void CalculateRecurrencePattern()
        {
            // determine frequency from recurrence pattern
            if (ScheduleRecurrence == RecurrencePattern.OneTime)
            {
                Frequency = 0;
                EndDate = null;
            }
            else // repeat pattern
            {
                if (FrequencyTypeOptions == FrequencyType.Daily)
                {
                    DayIntervalOptions =
                        DayInterval.Sun |
                        DayInterval.Mon |
                        DayInterval.Tue |
                        DayInterval.Wed |
                        DayInterval.Thu |
                        DayInterval.Fri |
                        DayInterval.Sat;
                }
            }
        }
    }
}
