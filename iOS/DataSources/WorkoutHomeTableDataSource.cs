using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using WorkoutLog.ViewModels;
using Masonry;
using System.Runtime.CompilerServices;
using WorkoutLog.Models;
namespace WorkoutLog.iOS.DataSources
{
    public class WorkoutHomeTableDataSource : UITableViewSource
    {
        public WorkoutHomeTableDataSource()
        {
        }

        public List<IWorkoutHomeItem> Items { get; set; }
        public Action<Workout> OpenExisitingWorkout;
        public Action AddWorkoutAction;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var item = Items[indexPath.Row];

            switch (item.ItemType)
            {
                case IWorkoutItemType.AddWorkout:
                    var addWorkoutCell = tableView.DequeueReusableCell(AddWorkoutTableCell.CellIdentifier, indexPath) as AddWorkoutTableCell;
                    addWorkoutCell.AddWorkoutAction = AddWorkoutAction;
                    return addWorkoutCell;
                case IWorkoutItemType.WorkoutItem:
                    var workoutHomeItemCell = tableView.DequeueReusableCell(WorkoutHomeItemTableCell.CellIdentifier, indexPath) as WorkoutHomeItemTableCell;
                    workoutHomeItemCell.Bind((item as WorkoutHomeItem));
                    return workoutHomeItemCell;
            }

            return new UITableViewCell();
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return Items?.Count ?? 0;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var item = Items[(int)indexPath.Row];

            if (item is WorkoutHomeItem)
            {
                OpenExisitingWorkout?.Invoke((item as WorkoutHomeItem)?.Workout);
            }

            return;
        }

    }

    //todo separate out; add action for button
    public class AddWorkoutTableCell : UITableViewCell
    {
        public AddWorkoutTableCell(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public static string CellIdentifier = "addWorkoutCell";
        public Action AddWorkoutAction;
        private UIButton _addWorkoutButton;

        public void Initialize()
        {
            _addWorkoutButton = new UIButton();
            _addWorkoutButton.SetTitle("Add Workout", UIControlState.Normal);
            _addWorkoutButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            _addWorkoutButton.TouchUpInside += (sender, e) => { AddWorkoutAction?.Invoke(); };

            ContentView.AddSubview(_addWorkoutButton);

            SetNeedsUpdateConstraints();
        }

        public override void UpdateConstraints()
        {
            base.UpdateConstraints();

            _addWorkoutButton.MakeConstraints(make =>
            {
                make.CenterX.EqualTo(this.CenterX());
                make.CenterY.EqualTo(this.CenterY());
            });
        }
    }

    public class WorkoutHomeItemTableCell : UITableViewCell
    {
        public WorkoutHomeItemTableCell(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public const string CellIdentifier = "workoutHomeItem";
        private UILabel _lblWorkoutTitle;
        private UILabel _lblWorkoutDate;

        private void Initialize()
        {
            _lblWorkoutTitle = new UILabel();
            _lblWorkoutDate = new UILabel();

            ContentView.AddSubview(_lblWorkoutTitle);
            ContentView.AddSubview(_lblWorkoutDate);

            SetNeedsUpdateConstraints();
        }

        public override void UpdateConstraints()
        {
            base.UpdateConstraints();

            _lblWorkoutTitle.MakeConstraints(make =>
            {
                make.Left.EqualTo(ContentView.Left()).Offset(5);
                make.CenterY.EqualTo(ContentView.CenterY());
            });

            _lblWorkoutDate.MakeConstraints(make =>
            {
                make.Right.EqualTo(ContentView.Right()).Offset(-5);
                make.CenterY.EqualTo(ContentView.CenterY());
            });
        }

        public void Bind(WorkoutHomeItem viewModel)
        {
            _lblWorkoutTitle.Text = viewModel.Workout.Title;

            _lblWorkoutDate.Text = viewModel.Workout.WorkoutDate.ToString();
        }
    }
}
