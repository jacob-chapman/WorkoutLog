using System;
using System.IO;
using WorkoutLog;

namespace WorkoutLog.Droid.ClientServices
{
    public class FileSystemService : IFileSystemService
    {
        public FileSystemService()
        {
            DatabasePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public string DatabasePath { get; set; }

        public string GetDatabasePathFor(string databaseName)
        {
            return Path.Combine(DatabasePath, $"{databaseName}.txt");
        }
    }
}
