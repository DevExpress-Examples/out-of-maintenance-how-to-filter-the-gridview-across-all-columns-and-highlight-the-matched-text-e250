using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Paint;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;

namespace WindowsApplication1
{
    public class GridViewFilterHelper
    {
        private string _ActiveFilter = string.Empty;
        private GridView _View;
        XPaint paint = new XPaint();
       
        public string ActiveFilter
        {
            get { return _ActiveFilter; }
            set
            {
                if (_ActiveFilter != value)
                    _ActiveFilter = value;
                OnActiveFilterChanged();
            }
        }

        public GridViewFilterHelper(GridView view)
        {
            _View = view;
            _View.CustomDrawCell += new RowCellCustomDrawEventHandler(_View_CustomDrawCell);
        }

        void _View_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (_ActiveFilter == string.Empty)
                return;
            int index = e.DisplayText.IndexOf(_ActiveFilter);
            if (index < 0)
                return;
            e.Handled = true;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            MultiColorDrawStringParams args = new MultiColorDrawStringParams(e.Appearance);
            args.Bounds = e.Bounds;
            args.Text = e.DisplayText;
            args.Appearance.Assign(e.Appearance);
            AppearanceObject apperance = _View.PaintAppearance.SelectedRow;
            CharacterRangeWithFormat defaultRange = new CharacterRangeWithFormat(0, e.DisplayText.Length, e.Appearance.ForeColor, e.Appearance.BackColor);
            CharacterRangeWithFormat range = new CharacterRangeWithFormat(index, _ActiveFilter.Length, apperance.ForeColor, apperance.BackColor);
            args.Ranges = new CharacterRangeWithFormat[] { defaultRange, range };
            paint.MultiColorDrawString(e.Cache, args);
        }

        CriteriaOperator CreateFilterCriteria()
        {
            CriteriaOperator[] operators = new CriteriaOperator[_View.VisibleColumns.Count];
            for (int i = 0; i < _View.VisibleColumns.Count; i++)
            {
                operators[i] = new BinaryOperator(_View.VisibleColumns[i].FieldName, String.Format("%{0}%", _ActiveFilter), BinaryOperatorType.Like);
            }
            return new GroupOperator(GroupOperatorType.Or, operators);

        }

        private void OnActiveFilterChanged()
        {
            _View.ActiveFilterCriteria = CreateFilterCriteria();
        }
    }
}
