using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using WorkoutLog.Models;
using WorkoutLog.ViewModels;
using Masonry;
using HealthKitUI;

namespace WorkoutLog.iOS.Views
{
    //todo dynamic cell sizing for padding 
    public class SetsTableCell : UITableViewCell
    {
        public SetsTableCell(IntPtr handle) : base(handle)
        {
            if (_isInitialized) return;

            Initialize();
        }

        private void Initialize()
        {
            _isInitialized = true;

            _tableView = new UITableView();
            _dataSource = new SetsTableCellDataSource();

            _tableView.RegisterClassForCellReuse(typeof(SetTableCell), SetTableCell.CellIdentifier);
            _tableView.RegisterClassForCellReuse(typeof(SetsHeaderTableCell), SetsHeaderTableCell.CellIdentifier);
            _tableView.Source = _dataSource;

            ContentView.AddSubview(_tableView);

            _tableView.MakeConstraints(make =>
            {
                make.Left.EqualTo(ContentView.Left());
                make.Top.EqualTo(ContentView.Top());
                make.Right.EqualTo(ContentView.Right());
                make.Bottom.EqualTo(ContentView.Bottom());
            });
        }

        public void Bind(SetsViewModel viewModel)
        {
            _viewModel = viewModel;
            _dataSource.Sets = _viewModel.Sets;
            _tableView.ReloadData();
        }

        public static string CellIdentifier = "setViewModelCell";
        private bool _isInitialized = false;
        private UITableView _tableView;
        private SetsTableCellDataSource _dataSource;
        private SetsViewModel _viewModel;

        private class SetsTableCellDataSource : UITableViewSource
        {
            public List<Set> Sets { get; set; }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                if (indexPath.Row == 0)
                {
                    var setsHeaderCell = tableView.DequeueReusableCell(SetsHeaderTableCell.CellIdentifier, indexPath);

                    return setsHeaderCell;
                }

                var cell = tableView.DequeueReusableCell(SetTableCell.CellIdentifier, indexPath) as SetTableCell;

                var set = Sets[indexPath.Row - 1];

                cell.Bind(set, (indexPath.Row + 1).ToString());

                return cell;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return Sets?.Count + 1 ?? 0;
            }

        }

        private class SetsHeaderTableCell : UITableViewCell
        {
            public static string CellIdentifier = "setsHeaderCell";
            private UILabel _lblNumber;
            private UILabel _lblWeight;
            private UILabel _lblReps;
            private UILabel _lblComplete;
            private bool _isInitialized = false;


            public SetsHeaderTableCell(IntPtr handle) : base(handle)
            {
                if (_isInitialized) return;

                Initialize();
            }

            public override void UpdateConstraints()
            {
                base.UpdateConstraints();

                _lblNumber.MakeConstraints(make =>
                {
                    make.Left.EqualTo(ContentView.Left()).Offset(5);
                    make.Top.EqualTo(ContentView.Top()).Offset(10);
                    make.Bottom.EqualTo(ContentView.Bottom()).Offset(-10);
                    make.CenterY.EqualTo(ContentView.CenterY());
                    make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
                });

                _lblWeight.MakeConstraints(make =>
                {
                    make.Left.EqualTo(_lblNumber.Right());
                    make.Top.EqualTo(ContentView.Top());
                    make.CenterY.EqualTo(ContentView.CenterY());
                    make.Bottom.EqualTo(ContentView.Bottom());
                    make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
                });

                _lblReps.MakeConstraints(make =>
                {
                    make.Left.EqualTo(_lblWeight.Right());
                    make.Top.EqualTo(ContentView.Top());
                    make.CenterY.EqualTo(ContentView.CenterY());
                    make.Bottom.EqualTo(ContentView.Bottom());
                    make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
                });

                _lblComplete.MakeConstraints(make =>
                {
                    make.Right.EqualTo(ContentView.Right());
                    make.Top.EqualTo(ContentView.Top());
                    make.CenterY.EqualTo(ContentView.CenterY());
                    make.Bottom.EqualTo(ContentView.Bottom());
                    make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
                });
            }

