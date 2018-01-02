using System;
using System.Collections.Generic;
using WorkoutLog.Models;
using System.Linq;
namespace WorkoutLog.ViewModels
{
    //todo group by muscle group
    public class AddExerciseViewModel
    {
        public AddExerciseViewModel()
        {
        }

        public List<Exercise> Exercises { get; private set; }

        public void SetExercises(IEnumerable<Exercise> exercises)
        {
            if (Exercises == null) Exercises = new List<Exercise>();

            Exercises = exercises.ToList();
        }

        public void AddExercise(Exercise newExercise)
        {
            //todo some index validation to put in correct group
            Exercises?.Add(newExercise);
        }
    }
}
