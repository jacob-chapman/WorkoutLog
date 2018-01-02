using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using WorkoutLog.Models;

namespace WorkoutLog.iOS.Views
{

    public class SetsTableCell : UITableViewCell
    {
        public SetsTableCell()
        {
            _tableView = new UITableView();
            _dataSource = new SetsTableCellDataSource();

            _tableView.RegisterClassForCellReuse(typeof(SetTableCell), SetsTableCellDataSource.CellIdentifier);
            _tableView.DataSource = _dataSource;
        }

        public static string CellIdentifier = "setsTableCell";
        private UITableView _tableView;
        private SetsTableCellDataSource _dataSource;

        private class SetsTableCellDataSource : UITableViewDataSource
        {
            public List<Set> Sets { get; set; }

            public static string CellIdentifier = "setTableCell";

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

                return cell;
            }

            public override nint RowsInSection(UITableView tableView, nint section)
            {
                return Sets?.Count ?? 0;
            }
        }

        private class SetTableCell : UITableViewCell
        {
            private UILabel _setNumber;
            private UIButton _setCompletedCheckbox;
            private UITextField _repsField;
            private UITextField _weightField;
        }
    }

}
