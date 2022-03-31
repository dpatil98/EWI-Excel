using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace EWApp
{
    public partial class Sheet1
    {
        private void Sheet1_Startup(object sender, System.EventArgs e)
        {
           

        }



        private void Sheet1_Shutdown(object sender, System.EventArgs e)
        {
        }

        
        
        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.BeforeDoubleClick += new Microsoft.Office.Interop.Excel.DocEvents_BeforeDoubleClickEventHandler(this.Sheet1_BeforeDoubleClick);
            this.Startup += new System.EventHandler(this.Sheet1_Startup);
            this.Shutdown += new System.EventHandler(this.Sheet1_Shutdown);

        }

        #endregion

        private void Sheet1_BeforeDoubleClick(Excel.Range Target, ref bool Cancel)
        {
            
            if (Target.Interior.Color == 8421504 || Target.Interior.Color == 255)
            {
                MessageBox.Show("This Column is marked as ReadOnly");
            }
        }
    }
}
