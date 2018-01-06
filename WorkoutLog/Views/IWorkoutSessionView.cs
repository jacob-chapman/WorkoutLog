using System;
using WorkoutLog.ViewModels;
namespace WorkoutLog.Views
{
    public interface IWorkoutSessionView : IView
    {
        void Render(WorkoutSessionViewModel viewModel);

        void AskForWorkoutTitle();
    }
}
