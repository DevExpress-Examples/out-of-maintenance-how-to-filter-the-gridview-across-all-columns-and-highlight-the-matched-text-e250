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

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        const string letters = "qwertyuiopasdfghjklzxcvbnm";
        Random r = new Random();
        public string GetRandomString()
        {
            int start = r.Next(letters.Length - 5) ;
            int length = r.Next(letters.Length - start - 1);
            return letters.Substring(start, length);
        }
        
        private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("Name2", typeof(string));
            tbl.Columns.Add("Name3", typeof(string));
            tbl.Columns.Add("Name4", typeof(string));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { GetRandomString(), GetRandomString(), GetRandomString(), GetRandomString() });
            return tbl;
        }

        GridViewFilterHelper filterHelper;
        public Form1()
        {
            InitializeComponent();
            gridControl1.DataSource = CreateTable(2000);
            filterHelper = new GridViewFilterHelper(gridView1);
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            filterHelper.ActiveFilter = textEdit1.Text;
        }
    }
}