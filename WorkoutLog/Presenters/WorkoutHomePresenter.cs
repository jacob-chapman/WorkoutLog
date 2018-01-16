using System;
using WorkoutLog.ViewModels;
using WorkoutLog.Views;
using WorkoutLog.Services;
using System.Linq;
using System.Collections.Generic;
using WorkoutLog.Models;
namespace WorkoutLog.Presenters
{
    public class WorkoutHomePresenter : IPresenter
    {
        private WorkoutHomeViewModel _workoutHomeViewModel;
        private const int _batchNumber = 15;
        private int _pageNumber = 0;

        public IWorkoutHomeView View { get; set; }



        public async void Initialize()
        {
            //Initialize the view model
            _workoutHomeViewModel = new WorkoutHomeViewModel();

            //Get all the workouts
            var workouts = await WorkoutDatabaseService.GetWorkoutHistory(_pageNumber, _batchNumber);

            //Add the workouts into the viewModel
            foreach (var workout in workouts)
            {
                _workoutHomeViewModel.InsertWorkoutViewModel(workout);
            }

            View?.Render(_workoutHomeViewModel);
        }

        public void Pause()
        {

        }

        public async void Resume()
        {
            //Refresh the data
            //Get the data again and compare
            var workouts = await WorkoutDatabaseService.GetWorkoutHistory(_pageNumber, _batchNumber);
            var workoutIds = _workoutHomeViewModel.ViewModels.Where(y => y is WorkoutHomeItem)
                                                  .Select(x => (x as WorkoutHomeItem).Workout.ID).ToList();
            //Get the new workouts if any
            var newWorkouts = workouts.Where(x => !workoutIds.Contains(x.ID));

            if (newWorkouts != null)
            {
                foreach (var workout in newWorkouts)
                {
                    _workoutHomeViewModel.InsertWorkoutViewModel(workout);
                }

                View?.Render(_workoutHomeViewModel);
            }
        }

        public void Stop()
        {

        }
    }
}
