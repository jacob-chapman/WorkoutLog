using System;
using System.Diagnostics.Contracts;
namespace WorkoutLog
{
    public interface IFileSystemService
    {
        string DatabasePath { get; set; }

        string GetDatabasePathFor(string databaseName);
    }
}
