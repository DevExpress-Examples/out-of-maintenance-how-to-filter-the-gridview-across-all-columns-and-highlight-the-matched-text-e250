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
Imports System.Windows.Forms

Namespace WindowsApplication1
	Friend NotInheritable Class Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		Private Sub New()
		End Sub
		<STAThread> _
		Shared Sub Main()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New Form1())
		End Sub
	End Class
End Namespace