using System;
using System.IO;
namespace WorkoutLog.iOS.ClientServices
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
            return Path.Combine(DatabasePath, "..", "Library", $"{databaseName}.db");
        }
    }
}
