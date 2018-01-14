using System;
using WorkoutLog.ViewModels;
using WorkoutLog.Views;
using WorkoutLog.Models;
using WorkoutLog.Services;
using System.Threading.Tasks;
namespace WorkoutLog.Presenters
{
    //todo save workout
    public class WorkoutSessionPresenter : IPresenter
    {
        public WorkoutSessionPresenter()
        {
            //Initialize();
        }

        private WorkoutSessionViewModel _viewModel;

        public IWorkoutSessionView View { get; set; }

        public void Initialize()
        {
            _viewModel = new WorkoutSessionViewModel();

            View?.AskForWorkoutTitle();
        }

        /// <summary>
        /// Creates the workout for the session and inserts into view model
        /// </summary>
        /// <param name="title">Workout title</param>
        public async void CreateWorkout(string title)
        {
            //Create the workout item
            Workout workout = new Workout()
            {
                Title = title
            };

            //Insert into database
            await WorkoutDatabaseService.CreateWorkout(workout);

            //Insert into view model
            _viewModel.InsertWorkout(workout);

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
        public async void UpdateWorkoutSet(Set set)
        {
            //todo look into whether or not the update with children call handles the individual call
            //Should be ok to just run this in the background since UI would already reflect the update
            Task.Run(() =>
            {
                WorkoutDatabaseService.UpdateSet(set);
                WorkoutDatabaseService.UpdateWorkoutWithSet(_viewModel.Workout);
            });
        }

        private async Task<Set> InsertSetIntoDb(Exercise exercise)
        {
            //create the set object
            Set newSet = new Set()
            {
                Exercise = exercise
            };

            //insert into database
            await WorkoutDatabaseService.CreateSet(newSet);

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
