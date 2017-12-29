using System;
namespace WorkoutLog.Views
{
    public interface IView
    {

        void Resume();

        void Pause();

        void Stop();
    }

    public interface IWorkoutHomeView : IView
    {

        void Render();

    }
}
