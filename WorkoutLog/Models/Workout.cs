using System;
using System.Collections.Generic;
using SQLite;

namespace WorkoutLog.Models
{
    public class Workout
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Indexed]
        public List<int> SetIds { get; set; }

        public string Title { get; set; }

        public string Notes { get; set; }

        public DateTime WorkoutDate { get; set; }
    }
}
