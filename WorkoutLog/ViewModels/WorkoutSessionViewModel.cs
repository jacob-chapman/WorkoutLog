using System;
using WorkoutLog.Models;
using System.Collections.Generic;
using WorkoutLog.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WorkoutLog.ViewModels
{
    public interface IWorkoutSessionItem
    {
        IWorkoutSessionItemType ItemType { get; }
    }

    public class AddExerciseItemViewModel : IWorkoutSessionItem
    {
        public IWorkoutSessionItemType ItemType => IWorkoutSessionItemType.AddExercise;

        public string ButtonTitle => "Add Exercise";
    }

    public class FinishExerciseViewModel : IWorkoutSessionItem
    {
        public IWorkoutSessionItemType ItemType => IWorkoutSessionItemType.FinishWorkout;

        public string ButtonTitle => "Finish Workout";
    }

    public class SetsViewModel : IWorkoutSessionItem
    {
        public IWorkoutSessionItemType ItemType => IWorkoutSessionItemType.SetsItem;

        public List<Set> Sets { get; set; }

        public SetsViewModel()
        {
            Sets = new List<Set>();
        }

        public async Task<Set> CreateSet(Exercise exercise, int? reps, double? weight, double? time)
        {
            Set set = new Set()
            {
                Exercise = exercise,
                Reps = reps,
                Weight = weight,
                Time = time
            };

            //Create the set item
            await WorkoutDatabaseService.CreateSet(set);

            Sets.Add(set);

            return set;
        }
    }

    //Controls the current workout session
    //Has the ability to add sets
    //Modify a current set
    public class WorkoutSessionViewModel
    {
        public WorkoutSessionViewModel()
        {
            Items = new List<IWorkoutSessionItem>();

            //Probably unncessary for this situation but it happened
            Items.Insert(addExerciseItemIndex, new AddExerciseItemViewModel());
            Items.Insert(finishWorkoutItemIndex, new FinishExerciseViewModel());
        }

        #region Indexes

        private int setsViewModelIndex
        {
            get
            {
                int index = 0;
                index += Items.Count - 2;
                return index;
            }
        }

        private int addExerciseItemIndex
        {
            get
            {
                int index = 0;
                if (Items.Count > 2) index += Items.Count;
                return index;
            }
        }

        private int finishWorkoutItemIndex
        {
            get
            {
                int index = 0;
                index++; //For Add Workout
                if (Items.Count > 2) index += Items.Count;
                return index;
            }
        }

        #endregion Indexes

        public Workout Workout { get; set; }

        public List<IWorkoutSessionItem> Items { get; set; }

        public async void CreateAndSetWorkout(string title)
        {
            var dbWorkout = await WorkoutDatabaseService.CreateWorkout(title);

            if (dbWorkout == null) return;

            Workout = dbWorkout;
        }

        public SetsViewModel CreateSetsViewModel()
        {
            SetsViewModel vm = new SetsViewModel();

            Items.Insert(setsViewModelIndex, vm);

            return vm;
        }

        public async void UpdateWorkoutWithSet(Set set)
        {
            if (Workout.Sets == null)
                Workout.Sets = new List<Set>();

            Workout.Sets.Add(set);
        }
    }
}
