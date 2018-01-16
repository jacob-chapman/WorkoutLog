using System;
using SQLite;
using Splat;
using WorkoutLog.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml.Linq;
using SQLiteNetExtensionsAsync.Extensions;

namespace WorkoutLog
{
    public abstract class Database
    {
        protected abstract string EntityName { get; }

        protected SQLiteAsyncConnection _connection;
        protected IFileSystemService _fileSystemService;

        public abstract Task Init();

        protected Database()
        {
            _fileSystemService = Locator.Current.GetService<IFileSystemService>();
            CreateConnection();
        }

        protected void CreateConnection()
        {
            try
            {
                _connection = new SQLiteAsyncConnection(_fileSystemService.GetDatabasePathFor(EntityName));
            }
            catch (Exception ex)
            {
                LogHost.Default.Error($"Database.CreateConnection: {ex.Message}");
            }
        }
    }

    public class WorkoutDatabase : Database
    {
        protected override string EntityName => "Workout";

        public override async Task Init()
        {
            var createTablesTask = CreateWorkoutDatabaseTables();

            await Task.WhenAll(createTablesTask);

            return;
        }

        #region Create

        private async Task CreateWorkoutDatabaseTables()
        {
            try
            {
                await _connection.CreateTablesAsync<Workout, Set, Exercise>();
            }
            catch (Exception ex)
            {
                LogHost.Default.Error($"WorkoutDatabase.CreateWorkoutDatabaseTables: {ex.Message}");
            }
            return;
        }

        public async Task<int?> CreateWorkoutItem(Workout item)
        {
            try
            {
                var id = await _connection.InsertAsync(item);
                return id;
            }
            catch (Exception ex)
            {
                LogHost.Default.Error($"WorkoutDatabase.CreateWorkoutItem: {ex.Message}");
            }

            return null;
        }

        public async Task<int?> CreateSetItem(Set item)
        {
            try
            {
                return await _connection.InsertAsync(item);
            }
            catch (Exception ex)
            {
                LogHost.Default.Error($"WorkoutDatabase.CreateSetItem: {ex.Message}");
            }

            return null;
        }

        public async Task CreateExerciseItem(Exercise item)
        {
            try
            {
                await _connection.InsertAsync(item);
            }
            catch (Exception ex)
            {
                LogHost.Default.Error($"WorkoutDatabase.CreateExerciseItem: {ex.Message}");
            }
        }

        #endregion

        #region Helpers

        public bool DoesExisit<T>(T item)
        {
            return false;
        }

        #endregion Helpers

        #region Update


        public async Task UpdateWorkoutWithSet(Workout workout)
        {
            try
            {
                await _connection.UpdateWithChildrenAsync(workout);
            }
            catch (Exception ex)
            {
                LogHost.Default.Error($"WorkoutDatabase.CreateSetItem: {ex.Message}");
            }

            return;
        }


        public async Task UpdateSet(Set set)
        {
            try
            {
                await _connection.UpdateAsync(set);
            }
            catch (Exception ex)
            {
                LogHost.Default.Error("Database.UpdateSet", ex.ToString());
            }
        }

        #endregion Update

        #region Query

        public async Task<IEnumerable<Exercise>> GetExercises()
        {
            return await _connection.Table<Exercise>().ToListAsync();
        }

        public async Task<IEnumerable<Workout>> GetWorkouts(int skipAmount, int takeAmount)
        {
            return await _connection.Table<Workout>().OrderByDescending(x => x.WorkoutDate).Skip(skipAmount).Take(takeAmount).ToListAsync();
        }

        public async Task<Workout> GetWorkout(int id)
        {
            return await _connection.Table<Workout>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        #endregion Query
    }
}
