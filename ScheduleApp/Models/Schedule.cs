using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScheduleApp.Models
{
    public class Schedule
    {
        public List<Day> Days { get; set; }
    }

    public class Day
    {
        public string Type { get; set; }
        public List<Lesson> Lessons { get; set; }
    }

    public class Lesson
    {
        public string Title { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Classroom { get; set; }
        public string Teacher { get; set; }
    }
}