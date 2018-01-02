using System;
using Masonry;
using UIKit;

namespace WorkoutLog.iOS.Views
{
    public class FinishWorkoutTableCell : UITableViewCell
    {
        private UILabel _cellLabel;
        public Action AddExerciseAction;
        public static string CellIdentifier = "finishWorkoutTableCell";

        public FinishWorkoutTableCell(IntPtr handle) : base(handle)
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
