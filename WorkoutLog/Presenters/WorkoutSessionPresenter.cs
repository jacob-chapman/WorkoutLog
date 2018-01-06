using System;
using WorkoutLog.ViewModels;
using WorkoutLog.Views;
using WorkoutLog.Models;
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

        public async void CreateWorkout(string title)
        {
            await _viewModel.CreateAndSetWorkout(title);

            View?.Render(_viewModel);
        }

        public async void CreateNewSet(Exercise exercise)
        {
            await _viewModel.CreateSetsViewModel(exercise);

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
