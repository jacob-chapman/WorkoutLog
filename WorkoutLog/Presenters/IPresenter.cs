using System;
using WorkoutLog.Views;
namespace WorkoutLog.Presenters
{
    public interface IPresenter
    {
        void Resume();

        void Pause();

        void Stop();
    }
}
