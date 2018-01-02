using System;
using Foundation;
using UIKit;
using CoreGraphics;
using WorkoutLog.Presenters;
using WorkoutLog.ViewModels;
using System.Collections.Generic;
using WorkoutLog.Models;
using Masonry;

namespace WorkoutLog.iOS.Controllers
{
    public class AddExerciseViewController : UIViewController, IAddExerciseView
    {
        public AddExerciseViewController()
        {
        }


        private UITableView _exerciseTableView;
        private AddExerciseTableViewSource _viewSource;
        private AddExercisePresenter _presenter;


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Nav menu stuff
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Close", UIBarButtonItemStyle.Plain, (sender, e) =>
            {
                NavigationController?.DismissViewController(true, null);
            });

            View.BackgroundColor = UIColor.LightGray;

            _exerciseTableView = new UITableView(new CGRect(), UITableViewStyle.Grouped);
            _viewSource = new AddExerciseTableViewSource();

            _exerciseTableView.Source = _viewSource;
            _exerciseTableView.RegisterClassForCellReuse(typeof(ExerciseTableCell), ExerciseTableCell.CellIdentifier);
            _presenter = new AddExercisePresenter();
            _presenter.View = this;
            _presenter.Initialize();

            View.AddSubview(_exerciseTableView);

            Initialize();
        }

        private void Initialize()
        {
            _exerciseTableView.MakeConstraints(make =>
            {
                make.Left.EqualTo(View.Left());
                make.Right.EqualTo(View.Right());
                make.Top.EqualTo(View.Top());
                make.Bottom.EqualTo(View.Bottom());
            });
        }

        #region IAddExerciseView

        public void Render(AddExerciseViewModel viewModel)
        {
            _viewSource.Items = viewModel.Exercises;
            _exerciseTableView.ReloadData();
        }

        #endregion IAddExerciseView
    }

    public class AddExerciseTableViewSource : UITableViewSource
    {
        public List<Exercise> Items { get; set; }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ExerciseTableCell.CellIdentifier, indexPath) as ExerciseTableCell;

            var item = Items[indexPath.Row];

            cell.Bind(item.Title);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return Items?.Count ?? 0;
        }
    }

    public class ExerciseTableCell : UITableViewCell
    {
        private UILabel _cellLabel;
        public static string CellIdentifier = "exerciseTableCell";

        public ExerciseTableCell(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public void Initialize()
        {
            _cellLabel = new UILabel();
            _cellLabel.TextColor = UIColor.Blue;

            ContentView.AddSubview(_cellLabel);

            _cellLabel.MakeConstraints(make =>
            {
                make.CenterX.EqualTo(ContentView.CenterX());
                make.CenterY.EqualTo(ContentView.CenterY());
            });
        }

        public void Bind(string text)
        {
            _cellLabel.Text = text;
        }
    }
}
