using System;
using UIKit;
using WorkoutLog.ViewModels;
using WorkoutLog.Views;
using WorkoutLog.iOS.DataSources;
using Masonry;
using WorkoutLog.Presenters;
using WorkoutLog.Models;
namespace WorkoutLog.iOS.Controllers
{
    public class WorkoutHomeViewController : UIViewController, IWorkoutHomeView
    {
        public WorkoutHomeViewController()
        {
        }

        private UITableView _workoutHomeTableView = new UITableView();
        private WorkoutHomeTableDataSource dataSource = new WorkoutHomeTableDataSource();

        private WorkoutHomePresenter _presenter;
        public WorkoutHomePresenter Presenter
        {
            get
            {
                return _presenter ?? (_presenter = new WorkoutHomePresenter());
            }
        }

        #region Lifeycycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _workoutHomeTableView.Source = dataSource;
            _workoutHomeTableView.RegisterClassForCellReuse(typeof(WorkoutHomeItemTableCell), WorkoutHomeItemTableCell.CellIdentifier);
            _workoutHomeTableView.RegisterClassForCellReuse(typeof(AddWorkoutTableCell), AddWorkoutTableCell.CellIdentifier);

            dataSource.AddWorkoutAction = () =>
            {
                var vc = new UINavigationController(new WorkoutSessionViewController(null)
                {
                    OnDismissedAction = WillReappear
                });
                vc.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                NavigationController?.PresentViewController(vc, true, null);
            };
            dataSource.OpenExisitingWorkout = OpenExisitingWorkout;

            View.AddSubview(_workoutHomeTableView);

            AddTableConstraints();

            Presenter.View = this;
            Presenter.Initialize();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        #endregion Lifeycycle

        #region Helpers

        private void WillReappear()
        {
            Presenter?.Resume();
        }

        private void OpenExisitingWorkout(Workout workout)
        {
            var vc = new UINavigationController(new WorkoutSessionViewController(workout)
            {
                OnDismissedAction = WillReappear
            });
            vc.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
            NavigationController?.PresentViewController(vc, true, null);
        }

        #endregion Helpers
        #region Constraints

        private void AddTableConstraints()
        {
            _workoutHomeTableView.MakeConstraints(make =>
            {
                make.Left.EqualTo(View.Left());
                make.Right.EqualTo(View.Right());
                make.Top.EqualTo(View.Top());
                make.Bottom.EqualTo(View.Bottom());
            });
        }

        #endregion Constraints

        #region IWorkoutHomeView

        public void Render(WorkoutHomeViewModel viewModel)
        {
            dataSource.Items = viewModel.ViewModels;

            _workoutHomeTableView.ReloadData();
        }

        #endregion IWorkoutHomeView
    }
}
