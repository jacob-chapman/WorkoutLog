using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using WorkoutLog.ViewModels;
using Masonry;
namespace WorkoutLog.iOS.DataSources
{
    public class WorkoutHomeTableDataSource : UITableViewDataSource
    {
        public WorkoutHomeTableDataSource()
        {
        }

        public static string CellIdentifier = "workoutHomeCell";
        public List<IWorkoutHomeItem> Items { get; set; }

        public Action AddWorkoutAction;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);
            var item = Items[indexPath.Row];

            if (cell == null)
            {
                //todo add switch for different item types
                cell = new AddWorkoutTableCell(AddWorkoutAction);
            }

            cell = new AddWorkoutTableCell(AddWorkoutAction);

            return cell;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return Items?.Count ?? 0;
        }
    }

    //todo separate out; add action for button
    public class AddWorkoutTableCell : UITableViewCell
    {
        private UIButton _addWorkoutButton;

        public AddWorkoutTableCell(Action addWorkoutActon)
        {
            _addWorkoutButton = new UIButton();
            _addWorkoutButton.SetTitle("Add Workout", UIControlState.Normal);
            _addWorkoutButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            _addWorkoutButton.TouchUpInside += (sender, e) => { addWorkoutActon?.Invoke(); };

            ContentView.AddSubview(_addWorkoutButton);

            Initialize();
        }

        private void Initialize()
        {
            _addWorkoutButton.MakeConstraints(make =>
            {
                make.CenterX.EqualTo(this.CenterX());
                make.CenterY.EqualTo(this.CenterY());
            });
        }
    }
}
