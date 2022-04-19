using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EWApp
{
    public  partial class LogViewControl : UserControl
    {
       
        public LogViewControl()
        {
            InitializeComponent();
            //this.ActionRTextBox.Enabled = false;
            //this.ErrorLogsTextBox.Enabled = false;
            this.ErrorLogsTextBox.ReadOnly = true;
            this.ErrorLogsTextBox.ScrollBars = ScrollBars.Both;
            this.ActionRTextBox.ReadOnly = true;
            this.ActionRTextBox.ScrollBars = ScrollBars.Both;
        }
        
        public  void ShowLogText(string log)
        {
            PrintActionLog(log);   
        }

        public void PrintActionLog(string Data)
        {
           this.ActionRTextBox.Text += Data + Environment.NewLine;
        }

        public void PrintErrorLog(string Data)
        {
            this.ErrorLogsTextBox.Text += Data + Environment.NewLine;
        }

        private void ActionRTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
