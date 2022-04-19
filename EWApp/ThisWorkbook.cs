using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EWApp
{
    public partial class ThisWorkbook
    {
        //creating static object of LogViewControl so it can be accessed from form1.cs
        //to write a log
         public static LogViewControl logView = new LogViewControl();
        private void ThisWorkbook_Startup(object sender, System.EventArgs e)
        {
            try
            {

            
            // var remoteIpAddress = HttpContext.GetFeature<IHttpConnectionFeature>()?.RemoteIpAddress;
            HttpClient client = new HttpClient();
            string MyIp=string.Empty;
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                    
                    MyIp = ip.ToString();
                    //MessageBox.Show(ip.ToString());
                    break;
                    }
                }

            Task <string> response = client.GetStringAsync($"{ConfigurationManager.AppSettings["APIUrl"]}GetFileName?key=user{MyIp}");
            
            form_LoadFile.SelectedDropdown = JsonConvert.DeserializeObject(response.Result).ToString();
            if(form_LoadFile.SelectedDropdown != "")
            { 
                Globals.Ribbons.Ribbon1.SetRibbonBtns(true);
                form_LoadFile webOpen = new form_LoadFile();
                webOpen.ReadExcelAPI();
               
                }

                // throw new Exception("No network adapters with an IPv4 address in the system!");

                
                //LogViewControl logView = new LogViewControl();
                logView.AutoSize = true;
               //logView.TopLevel = false;        
                Globals.ThisWorkbook.ActionsPane.Controls.Add(logView);
               //logView.Anchor=(AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left);
                logView.Dock = DockStyle.Fill;
               
                Globals.ThisWorkbook.Application.DisplayDocumentActionTaskPane = true;

            }
            catch (Exception ex)
            {
                form_LoadFile webOpen = new form_LoadFile();
                webOpen.ErrorLogging(String.Format("{0} @ {1}", DateTime.Now, $"Client-Side : Starting Workbook Not able to connect to service  , Error: " + ex.Message));
                MessageBox.Show(ex.Message);
            }
        }

        private void ThisWorkbook_Shutdown(object sender, System.EventArgs e)
        {

        }

        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
        }

        #endregion

    }
}
