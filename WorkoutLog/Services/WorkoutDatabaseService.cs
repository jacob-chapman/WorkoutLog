using System;
using WorkoutLog.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
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

            CreateExercises();
        }

        public static async Task<IEnumerable<Workout>> GetWorkoutHistory(int skipAmount, int takeAmount)
        {
            IEnumerable<Workout> workouts;

            workouts = await _database.GetWorkouts(skipAmount, takeAmount);
            //todo maybe some validation or try catch
            return workouts;
        }

        public static async Task<IEnumerable<Exercise>> GetExercises()
        {
            IEnumerable<Exercise> exercises;

            exercises = await _database.GetExercises();

            exercises.GroupBy(x => x.MuscleGroup);

            return exercises;
        }

        #region Update

        public static async Task UpdateSet(Set set)
        {
            await _database.UpdateSet(set);
        }

        public static async Task UpdateWorkoutWithSet(Workout workout)
        {
            await _database.UpdateWorkoutWithSet(workout);
        }

        #endregion Update

        #region Creation

        public static async Task CreateWorkout(Workout workout)
        {
            var workoutId = await _database.CreateWorkoutItem(workout);
        }

        public static async Task CreateSet(Set set)
        {
            var setId = await _database.CreateSetItem(set);
        }

        public static async Task CreateExercise(Exercise item)
        {
            await _database.CreateExerciseItem(item);
        }

        private static async void CreateExercises()
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

            await _database.CreateExerciseItem(arnoldPress);
            await _database.CreateExerciseItem(latRaise);
            await _database.CreateExerciseItem(militaryPress);
            await _database.CreateExerciseItem(frontRaise);
            await _database.CreateExerciseItem(uprightRow);
            await _database.CreateExerciseItem(shrug);

        }

        #endregion Creation
    }
}
