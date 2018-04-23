' Developer Express Code Central Example:
' How to filter the GridView across all columns and highlight the matched text
' 
' This example demonstrates how to show only those rows in the GridView whose
' cells contain a specified text. Matched text is highlighted using the
' MultiColorDrawString method.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2508


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
Imports DevExpress.Utils.Paint
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Namespace WindowsApplication1
	Public Class GridViewFilterHelper
		Private _ActiveFilter As String = String.Empty
		Private _View As GridView
		Private paint As New XPaint()

		Public Property ActiveFilter() As String
			Get
				Return _ActiveFilter
			End Get
			Set(ByVal value As String)
				If _ActiveFilter <> value Then
					_ActiveFilter = value
				End If
				OnActiveFilterChanged()
			End Set
		End Property

		Public Sub New(ByVal view As GridView)
			_View = view
			AddHandler _View.CustomDrawCell, AddressOf _View_CustomDrawCell
		End Sub

		Private Sub _View_CustomDrawCell(ByVal sender As Object, ByVal e As RowCellCustomDrawEventArgs)
			If _ActiveFilter = String.Empty Then
				Return
			End If
			Dim index As Integer = e.DisplayText.IndexOf(_ActiveFilter)
			If index < 0 Then
				Return
			End If
			e.Handled = True
			Dim inf = (TryCast(e.Cell, GridCellInfo))
			Dim vi As TextEditViewInfo = TryCast(inf.ViewInfo, TextEditViewInfo)
			e.Appearance.FillRectangle(e.Cache, e.Bounds)
			Dim args As New MultiColorDrawStringParams(e.Appearance)
			args.Bounds = e.Bounds
			args.Text = e.DisplayText
			args.Appearance.Assign(e.Appearance)
			Dim apperance As AppearanceDefault = LookAndFeelHelper.GetHighlightSearchAppearance(vi.LookAndFeel, (Not vi.UseHighlightSearchAppearance))
			e.Cache.Paint.DrawMultiColorString(e.Cache, e.Bounds, e.DisplayText, _ActiveFilter, vi.PaintAppearance, vi.PaintAppearance.GetTextOptions().GetStringFormat(vi.DefaultTextOptions), apperance.ForeColor, apperance.BackColor, False, index)
		End Sub

		Private Function CreateFilterCriteria() As CriteriaOperator
			Dim operators(_View.VisibleColumns.Count - 1) As CriteriaOperator
			For i As Integer = 0 To _View.VisibleColumns.Count - 1
				operators(i) = New BinaryOperator(_View.VisibleColumns(i).FieldName, String.Format("%{0}%", _ActiveFilter), BinaryOperatorType.Like)
			Next i
			Return New GroupOperator(GroupOperatorType.Or, operators)

		End Function

		Private Sub OnActiveFilterChanged()
			_View.ActiveFilterCriteria = CreateFilterCriteria()
		End Sub
	End Class
End Namespace
