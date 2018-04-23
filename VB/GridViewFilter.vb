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
			e.Appearance.FillRectangle(e.Cache, e.Bounds)
			Dim args As New MultiColorDrawStringParams(e.Appearance)
			args.Bounds = e.Bounds
			args.Text = e.DisplayText
			args.Appearance.Assign(e.Appearance)
			Dim apperance As AppearanceObject = _View.PaintAppearance.SelectedRow
			Dim defaultRange As New CharacterRangeWithFormat(0, e.DisplayText.Length, e.Appearance.ForeColor, e.Appearance.BackColor)
			Dim range As New CharacterRangeWithFormat(index, _ActiveFilter.Length, apperance.ForeColor, apperance.BackColor)
			args.Ranges = New CharacterRangeWithFormat() { defaultRange, range }
			paint.MultiColorDrawString(e.Cache, args)
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
