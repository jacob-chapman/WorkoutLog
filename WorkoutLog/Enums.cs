using System;
using System.Collections.Generic;
namespace WorkoutLog
{
    public enum ExerciseType
    {
        Barbell = 0,
        Dumbbell = 1,
        Machine = 2,
        Body = 3,
        Other = 4,
        None = 5
    }

    public static class WorkoutLogExtensions
    {
        //public static List<> GetEnumAsList 
    }

    public enum MuscleGroup
    {
        Chest = 0,
        Shoulders = 1,
        Back = 2,
        Core = 3,
        Cardio = 4,
        Legs = 5,
        Biceps = 6,
        Triceps = 7,
        Traps = 8,
        None = 9
    }

    public enum IWorkoutItemType
    {
        AddWorkout = 0,
        WorkoutItem = 1
    }

    public enum IWorkoutSessionItemType
    {
        AddExercise,
        SetsItem,
        FinishWorkout,
        SetsHeader,
        SetItem,
        AddSet
    }
}
