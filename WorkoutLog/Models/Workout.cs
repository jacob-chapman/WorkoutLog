using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace WorkoutLog.Models
{
    public class Workout
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [OneToMany]
        public List<Set> Sets { get; set; }

        public string Title { get; set; }

        public string Notes { get; set; }

        public DateTime WorkoutDate { get; set; }
    }
}
