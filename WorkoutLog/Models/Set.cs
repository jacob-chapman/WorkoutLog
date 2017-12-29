using System;
using SQLite;

namespace WorkoutLog.Models
{
    public class Set
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Indexed]
        public int ExerciseId { get; set; }

        public int? Reps { get; set; }

        public double? Weight { get; set; }

        public double? Time { get; set; }
    }
}
