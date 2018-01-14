using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using WorkoutLog.Models;
using WorkoutLog.ViewModels;
using Masonry;
using HealthKitUI;
using System.Runtime.InteropServices;

namespace WorkoutLog.iOS.Views
{
    public class SetsHeaderTableCell : UITableViewCell
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
                make.Left.EqualTo(ContentView.Left());
                make.CenterY.EqualTo(ContentView.CenterY());
                make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
            });

            _lblWeight.MakeConstraints(make =>
            {
                make.Left.EqualTo(_lblNumber.Right());
                make.CenterY.EqualTo(ContentView.CenterY());
                make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
            });

            _lblReps.MakeConstraints(make =>
            {
                make.Left.EqualTo(_lblWeight.Right());
                make.CenterY.EqualTo(ContentView.CenterY());
                make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
            });

            _lblComplete.MakeConstraints(make =>
            {
                make.Right.EqualTo(ContentView.Right());
                make.CenterY.EqualTo(ContentView.CenterY());
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

        //todo
        public void Bind(SetHeaderViewModel viewModel)
        {

        }
    }

    public class SetTableCell : UITableViewCell
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
                make.CenterY.EqualTo(ContentView.CenterY());
                make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
            });

            _weightField.MakeConstraints(make =>
            {
                make.Right.EqualTo(_repsField.Left());
                make.CenterY.EqualTo(ContentView.CenterY());
                make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
            });

            _repsField.MakeConstraints(make =>
            {
                make.Right.EqualTo(_setCompletedCheckbox.Left());
                make.CenterY.EqualTo(ContentView.CenterY());
                make.Width.EqualTo(ContentView.Width()).MultipliedBy(0.25f);
            });

            _setCompletedCheckbox.MakeConstraints(make =>
            {
                make.Right.EqualTo(ContentView.Right());
                make.CenterY.EqualTo(ContentView.CenterY());
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

        public void Bind(SetViewModel setViewModel, string setNumber)
        {
            _setNumber.Text = setNumber;

            var set = setViewModel.Set;

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

    public class AddSetTableCell : UITableViewCell
    {
        public AddSetTableCell(IntPtr handle) : base(handle)
        {
            if (_isInitialized) return;

            Initialize();
        }

        private bool _isInitialized = false;
        public UILabel _lblAddSet;

        private void Initialize()
        {
            _lblAddSet = new UILabel();
            _lblAddSet.TextColor = UIColor.Black;
            _lblAddSet.TextAlignment = UITextAlignment.Center;
        }

        public void Bind(AddSetViewModel viewModel)
        {
            _lblAddSet.Text = viewModel.Text;
        }
    }
}
