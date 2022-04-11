using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EWApp
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            //gets true after Laoding excel and xml file successfully in LoadXML function.
            if(form_LoadFile.SelectedDropdown != "")
            {
               SetRibbonBtns(true);
            }
            else
            {
               SetRibbonBtns(false);
            }
            
        }

        private void btn_Load_Click(object sender, RibbonControlEventArgs e)
        {
            form_LoadFile form = new form_LoadFile();
            form.ShowDialog();
        }

       /* private void btn_Setting_Click(object sender, RibbonControlEventArgs e)
        {
              

        }*/

        public void SetRibbonBtns(bool isEnabled)
        {
            btn_Setting.Enabled = isEnabled;
            btn_Save.Enabled = isEnabled;
           // btn_Setting.Visible = isEnabled;
        }

        private void btn_Setting_Click_1(object sender, RibbonControlEventArgs e)
        {
            SettingsForm s_form = new SettingsForm();
            s_form.ShowDialog(); 
            
        }

        private void btn_Save_Click(object sender, RibbonControlEventArgs e)
        {
           form_LoadFile s_form = new form_LoadFile();
            s_form.Save();
        }
    }
}
