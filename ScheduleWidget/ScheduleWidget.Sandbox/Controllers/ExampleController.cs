using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScheduleWidget.Common;
using ScheduleWidget.Sandbox.Models;
using ScheduleWidget.Sandbox.Repository;
using ScheduleWidget.Schedule;

namespace ScheduleWidget.Sandbox.Controllers
{
    public class ExampleController : Controller
    {
        private const string CookieName = "ScheduleWidget";

        public IActionResult Index()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                CreateUserCookie();
                userId = GetUserId();
            }
            return View();
        }

        public ActionResult CreateSchedule(DateTime eventDate)
        {
            var userId = GetUserId();
            var scheduleViewModel  = new ScheduleViewModel()
            {
                UserId = userId,
                StartDate = eventDate,
                StartTime = new TimeSpan(DateTime.Now.Hour, 0, 0),
                EndTime = new TimeSpan(DateTime.Now.Hour + 1, 0, 0)
            };

            LoadViewBag();
            return View(scheduleViewModel);
        }

        [HttpPost]
        public ActionResult CreateSchedule(ScheduleViewModel scheduleViewModel)
        {
            var repository = new ScheduleRepository();
            repository.Save(scheduleViewModel);
            return RedirectToAction("Index", "Example");
        }

        public ActionResult ScheduleOccurrence(int id, DateTime occurrenceDate)
        {
            var scheduleOccurrence = new ScheduleOccurrenceViewModel()
            {
                ScheduleID = id,
                OccurrenceDate = occurrenceDate
            };

            return View(scheduleOccurrence);
        }

        public JsonResult GetSchedules(DateTime start, DateTime end)
        {
            var userId = GetUserId();
            var calendarRange = new DateRange(start, end);
            var calendarObjects = new List<object>();
            var repository = new ScheduleRepository();
            var schedules = repository.GetAllSchedules(userId);
            foreach (var schedule in schedules)
            {
                calendarObjects.AddRange(BuildCalendarObjects(schedule, calendarRange));
            }

            return Json(calendarObjects.ToArray());
        }

        [HttpPost]
        public ActionResult RemoveCookie()
        {
            Response.Cookies.Delete(CookieName);
            return RedirectToAction("Index");
        }

        private void LoadViewBag()
        {
            LoadFrequencyChoices();
            LoadDaysOfWeekChoices();
        }

        private void LoadFrequencyChoices()
        {
            var list = new List<object>()
            {
                new { ID = 1, Name = "Daily" },
                new { ID = 2, Name = "Weekly" },
                new { ID = 4, Name = "Monthly" }
            };

            ViewBag.FrequencyChoices = new SelectList(list, "ID", "Name");
        }

        private void LoadDaysOfWeekChoices()
        {
            var daysOfWeek = new List<object>()
            {
                new { Name = "Sat" },
                new { Name = "Sun" },
                new { Name = "Mon" },
                new { Name = "Tue" },
                new { Name = "Wed" },
                new { Name = "Thu" },
                new { Name = "Fri" }
            };

            ViewBag.DaysOfWeekChoices = new SelectList(daysOfWeek, "Name", "Name");
        }

        private string GetUserId()
        {
            return Request.Cookies[CookieName];
        }

        private void CreateUserCookie()
        {
            var userId = Guid.NewGuid().ToString("N");
            var cookie = new CookieOptions { Expires = DateTime.Now.AddHours(1) };
            Response.Cookies.Append(CookieName, userId, cookie);
        }

        private List<object> BuildCalendarObjects(ScheduleViewModel scheduleViewModel, DateRange calendarRange)
        {
            var calendarObjects = new List<object>();

            // if frequency is none then this is a one-time only event
            if (scheduleViewModel.FrequencyTypeOptions.Equals(FrequencyType.None))
            {
                return BuildCalendarObjects(scheduleViewModel);
            }

            // use the ScheduleWidget engine to calculate the dates in the schedule
            var builder = new ScheduleBuilder();

            var schedule = builder
                .OnDaysOfWeek(scheduleViewModel.Days)
                .DuringMonth(scheduleViewModel.Weeks)
                .HavingFrequency(scheduleViewModel.Frequency)
                .Create();

            // use the calendar start date or the schedule start date whichever is later
            var start = (scheduleViewModel.StartDate > calendarRange.StartDateTime)
                ? scheduleViewModel.StartDate
                : calendarRange.StartDateTime;

            // use the calendar end date or the schedule end date whichever is earlier
            var end = (scheduleViewModel.EndDate < calendarRange.EndDateTime)
                ? scheduleViewModel.EndDate.Value
                : calendarRange.EndDateTime;

            var scheduleDateRange = new DateRange(start, end);

            // let ScheduleWidget calculate the schedule dates for us and
            // add each date to the payload for the javascript calendar
            foreach (var date in schedule.Occurrences(scheduleDateRange))
            {
                calendarObjects.Add(new
                {
                    id = scheduleViewModel.ID,
                    title = scheduleViewModel.Title,
                    start = date + scheduleViewModel.StartTime,
                    end = date + scheduleViewModel.EndTime,
                    url = Url.Action("ScheduleOccurrence", "Example", new { id = scheduleViewModel.ID, occurrenceDate = date }),
                    allDay = false
                });
            }

            return calendarObjects;
        }

        private List<object> BuildCalendarObjects(ScheduleViewModel scheduleViewModel)
        {
            var calendarObjects = new List<object>
            {
                new
                {
                    id = scheduleViewModel.ID,
                    title = scheduleViewModel.Title,
                    start = scheduleViewModel.StartDate + scheduleViewModel.StartTime,
                    end = scheduleViewModel.StartDate + scheduleViewModel.EndTime,
                    url = Url.Action("ScheduleOccurrence", "Example", new {id = scheduleViewModel.ID, occurrenceDate = scheduleViewModel.StartDate}),
                    allDay = false
                }
            };

            return calendarObjects;
        }
    }
}

