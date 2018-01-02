using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using WorkoutLog.Models;
using WorkoutLog.ViewModels;
using Masonry;
using WorkoutLog.iOS.Views;

namespace WorkoutLog.iOS.DataSources
{
    public class WorkoutSessionDataSource : UITableViewSource
    {
        public WorkoutSessionDataSource()
        {
        }

        public Action AddExerciseAction;
        public List<IWorkoutSessionItem> Items { get; set; }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //Get itemtype 
            var currentItem = Items[indexPath.Section];

            switch (currentItem.ItemType)
            {
                case IWorkoutSessionItemType.AddExercise:
                    var addExerciseCell = tableView.DequeueReusableCell(AddExerciseTableCell.CellIdentifier, indexPath) as AddExerciseTableCell;
                    addExerciseCell.Bind((currentItem as AddExerciseItemViewModel).ButtonTitle);
                    return addExerciseCell;
                case IWorkoutSessionItemType.FinishWorkout:
                    var finishWorkoutCell = tableView.DequeueReusableCell(FinishWorkoutTableCell.CellIdentifier, indexPath) as FinishWorkoutTableCell;
                    finishWorkoutCell.Bind((currentItem as FinishExerciseViewModel).ButtonTitle);
                    return finishWorkoutCell;
                case IWorkoutSessionItemType.SetsItem:
                    var setsTableCell = tableView.DequeueReusableCell(SetsTableCell.CellIdentifier, indexPath) as SetsTableCell;
                    return setsTableCell;
            }

            return new UITableViewCell();
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return Items?.Count ?? 0;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 1;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var item = Items[indexPath.Section];

            if (item.ItemType == IWorkoutSessionItemType.AddExercise) AddExerciseAction?.Invoke();

            return;
        }
    }

}
