using System;
using UIKit;
using WorkoutLog.ViewModels;
using WorkoutLog.Views;
using WorkoutLog.iOS.DataSources;
using Masonry;
using WorkoutLog.Presenters;
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
            _workoutHomeTableView.RegisterClassForCellReuse(typeof(UITableViewCell), WorkoutHomeTableDataSource.CellIdentifier);

            dataSource.AddWorkoutAction = () =>
            {
                var vc = new UINavigationController(new WorkoutSessionViewController());
                vc.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                NavigationController?.PresentViewController(vc, true, null);
            };

            View.AddSubview(_workoutHomeTableView);

            AddTableConstraints();

            Presenter.View = this;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            Presenter.Resume();
        }

        #endregion Lifeycycle

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
