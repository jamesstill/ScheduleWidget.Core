using System;
using System.ComponentModel.DataAnnotations;

namespace ScheduleWidget.Sandbox.Models
{
    public class ScheduleOccurrenceViewModel
    {
        public int ScheduleID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OccurrenceDate { get; set; }
    }
}
