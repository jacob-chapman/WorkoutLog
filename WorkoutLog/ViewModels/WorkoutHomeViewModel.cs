using System;
using System.Diagnostics.Contracts;
using WorkoutLog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Workout Workout { get; private set; }
    }

    public class WorkoutHomeViewModel
    {

        public AddWorkoutHomeItem AddWorkoutHomeItem => new AddWorkoutHomeItem();

        public List<WorkoutHomeItem> WorkoutHistory { get; set; }

        //todo 
        public Task GetWorkoutHistory()
        {
            return null;
        }
    }
}
