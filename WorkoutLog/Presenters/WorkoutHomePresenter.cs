using System;
using WorkoutLog.ViewModels;
namespace WorkoutLog.Presenters
{
    public class WorkoutHomePresenter : IPresenter
    {
        private WorkoutHomeViewModel _workoutHomeViewModel;

        public WorkoutHomePresenter()
        {
        }

        private void Initialize()
        {

            _workoutHomeViewModel = new WorkoutHomeViewModel();

            _workoutHomeViewModel.GetWorkoutHistory();
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
