﻿using System;
using SQLite;
using Splat;
using WorkoutLog.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WorkoutLog
{
    public abstract class Database
    {
        protected abstract string EntityName { get; }

        protected SQLiteAsyncConnection _connection;
        protected IFileSystemService _fileSystemService;

        protected abstract void Init();

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

        protected override async void Init()
        {
            await CreateWorkoutDatabaseTables();
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

        private async Task CreateWorkoutItem(Workout item)
        {
            try
            {
                await _connection.InsertAsync(item);
            }
            catch (Exception ex)
            {
                LogHost.Default.Error($"WorkoutDatabase.CreateWorkoutItem: {ex.Message}");
            }
        }

        private async Task CreateSetItem(Set item)
        {
            try
            {
                await _connection.InsertAsync(item);
            }
            catch (Exception ex)
            {
                LogHost.Default.Error($"WorkoutDatabase.CreateSetItem: {ex.Message}");
            }
        }

        private async Task CreateExerciseItem(Exercise item)
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

        #region Query

        public async Task<IEnumerable<Workout>> GetWorkouts()
        {
            return await _connection.Table<Workout>().ToListAsync();
        }

        #endregion Query
    }
}