using System;
using WorkoutLog.Models;
using System.Collections.Generic;
using WorkoutLog.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

namespace WorkoutLog.ViewModels
{
    public interface IWorkoutSessionItem
    {
        IWorkoutSessionItemType ItemType { get; }

        /// <summary>
        /// Value to determine which exercise the view model is related too.
        /// Helps determine the spacing on the UI
        /// </summary>
        /// <value>The related to exercise. Null shows the filler items</value>
        Exercise RelatedToExercise { get; set; }
    }

    public class AddExerciseItemViewModel : IWorkoutSessionItem
    {
        public IWorkoutSessionItemType ItemType => IWorkoutSessionItemType.AddExercise;

        public Exercise RelatedToExercise { get; set; } = null;

        public string ButtonTitle => "Add Exercise";
    }

    public class FinishExerciseViewModel : IWorkoutSessionItem
    {
        public IWorkoutSessionItemType ItemType => IWorkoutSessionItemType.FinishWorkout;

        public Exercise RelatedToExercise { get; set; } = null;

        public string ButtonTitle => "Finish Workout";
    }

    public class SetHeaderViewModel : IWorkoutSessionItem
    {
        public IWorkoutSessionItemType ItemType => IWorkoutSessionItemType.SetsHeader;

        public Exercise RelatedToExercise { get; set; }

        public string SetNumberHeader = "Sets Number";

        public string WeightHeader = "Weight";

        public string RepsHeader = "Reps";

        public string TimeHeader = "Time";

        public string CompletedHeader = "Completed";
    }

    public class SetViewModel : IWorkoutSessionItem
    {
        public IWorkoutSessionItemType ItemType => IWorkoutSessionItemType.SetItem;

        public Exercise RelatedToExercise { get; set; }

        public string SetNumber { get; set; }

        public Set Set
        {
            get => _set;

            set
            {
                _set = value;
                RelatedToExercise = _set.Exercise;
            }
        }

        private Set _set;
    }

    public class AddSetViewModel : IWorkoutSessionItem
    {
        public IWorkoutSessionItemType ItemType => IWorkoutSessionItemType.AddSet;

        public Exercise RelatedToExercise { get; set; }

        public string Text = "Add Set";
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

        /// <summary>
        /// Gets the index to insert the set model given
        /// </summary>
        /// <returns>The set view model index.</returns>
        /// <param name="set">Set.</param>
        private (int, string) GetSetViewModelIndex(Set set)
        {
            int index = 0;

            if (!Items.Any(x => x.ItemType == IWorkoutSessionItemType.SetsHeader)) return (index, 1.ToString());

            do
            {
                //Check for item being header
                if (Items[index].ItemType == IWorkoutSessionItemType.SetsHeader) index++;
                if (Items[index].ItemType == IWorkoutSessionItemType.SetItem)
                {
                    var setItem = Items[index] as SetViewModel;
                    //Check for the same exercise 
                    if (setItem.Set.Exercise == set.Exercise)
                    {
                        //Same exercise need to get index after the other sets
                        //Find the total count of same exercise sets
                        var itemCount = Items.Count(x =>
                        {
                            var item = x as SetViewModel;
                            if (item?.Set?.Exercise == set.Exercise) return true;
                            return false;
                        });

                        //break the loop since we have the correct set collection
                        return (index += itemCount, (itemCount + 1).ToString());
                    }
                    else
                    {
                        //we are in the wrong set collection
                        //get the item in the set collection
                        var item = Items[index] as SetViewModel;
                        //Now get the total set count of the current set collection
                        var count = Items.Count(x =>
                        {
                            if ((x as SetViewModel)?.Set.Exercise == item.Set.Exercise) return true;
                            return false;
                        });
                        //add to index
                        index += count;
                    }
                }

                if (Items[index].ItemType == IWorkoutSessionItemType.AddSet) index++;

            }
            while (Items.Count() > index + 2); //2 for the trailing view models always at the end

            return (index, "1");
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

        public void InsertWorkout(Workout workout)
        {
            //Check to see if the workout is null -- does not matter if vm workout object has been set already Presenter should control the actual state
            if (workout != null)
            {
                Workout = workout;
            }
            else //workout is null throw exception 
            {
                throw new Exception("workout object cannot be null!");
            }
        }

        /// <summary>
        /// Inserts a new exercise into the view model. This will include the Header, and the Add Set model to follow all future sets
        /// </summary>
        /// <param name="set">New Exercise Set to insert</param>
        public void InsertNewSetExercise(Set set)
        {
            //First things first check set value
            if (set == null)
            {
                throw new Exception("Set Object cannot be null!");
            }

            //Now get the index to insert the set at 
            var (index, setNumber) = GetSetViewModelIndex(set);

            Items.Insert(index, new AddSetViewModel() { RelatedToExercise = set.Exercise });
            Items.Insert(index, new SetViewModel() { Set = set, SetNumber = setNumber });
            Items.Insert(index, new SetHeaderViewModel() { RelatedToExercise = set.Exercise });

        }

        /// <summary>
        /// Inserts an exisiting exercise set into the view model
        /// </summary>
        /// <param name="set">Set.</param>
        public void InsertExistingExerciseSet(Set set)
        {
            //First things first again check for null
            //todo maybe dont throw an exception -- maybe handle gracefully and proceed with alert?
            if (set == null)
            {
                throw new Exception("Set Object cannot be null!");
            }

            var (index, setNumber) = GetSetViewModelIndex(set);

            Items.Insert(index, new SetViewModel()
            {
                Set = set,
                SetNumber = setNumber
            });
        }

        public void UpdateWorkoutWithSet(Set set)
        {
            if (Workout.Sets == null)
                Workout.Sets = new List<Set>();

            Workout.Sets.Add(set);
        }
    }
}
