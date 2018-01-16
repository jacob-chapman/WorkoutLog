using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using WorkoutLog.Models;
using WorkoutLog.ViewModels;
using Masonry;
using WorkoutLog.iOS.Views;
using System.Linq;

namespace WorkoutLog.iOS.DataSources
{
    public class WorkoutSessionDataSource : UITableViewSource
    {
        public WorkoutSessionDataSource()
        {
        }

        #region Actions

        public Action AddExerciseAction;
        public Action<Exercise> AddSetToExisitingExercise;

        #endregion Actions

        public List<IWorkoutSessionItem> Items { get; set; }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //Get itemtype 
            var currentItem = Items.GroupBy(x => x.RelatedToExercise).ElementAt(indexPath.Section).ElementAt(indexPath.Row);

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
                case IWorkoutSessionItemType.SetItem:
                    var setTableCell = tableView.DequeueReusableCell(SetTableCell.CellIdentifier, indexPath) as SetTableCell;
                    var set = currentItem as SetViewModel;
                    setTableCell.Bind(set);
                    tableView.ReloadRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
                    return setTableCell;
                case IWorkoutSessionItemType.SetsHeader:
                    var setHeaderTableCell = tableView.DequeueReusableCell(SetsHeaderTableCell.CellIdentifier, indexPath) as SetsHeaderTableCell;
                    setHeaderTableCell.Bind(currentItem as SetHeaderViewModel);
                    return setHeaderTableCell;
                case IWorkoutSessionItemType.AddSet:
                    var addSetTableCell = tableView.DequeueReusableCell(AddSetTableCell.CellIdentifier) as AddSetTableCell;
                    addSetTableCell.Bind(currentItem as AddSetViewModel);
                    return addSetTableCell;
            }

            return new UITableViewCell();
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            var item = Items.GroupBy(x => x.RelatedToExercise).ElementAt((int)section);

            if (item.FirstOrDefault()?.ItemType != IWorkoutSessionItemType.SetsHeader) return string.Empty;

            return (item.ElementAt(1) as SetViewModel).Set.Exercise.Title;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            if (Items == null) return 0;

            //Get the number of different exercises
            var exercisesCount = Items.Select(x => x.RelatedToExercise).Distinct().Count();

            return exercisesCount;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (Items == null) return 0;

            var exerciseGroups = Items.GroupBy(x => x.RelatedToExercise);

            return exerciseGroups.ElementAt((int)section).Count();
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var item = Items.GroupBy(x => x.RelatedToExercise).ElementAt(indexPath.Section).ElementAt(indexPath.Row);

            if (item.ItemType == IWorkoutSessionItemType.AddExercise) AddExerciseAction?.Invoke();

            if (item.ItemType == IWorkoutSessionItemType.AddSet) AddSetToExisitingExercise?.Invoke((item as AddSetViewModel)?.RelatedToExercise);

            return;
        }
    }

}
