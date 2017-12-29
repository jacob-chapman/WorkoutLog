using System;
using Splat;
using WorkoutLog.Droid.ClientServices;
namespace WorkoutLog.Droid
{
    public class AndroidModule
    {
        public static void Init()
        {

            Locator.CurrentMutable.Register(() => new FileSystemService(), typeof(IFileSystemService));

        }
    }
}
