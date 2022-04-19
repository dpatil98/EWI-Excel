using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EWApp
{

    public partial class NewSettingForm : Form
    {
        string Selectedfile = form_LoadFile.SelectedDropdown;
        static HttpClient client = new HttpClient();
        public List<string> colNames = new List<string>();
        XElement xelement;
        form_LoadFile ErrorLog = new form_LoadFile();
        LogViewControl LogView = ThisWorkbook.logView;
       // List<SettingInfo> colObjList = new List<SettingInfo>();
        List<object> colObjList = new List<Object>();

        public NewSettingForm()
        {
            InitializeComponent();
        }

        private void NewSettingForm_Load(object sender, EventArgs e)
        {
            
                      /*DataGridViewTextBoxColumn pid = new DataGridViewTextBoxColumn();
                        pid.Name = "ProjectId";
                        pid.HeaderText = "Project Id";
                        pid.DataPropertyName = "ProjectId";
                        this.dataGridView1.Columns.Insert(0, pid);

                        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                        checkBoxColumn.HeaderText = "";
                        checkBoxColumn.ValueType = typeof(bool);
                        checkBoxColumn.Width = 30;
                        checkBoxColumn.Name = "checkBoxColumn";
                        checkBoxColumn.TrueValue = true;
                        checkBoxColumn.FalseValue = false;
                        checkBoxColumn.ReadOnly = false;
                        checkBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                       this.dataGridView1.Columns.Insert(1, checkBoxColumn);*/

            ReadXML();
        }

        /* private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
         {
             *//* tableLayoutPanel1.RowStyles.Add(SizeType.Absolute, 52F);
              tableLayoutPanel1.ColumnStyles.Add(SizeType.Percent, 30F);*//*
             tableLayoutPanel1.Controls.Add(new Label() { Text = "assa" }, 0, 0);
         }*/

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        //Reads XML when setting from is loaded and gives value to setting form
        private void ReadXML()
        {
            try
            {

                Task<string> response = client.GetStringAsync($"{ConfigurationManager.AppSettings["APIUrl"]}ReadXML?fileName={Selectedfile}");

                string JsonResponse = response.Result;
                var resultn = JsonConvert.DeserializeObject(JsonResponse).ToString();
                var resultm = JsonConvert.DeserializeObject<T>(resultn);

                Dictionary<string, Dictionary<string, Boolean>> result = resultm.XMLData;

                //   IEnumerable<XElement> settingGroup = xelement.Elements();

                colObjList.Insert(0, Selectedfile);
                SettingInfo Obj = new SettingInfo();
                //List<SettingInfo> colObjList = new List<SettingInfo>();
                foreach (var col in result)
                {
                    Obj = new SettingInfo();
                    Obj.ColumnName = col.Key.ToString();
                    Obj.ReadOnly = Convert.ToBoolean(col.Value["ReadOnly"]);
                    Obj.Hidden = Convert.ToBoolean(col.Value["Hidden"]);
                    colObjList.Add(Obj);
                    colNames.Add(col.Key.ToString());

                    /* colNamesBox.Items.Add(col.Key.ToString());
                     
                     checkedList_readOnly.Items.Add(String.Empty, Convert.ToBoolean(col.Value["ReadOnly"]));
                     checkedList_visible.Items.Add(String.Empty, Convert.ToBoolean(col.Value["Hidden"]));*/

                }
                //removing fileName from list so objectProp can assign to settings
                //again adding it when saving file
                colObjList.RemoveAt(0);
                this.dataGridView1.DataSource = colObjList;
                this.dataGridView1.Columns[0].ReadOnly = true;
                
            }
            catch (Exception ex)
            {
                LogView.PrintErrorLog(ex.Message);
                ErrorLog.ErrorLogging(String.Format("{0} @ {1}", DateTime.Now, $"Client-Side : Reading XML Data,  Form:Setting , Error: " + ex.Message));
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }


        private async void UpdateXML()
        {
            try
            {
                colObjList.Insert(0, Selectedfile);
                var json = JsonConvert.SerializeObject(colObjList.ToArray());
                /*[ { "colName":"DEPTID","ReadOnly":true,"Hidden":false},
                     { "colName":"EMPID","ReadOnly":false,"Hidden":true},
                     { "colName":"NAME","ReadOnly":false,"Hidden":false},
                     { "colName":"BONUS","ReadOnly":false,"Hidden":false}]*/

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{ConfigurationManager.AppSettings["APIUrl"]}SaveXML";

                HttpResponseMessage response = await client.PostAsync(url, data);

                string result = response.Content.ReadAsStringAsync().Result;
                //MessageBox.Show(result.ToString());
                if (result == "\"Saved\"")
                {
                    form_LoadFile loadform = new form_LoadFile();
                    loadform.LoadXML();
                    LogView.PrintActionLog("Setting Saved Successfully: " + Selectedfile);
                }

            }
            catch (Exception ex)
            {
                LogView.PrintErrorLog(ex.Message);
                ErrorLog.ErrorLogging(String.Format("{0} @ {1}", DateTime.Now, $"Client-Side : Updating XML Data Form:Setting , Error: " + ex.Message));
                MessageBox.Show(ex.Message);
            }
        }


        private void settingSaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateXML();

                /*IEnumerable<XElement> settingGroup = xelement.Elements();
                int ind = 0;
                foreach (XElement setting in settingGroup)
                {
                    setting.Attribute("ReadOnly").Value=checkedList_readOnly.GetItemChecked(ind).ToString();
                    setting.Attribute("Hidden").Value=checkedList_visible.GetItemChecked(ind).ToString();
                    ind++;
                }

                xelement.Save("C:\\CSharp\\EWApp\\EWApp\\bin\\AllFiles\\" + Selectedfile + "\\" + Selectedfile + "_setting.xml");*/
                this.Visible = false;
                LogView.PrintActionLog("File Saved Successfully: " + Selectedfile);
            }
            catch (Exception ex)
            {
                LogView.PrintErrorLog(ex.Message);
                ErrorLog.ErrorLogging(String.Format("{0} @ {1}", DateTime.Now, $"Client-Side : Clicked On Save button Form:Setting , Error: " + ex.Message));
                MessageBox.Show(ex.Message);
            }
        }

        public class SettingInfo
        {

            public string ColumnName { get; set; }
            public bool ReadOnly { get; set; }
            public bool Hidden { get; set; }

        }

    }
}