            private void Initialize()
            {
                _isInitialized = true;

                _lblNumber = new UILabel();
                _lblNumber.Font = UIFont.BoldSystemFontOfSize(12);
                _lblNumber.TextColor = UIColor.Blue;
                _lblNumber.TextAlignment = UITextAlignment.Center;
                _lblNumber.Text = "Set Number";

                _lblWeight = new UILabel();
                _lblWeight.Font = UIFont.BoldSystemFontOfSize(12);
                _lblWeight.TextColor = UIColor.Blue;
                _lblWeight.TextAlignment = UITextAlignment.Center;
                _lblWeight.Text = "Weight";

                _lblReps = new UILabel();
                _lblReps.Font = UIFont.BoldSystemFontOfSize(12);
                _lblReps.TextColor = UIColor.Blue;
                _lblReps.TextAlignment = UITextAlignment.Center;
                _lblReps.Text = "Reps";

                _lblComplete = new UILabel();
                _lblComplete.Font = UIFont.BoldSystemFontOfSize(12);
                _lblComplete.TextColor = UIColor.Blue;
                _lblComplete.TextAlignment = UITextAlignment.Center;
                _lblComplete.Text = "Completed";

                ContentView.BackgroundColor = UIColor.LightGray;
                ContentView.AddSubview(_lblNumber);
                ContentView.AddSubview(_lblWeight);
                ContentView.AddSubview(_lblReps);
                ContentView.AddSubview(_lblComplete);

                SetNeedsUpdateConstraints();
            }
        }

        private class SetTableCell : UITableViewCell
        {
            public SetTableCell(IntPtr handle) : base(handle)
            {
                if (_isInitialized) return;

                Initialize();
            }

            public static string CellIdentifier = "setTableCell";
            private bool _isInitialized = false;
            private UILabel _setNumber;
            private UIButton _setCompletedCheckbox;
            private UITextField _repsField;
            private UITextField _weightField;

            public override void UpdateConstraints()
            {
                base.UpdateConstraints();

                _setNumber.MakeConstraints(make =>
                {
                    make.Left.EqualTo(ContentView.Left()).Offset(5);
                    make.Top.EqualTo(ContentView.Top());
                    make.Bottom.EqualTo(ContentView.Bottom());
                    make.CenterY.EqualTo(ContentView.CenterY());
                    make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
                });

                _weightField.MakeConstraints(make =>
                {
                    make.Right.EqualTo(_repsField.Left());
                    make.Top.EqualTo(ContentView.Top());
                    make.CenterY.EqualTo(ContentView.CenterY());
                    make.Bottom.EqualTo(ContentView.Bottom());
                    make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
                });

                _repsField.MakeConstraints(make =>
                {
                    make.Right.EqualTo(_setCompletedCheckbox.Left());
                    make.Top.EqualTo(ContentView.Top());
                    make.CenterY.EqualTo(ContentView.CenterY());
                    make.Bottom.EqualTo(ContentView.Bottom());
                    make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
                });

                _setCompletedCheckbox.MakeConstraints(make =>
                {
                    make.Right.EqualTo(ContentView.Right());
                    make.Top.EqualTo(ContentView.Top());
                    make.CenterY.EqualTo(ContentView.CenterY());
                    make.Bottom.EqualTo(ContentView.Bottom());
                    make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
                });
            }

            private void Initialize()
            {
                _isInitialized = true;

                _setNumber = new UILabel();
                _setNumber.TextColor = UIColor.Black;
                _setNumber.TextAlignment = UITextAlignment.Center;

                _setCompletedCheckbox = new UIButton();
                _setCompletedCheckbox.SetImage(UIImage.FromBundle("ic_check_box"), UIControlState.Selected);
                _setCompletedCheckbox.SetImage(UIImage.FromBundle("ic_check_box_outline_blank"), UIControlState.Normal);

                _repsField = new UITextField();
                _repsField.TextAlignment = UITextAlignment.Center;

                _weightField = new UITextField();
                _weightField.TextAlignment = UITextAlignment.Center;

                ContentView.AddSubview(_setNumber);
                ContentView.AddSubview(_setCompletedCheckbox);
                ContentView.AddSubview(_repsField);
                ContentView.AddSubview(_weightField);

                SetNeedsUpdateConstraints();
            }

            public void Bind(Set set, string setNumber)
            {
                _setNumber.Text = setNumber;

                if (set.Reps.HasValue)
                    _repsField.Text = set.Reps.Value.ToString();
                else
                    _repsField.Text = "0";

                if (set.Weight.HasValue)
                    _weightField.Text = set.Weight.Value.ToString();
                else
                    _weightField.Text = "0";

            }
        }
    }

}
