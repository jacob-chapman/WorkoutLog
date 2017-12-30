using System;
using WorkoutLog.ViewModels;
using WorkoutLog.Views;
namespace WorkoutLog.Presenters
{
    public class WorkoutHomePresenter : IPresenter
    {
        private WorkoutHomeViewModel _workoutHomeViewModel;

        public IWorkoutHomeView View { get; set; }

        public WorkoutHomePresenter()
        {
            Initialize();
        }

        private void Initialize()
        {

            _workoutHomeViewModel = new WorkoutHomeViewModel();
        }

        public void Pause()
        {

        }

        public void Resume()
        {
            _workoutHomeViewModel.GetWorkoutHistory();

            View?.Render(_workoutHomeViewModel);
        }

        public void Stop()
        {

        }
    }
}
