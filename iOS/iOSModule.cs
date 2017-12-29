using System;
using Splat;
using WorkoutLog.iOS.ClientServices;

namespace WorkoutLog.iOS
{
    public class iOSModule
    {
        public static void Init()
        {

            Locator.CurrentMutable.Register(() => new FileSystemService(), typeof(IFileSystemService));

        }
    }
}
