using System;
using Foundation;
using UIKit;
using CoreGraphics;
using WorkoutLog.Presenters;
using WorkoutLog.ViewModels;
using System.Collections.Generic;
using WorkoutLog.Models;
using Masonry;
using System.Linq;

namespace WorkoutLog.iOS.Controllers
{
    public class AddExerciseViewController : UIViewController, IAddExerciseView
    {
        public AddExerciseViewController(Action<Exercise> addExerciseAction)
        {
            _addExerciseAction = addExerciseAction;
        }

        private Action<Exercise> _addExerciseAction;
        private UITableView _exerciseTableView;
        private AddExerciseTableViewSource _viewSource;
        private AddExercisePresenter _presenter;
        private AddExerciseViewModel _viewModel;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Nav menu stuff
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Close", UIBarButtonItemStyle.Plain, (sender, e) =>
            {
                NavigationController?.DismissViewController(true, null);
            });

            NavigationItem.RightBarButtonItem = new UIBarButtonItem("New Exercise", UIBarButtonItemStyle.Plain, (sender, e) =>
            {
                var vc = new UINavigationController(new CreateExerciseViewController(_presenter));
                vc.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
                PresentViewController(vc, true, null);
            });

            View.BackgroundColor = UIColor.LightGray;

            _exerciseTableView = new UITableView(new CGRect(), UITableViewStyle.Grouped);
            _viewSource = new AddExerciseTableViewSource(AddExerciseActionHelper);

            _exerciseTableView.Source = _viewSource;
            _exerciseTableView.RegisterClassForCellReuse(typeof(ExerciseTableCell), ExerciseTableCell.CellIdentifier);
            _presenter = new AddExercisePresenter();
            _presenter.View = this;
            _presenter.Initialize();

            View.AddSubview(_exerciseTableView);

            Initialize();
        }

        private void AddExerciseActionHelper(Exercise exercise)
        {
            NavigationController?.DismissViewController(true, null);

            _addExerciseAction?.Invoke(exercise);
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
            _viewModel = viewModel;
            _viewSource.Items = _viewModel.Exercises;
            _exerciseTableView.ReloadData();
        }

        public void DisplayError(string message)
        {
            var alertController = UIAlertController.Create("Error", message, UIAlertControllerStyle.Alert);

            alertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));

            PresentViewController(alertController, true, null);
        }

        #endregion IAddExerciseView
    }

    public class AddExerciseTableViewSource : UITableViewSource
    {
        public AddExerciseTableViewSource(Action<Exercise> addExerciseAction)
        {
            _addExerciseAction = addExerciseAction;
        }

        private Action<Exercise> _addExerciseAction;
        public List<Exercise> Items { get; set; }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ExerciseTableCell.CellIdentifier, indexPath) as ExerciseTableCell;

            var item = Items.GroupBy(x => x.MuscleGroup)
                            .ElementAt(indexPath.Section)
                            .ElementAt(indexPath.Row);

            cell.Bind(item.Title);

            return cell;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            var groups = Items?.GroupBy(x => x.MuscleGroup);
            var currentSection = groups.ElementAt((int)section);

            return currentSection.ElementAt(0).MuscleGroup.ToString();
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return Items?.GroupBy(x => x.MuscleGroup).Count() ?? 0;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            var groups = Items?.GroupBy(x => x.MuscleGroup);

            return groups?.ElementAt((int)section).Count() ?? 0;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var groups = Items?.GroupBy(x => x.MuscleGroup);
            var section = groups?.ElementAt((int)indexPath.Section);
            _addExerciseAction?.Invoke(section?.ElementAt(indexPath.Row));
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
