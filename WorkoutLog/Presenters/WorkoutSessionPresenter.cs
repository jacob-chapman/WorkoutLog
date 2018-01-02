using System;
using WorkoutLog.ViewModels;
using WorkoutLog.Views;
namespace WorkoutLog.Presenters
{
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
