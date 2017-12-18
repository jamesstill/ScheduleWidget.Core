# ScheduleWidget

## Scheduling engine to create recurring events for calendars

ScheduleWidget is a scheduling engine that creates recurring events for calendar. 
It is an implementation based on Martin Fowler's white paper [Recurring Events for Calendars](https://martinfowler.com/apsupp/recurring.pdf) 
in which he describes the software design. This is a complete .NET Core 2.0 rewrite of the popular .NET Framework version. 
Suppose we want to create a schedule that describes the first Monday of every month:

```var builder = new ScheduleBuilder();

var schedule = builder
    .DuringMonth(WeekInterval.First)
    .OnDaysOfWeek(DayInterval.Mon)
    .HavingFrequency(FrequencyType.MonthlyByDayInMonth)
    .Create();
```
Once we create a `Schedule` we can ask it questions. The `Schedule` exposes methods (documented in more detail below) 
that return *schedule occurrences* (concrete `DateTime` value objects) over a defined period of time. Suppose `DateTime.Today` 
is 20 December 2030:

```var during = new DateRange(DateTime.Today, DateTime.Today.AddMonths(6));

foreach (var date in schedule.Occurrences(during))
{
    Debug.WriteLine(date.ToShortDateString());
}

Output CultureInfo("en-US"):

1/6/2031
2/3/2031
3/3/2031
4/7/2031
5/5/2031
6/2/2031
```

That's all there is to it. Visit the [Project Portal](http://schedulewidget.azurewebsites.net/) for download 
instructions, detailed documentation, and code samples.
