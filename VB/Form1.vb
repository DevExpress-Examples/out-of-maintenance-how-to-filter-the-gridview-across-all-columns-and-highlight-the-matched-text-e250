Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits Form
		Private Const letters As String = "qwertyuiopasdfghjklzxcvbnm"
		Private r As New Random()
		Public Function GetRandomString() As String
			Dim start As Integer = r.Next(letters.Length - 5)
			Dim length As Integer = r.Next(letters.Length - start - 1)
			Return letters.Substring(start, length)
		End Function

		Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("Name2", GetType(String))
			tbl.Columns.Add("Name3", GetType(String))
			tbl.Columns.Add("Name4", GetType(String))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { GetRandomString(), GetRandomString(), GetRandomString(), GetRandomString() })
			Next i
			Return tbl
		End Function

		Private filterHelper As GridViewFilterHelper
		Public Sub New()
			InitializeComponent()
			gridControl1.DataSource = CreateTable(2000)
			filterHelper = New GridViewFilterHelper(gridView1)
		End Sub

		Private Sub textEdit1_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles textEdit1.EditValueChanged
			filterHelper.ActiveFilter = textEdit1.Text
		End Sub
	End Class
End Namespace