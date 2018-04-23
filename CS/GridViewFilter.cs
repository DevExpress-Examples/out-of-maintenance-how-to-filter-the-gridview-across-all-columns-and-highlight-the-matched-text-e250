// Developer Express Code Central Example:
// How to filter the GridView across all columns and highlight the matched text
// 
// This example demonstrates how to show only those rows in the GridView whose
// cells contain a specified text. Matched text is highlighted using the
// MultiColorDrawString method.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2508

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
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.LookAndFeel;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

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
            var inf = (e.Cell as GridCellInfo);
            TextEditViewInfo vi = inf.ViewInfo as TextEditViewInfo;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            MultiColorDrawStringParams args = new MultiColorDrawStringParams(e.Appearance);
            args.Bounds = e.Bounds;
            args.Text = e.DisplayText;
            args.Appearance.Assign(e.Appearance);
            AppearanceDefault apperance = LookAndFeelHelper.GetHighlightSearchAppearance(vi.LookAndFeel, !vi.UseHighlightSearchAppearance);
            e.Cache.Paint.DrawMultiColorString(e.Cache, e.Bounds, e.DisplayText, _ActiveFilter, vi.PaintAppearance, vi.PaintAppearance.GetTextOptions().GetStringFormat(vi.DefaultTextOptions),
                apperance.ForeColor, apperance.BackColor, false, index);
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
