using System;
using WorkoutLog.ViewModels;
using WorkoutLog.Views;
using WorkoutLog.Models;
using WorkoutLog.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace WorkoutLog.Presenters
{
    //todo save workout
    public class WorkoutSessionPresenter : IPresenter
    {
        public WorkoutSessionPresenter(Workout workout) => _workout = workout;

        private Workout _workout;
        private WorkoutSessionViewModel _viewModel;

        public IWorkoutSessionView View { get; set; }

        public void Initialize()
        {
            _viewModel = new WorkoutSessionViewModel();

            if (_workout == null)
                View?.AskForWorkoutTitle();
            else //Use exisiting workout
                SetupExisitingWorkout(_workout);
        }

        /// <summary>
        /// Setups the exisiting workout into the view model 
        /// </summary>
        /// <param name="workout">Workout.</param>
        private void SetupExisitingWorkout(Workout workout)
        {
            _viewModel.InsertWorkout(workout);

            //list of execises
            List<Exercise> exercises = new List<Exercise>();

            foreach (var set in workout.Sets)
            {
                if (!exercises.Contains(set.Exercise)) //Not in the current view model yet
                {
                    exercises.Add(set.Exercise);

                    _viewModel.InsertNewSetExercise(set);
                }
                else
                {
                    _viewModel.InsertExistingExerciseSet(set);
                }
            }
        }

        /// <summary>
        /// Creates the workout for the session and inserts into view model
        /// </summary>
        /// <param name="title">Workout title</param>
        public async void CreateWorkout(string title)
        {
            //Create the workout item
            _workout = new Workout()
            {
                Title = title
            };

            //Insert into database
            await WorkoutDatabaseService.CreateWorkout(_workout);

            //Insert into view model
            _viewModel.InsertWorkout(_workout);

            View?.Render(_viewModel);
        }

        /// <summary>
        /// Creates a new set object from a given exercise.
        /// After creation takes the set object and inserts it into the view model.
        /// </summary>
        /// <param name="exercise">Exercise.</param>
        public async void CreateNewSet(Exercise exercise)
        {
            //Create the set and insert into db
            Set newSet = await InsertSetIntoDb(exercise);

            //insert into view model
            _viewModel.InsertNewSetExercise(newSet);

            View?.Render(_viewModel);
        }

        /// <summary>
        /// Creates a new set and adds it to an exisitng exercise set collection.
        /// Inserts the new object into the view model
        /// </summary>
        /// <param name="exercise">Exercise.</param>
        public async void AddSet(Exercise exercise)
        {
            //Create the set and insert into db
            Set newSet = await InsertSetIntoDb(exercise);

            _viewModel.InsertExistingExerciseSet(newSet);

            View?.Render(_viewModel);
        }

        //todo error handling
        public void UpdateWorkoutSet(Set set)
        {
            //todo look into whether or not the update with children call handles the individual call
            //Should be ok to just run this in the background since UI would already reflect the update
            Task.Run(() =>
            {
                WorkoutDatabaseService.UpdateSet(set);
                WorkoutDatabaseService.UpdateWorkoutWithSet(_workout);
            });
        }

        public void SaveWorkout()
        {
            Task.Run(() => { WorkoutDatabaseService.UpdateWorkoutWithSet(_workout); });
        }

        private async Task<Set> InsertSetIntoDb(Exercise exercise)
        {
            //create the set object
            Set newSet = new Set()
            {
                Exercise = exercise
            };

            //Add the set to the workout
            if (_workout.Sets == null) _workout.Sets = new List<Set>();

            _workout.Sets.Add(newSet);

            //insert into database
            await WorkoutDatabaseService.CreateSet(newSet);

            UpdateWorkoutSet(newSet);

            return newSet;

        }

        public void Pause()
        {

        }

        public void Resume()
        {

        }

        public void Stop()
        {

        }
    }
}
