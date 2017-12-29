using System;
using SQLite;

namespace WorkoutLog.Models
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Title { get; set; }

        public MuscleGroup MuscleGroup { get; set; }

        public ExerciseType ExerciseType { get; set; }
    }
}
