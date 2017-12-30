using System;
using WorkoutLog.ViewModels;
namespace WorkoutLog.Views
{
    public interface IView
    {
    }

    public interface IWorkoutHomeView : IView
    {
        void Render(WorkoutHomeViewModel viewModel);
    }
}
