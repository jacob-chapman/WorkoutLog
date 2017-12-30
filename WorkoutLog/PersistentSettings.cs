using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace WorkoutLog
{
    public class PersistentSettings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static string UserName
        {
            get => AppSettings.GetValueOrDefault(nameof(UserName), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UserName), value);
        }

        private static string HasLoadedExercisesKey = "hasLoadedExercisesKey";

        public static bool HasLoadedExercises
        {
            get => AppSettings.GetValueOrDefault(HasLoadedExercisesKey, false);
            set => AppSettings.AddOrUpdateValue(HasLoadedExercisesKey, value);
        }
    }
}
