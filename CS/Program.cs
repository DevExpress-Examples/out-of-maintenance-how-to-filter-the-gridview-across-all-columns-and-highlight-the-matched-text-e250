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
using System.Windows.Forms;

namespace WindowsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}