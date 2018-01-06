using System;
using WorkoutLog.ViewModels;
using WorkoutLog.Views;
using WorkoutLog.Services;
using WorkoutLog.Models;

namespace WorkoutLog.Presenters
{
    public interface IAddExerciseView : IView
    {
        void Render(AddExerciseViewModel viewModel);

        void DisplayError(string message);
    }

    public class AddExercisePresenter : IPresenter
    {
        public AddExercisePresenter()
        {
        }

        private AddExerciseViewModel _viewModel;

        public IAddExerciseView View { get; set; }

        public void Initialize()
        {
            GetExercises();
        }

        private async void GetExercises()
        {
            //Use the workout database service
            //Get all the exercises in the database
            var exercises = await WorkoutDatabaseService.GetExercises();

            if (_viewModel == null) _viewModel = new AddExerciseViewModel();

            _viewModel.SetExercises(exercises);

            View?.Render(_viewModel);
        }

        public async void CreateExercise(Exercise exercise)
        {
            if (string.IsNullOrEmpty(exercise.Title))
            {
                View?.DisplayError("Exercise needs a Title");
                return;
            }

            await WorkoutDatabaseService.CreateExercise(exercise);

            _viewModel.AddExercise(exercise);

            View?.Render(_viewModel);
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
