using System;
using UIKit;
using WorkoutLog.iOS.DataSources;
using WorkoutLog.Views;
using WorkoutLog.ViewModels;
using WorkoutLog.Presenters;
using Masonry;
using WorkoutLog.iOS.Views;
using WorkoutLog.Models;

namespace WorkoutLog.iOS.Controllers
{
    public class WorkoutSessionViewController : UIViewController, IWorkoutSessionView
    {
        public WorkoutSessionViewController()
        {
        }

        private UITableView _setsTableView;
        private WorkoutSessionDataSource _dataSource;

        private WorkoutSessionViewModel _viewModel;

        private WorkoutSessionPresenter _presenter;

        #region Lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Nav menu stuff
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Close", UIBarButtonItemStyle.Plain, (sender, e) =>
            {
                NavigationController?.DismissViewController(true, null);
            });

            _setsTableView = new UITableView(new CoreGraphics.CGRect(), UITableViewStyle.Grouped);
            _dataSource = new WorkoutSessionDataSource();

            _dataSource.AddExerciseAction = AddExerciseAction;
            _setsTableView.BackgroundColor = UIColor.White;
            _setsTableView.Source = _dataSource;
            _setsTableView.RegisterClassForCellReuse(typeof(SetsHeaderTableCell), SetsHeaderTableCell.CellIdentifier);
            _setsTableView.RegisterClassForCellReuse(typeof(SetTableCell), SetTableCell.CellIdentifier);
            _setsTableView.RegisterClassForCellReuse(typeof(AddExerciseTableCell), AddExerciseTableCell.CellIdentifier);
            _setsTableView.RegisterClassForCellReuse(typeof(FinishWorkoutTableCell), FinishWorkoutTableCell.CellIdentifier);
            _setsTableView.EstimatedRowHeight = 200f;
            _setsTableView.RowHeight = UITableView.AutomaticDimension;


            _presenter = new WorkoutSessionPresenter();
            _presenter.View = this;
            _presenter.Initialize();
            View.BackgroundColor = UIColor.LightGray;

            View.AddSubview(_setsTableView);

            Initialize();
        }

        #endregion Lifecycle

        #region Helpers

        private void ExerciseAdded(Exercise exercise)
        {
            _presenter.CreateNewSet(exercise);
        }

        private void AddExerciseAction()
        {
            var vc = new UINavigationController(new AddExerciseViewController(ExerciseAdded));
            vc.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;

            NavigationController?.PresentViewController(vc, true, null);
        }

        private void FinishExercise()
        {
            //todo save workout
            NavigationController?.DismissViewController(true, null);
        }

        private void Initialize()
        {
            _setsTableView.MakeConstraints(make =>
            {
                make.Top.EqualTo(View.Top()).Offset(15);
                make.Left.EqualTo(View.Left()).Offset(15);
                make.Right.EqualTo(View.Right()).Offset(-15);
                make.Bottom.EqualTo(View.Bottom());
            });
        }

        #endregion Helpers

        #region IWorkoutSessionView

        public void Render(WorkoutSessionViewModel viewModel)
        {
            _viewModel = viewModel;
            _dataSource.Items = viewModel.Items;
            _setsTableView.ReloadData();
            NavigationItem.Title = _viewModel.Workout.Title;
        }

        public void AskForWorkoutTitle()
        {
            var alertController = UIAlertController.Create("Workout", "Please enter a name for this workout?", UIAlertControllerStyle.Alert);

            alertController.AddTextField((textField) => { });

            alertController.AddAction(UIAlertAction.Create("Save", UIAlertActionStyle.Default, (obj) =>
            {
                _presenter.CreateWorkout(alertController.TextFields[0].Text);
            }));

            PresentViewController(alertController, true, null);
        }

        #endregion IWorkoutSessionView
    }
}
