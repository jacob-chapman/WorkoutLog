using System;
using UIKit;
using System.Collections.Generic;
using Foundation;
using Masonry;
using ObjCRuntime;
using WorkoutLog.Services;
using WorkoutLog.Models;
using WorkoutLog.ViewModels;
using WorkoutLog.Presenters;

namespace WorkoutLog.iOS.Controllers
{
    public class CreateExerciseViewController : UIViewController, IUITextFieldDelegate
    {
        public CreateExerciseViewController(AddExercisePresenter presenter)
        {
            _presenter = presenter;
        }

        private AddExercisePresenter _presenter;

        private const int _titleTag = 101;
        private const int _muscleGroupTag = 102;
        private const int _exerciseTypeTag = 103;

        private UITextField _title = new UITextField()
        {
            Placeholder = "Exercise Title",
            BackgroundColor = UIColor.White,
            TextColor = UIColor.Blue,
            Tag = _titleTag,
            AutocapitalizationType = UITextAutocapitalizationType.Words,
            AutocorrectionType = UITextAutocorrectionType.Yes
        };
        private UITextField _muscleGroup = new UITextField()
        {
            Placeholder = "Muscle Group",
            BackgroundColor = UIColor.White,
            TextColor = UIColor.Blue,
            Tag = _titleTag
        };
        private UITextField _exerciseType = new UITextField()
        {
            Placeholder = "Exercise Type",
            BackgroundColor = UIColor.White,
            TextColor = UIColor.Blue,
            Tag = _titleTag
        };

        private UIPickerView _muscleGroupPicker = new UIPickerView();
        private UIPickerView _exerciseTypePicker = new UIPickerView();

        private ExerciseType _selectedExerciseType = ExerciseType.None;
        private MuscleGroup _selectedMuscleGroup = MuscleGroup.None;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Close", UIBarButtonItemStyle.Plain, (sender, e) =>
            {
                NavigationController?.DismissViewController(true, null);
            });

            NavigationItem.RightBarButtonItem = new UIBarButtonItem("Save", UIBarButtonItemStyle.Done,
            (sender, e) =>
            {
                NavigationController?.DismissViewController(true, null);

                _presenter.CreateExercise(new Exercise()
                {
                    MuscleGroup = _selectedMuscleGroup,
                    ExerciseType = _selectedExerciseType,
                    Title = _title.Text
                });
            });

            View.BackgroundColor = UIColor.Black;

            _title.Delegate = this;
            _muscleGroup.Delegate = this;
            _exerciseType.Delegate = this;

            _muscleGroupPicker.Model = new GenericPickerViewModel<MuscleGroup>()
            {
                Items = new List<MuscleGroup>()
                {
                    MuscleGroup.Back,
                    MuscleGroup.Biceps,
                    MuscleGroup.Cardio,
                    MuscleGroup.Chest,
                    MuscleGroup.Core,
                    MuscleGroup.Legs,
                    MuscleGroup.Shoulders,
                    MuscleGroup.Traps,
                    MuscleGroup.Triceps,
                    MuscleGroup.None
                },
                OnSelected = (obj) =>
                {
                    _selectedMuscleGroup = obj;
                    _muscleGroup.Text = obj.ToString();
                    _muscleGroup.ResignFirstResponder();
                }
            };
            _exerciseTypePicker.Model = new GenericPickerViewModel<ExerciseType>()
            {
                Items = new List<ExerciseType>()
                {
                    ExerciseType.Barbell,
                    ExerciseType.Body,
                    ExerciseType.Dumbbell,
                    ExerciseType.Machine,
                    ExerciseType.Other,
                    ExerciseType.None
                },
                OnSelected = (obj) =>
                {
                    _selectedExerciseType = obj;
                    _exerciseType.Text = obj.ToString();
                    _exerciseType.ResignFirstResponder();
                }
            };

            _muscleGroup.InputView = _muscleGroupPicker;
            _exerciseType.InputView = _exerciseTypePicker;

            View.AddSubview(_title);
            View.AddSubview(_muscleGroup);
            View.AddSubview(_exerciseType);
            //View.AddSubview(_muscleGroupPicker);
            //View.AddSubview(_exerciseTypePicker);

            Initialize();
        }


        private void Initialize()
        {
            _title.MakeConstraints(make =>
            {
                make.Left.EqualTo(View.Left());
                make.Right.EqualTo(View.Right());
                make.Top.EqualTo(View.Top()).Offset(NavigationController.NavigationBar.Frame.Height + 25);
                make.Height.Offset(43);
            });

            _muscleGroup.MakeConstraints(make =>
            {
                make.Left.EqualTo(View.Left());
                make.Right.EqualTo(View.Right());
                make.Top.EqualTo(_title.Bottom()).Offset(15);
                make.Height.Offset(43);
            });

            _exerciseType.MakeConstraints(make =>
            {
                make.Left.EqualTo(View.Left());
                make.Right.EqualTo(View.Right());
                make.Top.EqualTo(_muscleGroup.Bottom()).Offset(15);
                make.Height.Offset(43);
            });
        }

        #region IUITextFieldDelegate

        [Export("textFieldShouldBeginEditing:")]
        public bool ShouldBeginEditing(UITextField textField)
        {
            return true;
        }

        [Export("textFieldDidBeginEditing:")]
        public void EditingStarted(UITextField textField)
        {
            return;
        }

        #endregion IUITextFieldDelegate

    }


    public class GenericPickerViewModel<T> : UIPickerViewModel
    {
        public List<T> Items { get; set; }

        public Action<T> OnSelected;

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return Items?.Count ?? 0;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            var item = Items[(int)row] as Enum;

            return item.ToString();
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            var item = Items[(int)row];

            OnSelected?.Invoke(item);
        }
    }
}
