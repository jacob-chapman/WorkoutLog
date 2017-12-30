using System;
using System.Diagnostics.Contracts;
using WorkoutLog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutLog.Services;
namespace WorkoutLog.ViewModels
{


    public interface IWorkoutHomeItem
    {
        IWorkoutItemType ItemType { get; }
    }

    public class AddWorkoutHomeItem : IWorkoutHomeItem
    {
        public IWorkoutItemType ItemType => IWorkoutItemType.AddWorkout;

        public string Title => "Create Workout";
    }

    public class WorkoutHomeItem : IWorkoutHomeItem
    {

        public IWorkoutItemType ItemType => IWorkoutItemType.WorkoutItem;

        public Workout Workout { get; set; }
    }

    public class WorkoutHomeViewModel
    {
        public List<IWorkoutHomeItem> ViewModels { get; private set; }

        private AddWorkoutHomeItem _addWorkoutHomeItem => new AddWorkoutHomeItem();

        private List<WorkoutHomeItem> _workoutHistory { get; set; }

        public WorkoutHomeViewModel()
        {
            _workoutHistory = new List<WorkoutHomeItem>();
            ViewModels = new List<IWorkoutHomeItem>();
            ViewModels.Add(_addWorkoutHomeItem);
        }

        public async void GetWorkoutHistory()
        {
            var workouts = await WorkoutDatabaseService.GetWorkoutHistory();

            foreach (var workout in workouts)
            {
                _workoutHistory.Add(new WorkoutHomeItem()
                {
                    Workout = workout
                });
            }

            //todo add only those that are new
            ViewModels.RemoveAll(x => x.ItemType == IWorkoutItemType.WorkoutItem);
            ViewModels.AddRange(_workoutHistory);
        }
    }
}
