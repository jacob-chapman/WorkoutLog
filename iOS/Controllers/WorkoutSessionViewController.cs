using System;
using UIKit;
namespace WorkoutLog.iOS.Controllers
{
    public class WorkoutSessionViewController : UIViewController
    {
        public WorkoutSessionViewController()
        {
        }

        #region Lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Nav menu stuff
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Close", UIBarButtonItemStyle.Plain, (sender, e) =>
            {
                NavigationController?.DismissViewController(true, null);
            });

            View.BackgroundColor = UIColor.Black;
        }

        #endregion Lifecycle
    }
}
