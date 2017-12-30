using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace WorkoutLog.Models
{
    public class Set
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [OneToOne]
        public Exercise Exercise { get; set; }

        public int? Reps { get; set; }

        public double? Weight { get; set; }

        public double? Time { get; set; }
    }
}
