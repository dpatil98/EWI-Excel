using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace EWApp
{
    public partial class SettingsForm : Form
    {
        string Selectedfile = form_LoadFile.SelectedDropdown;
        static HttpClient client = new HttpClient();
        public List<string> colNames = new List<string>();
        XElement xelement;
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            
            /*string xmlfilePath = "C:\\CSharp\\EWApp\\EWApp\\bin\\AllFiles\\" + Selectedfile + "\\" + Selectedfile + "_setting.xml";
            xelement = XElement.Load(xmlfilePath);*/

            ReadXML();
              
        }

        private async void UpdateXML()
        {
            List<object> list = new List<Object>();
            int ind = 0;
            list.Add(Selectedfile);
            foreach (string colName in colNames)
            {
                ColData _colName = new ColData();
                _colName.ReadOnly = checkedList_readOnly.GetItemChecked(ind);
                _colName.Hidden = checkedList_visible.GetItemChecked(ind);
                _colName.colName = colName;
                list.Add(_colName);
                ind++;
            }         
            var json = JsonConvert.SerializeObject(list.ToArray());
           /* [ { "colName":"DEPTID","ReadOnly":true,"Hidden":false},
                { "colName":"EMPID","ReadOnly":false,"Hidden":true},
                { "colName":"NAME","ReadOnly":false,"Hidden":false},
                { "colName":"BONUS","ReadOnly":false,"Hidden":false}]*/

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{ConfigurationManager.AppSettings["APIUrl"]}SaveXML";

            HttpResponseMessage response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            //MessageBox.Show(result.ToString());
            if(result == "\"Saved\"")
            {
                form_LoadFile loadform = new form_LoadFile();
                loadform.LoadXML();
            }
            

        }

        private void btn_Save_Click(object sender, EventArgs e)
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
        }

        private void checkedList_readOnly_SelectedIndexChanged(object sender, EventArgs e)
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
                foreach (var col in result)
                {

                    colNamesBox.Items.Add(col.Key.ToString());
                    colNames.Add(col.Key.ToString());
                    checkedList_readOnly.Items.Add(String.Empty, Convert.ToBoolean(col.Value["ReadOnly"]));
                    checkedList_visible.Items.Add(String.Empty, Convert.ToBoolean(col.Value["Hidden"]));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Load a File First");
                this.Close();
            }
          
        }

        internal class ColData
        {      
            public string colName { get; set; }
            public Boolean ReadOnly { get; set; }
            public Boolean Hidden { get; set; }          
            
        }
    }

}
