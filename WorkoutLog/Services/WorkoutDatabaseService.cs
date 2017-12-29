using System;
using WorkoutLog.Models;
namespace WorkoutLog.Services
{
    //todo database service with query filters
    public static class WorkoutDatabaseService
    {
        private static WorkoutDatabase _database;

        public static async void Initialize()
        {
            _database = new WorkoutDatabase();

            await _database.Init();

            CreateExercise();
        }

        private static async void CreateExercise()
        {
            Exercise arnoldPress = new Exercise()
            {
                ExerciseType = ExerciseType.Dumbbell,
                MuscleGroup = MuscleGroup.Shoulders,
                Title = "Arnold Press"
            };

            Exercise latRaise = new Exercise()
            {
                ExerciseType = ExerciseType.Dumbbell,
                MuscleGroup = MuscleGroup.Shoulders,
                Title = "Lateral Raise"
            };

            Exercise militaryPress = new Exercise()
            {
                ExerciseType = ExerciseType.Barbell,
                MuscleGroup = MuscleGroup.Shoulders,
                Title = "Military Press"
            };

            Exercise frontRaise = new Exercise()
            {
                ExerciseType = ExerciseType.Dumbbell,
                MuscleGroup = MuscleGroup.Shoulders,
                Title = "Front Raise"
            };

            Exercise uprightRow = new Exercise()
            {
                ExerciseType = ExerciseType.Barbell,
                MuscleGroup = MuscleGroup.Traps,
                Title = "Upright Row"
            };

            Exercise shrug = new Exercise()
            {
                ExerciseType = ExerciseType.Dumbbell,
                MuscleGroup = MuscleGroup.Traps,
                Title = "Dumbbell Shrug"
            };

            await _database.CreateExerciseItem(shrug);
        }
    }
}
