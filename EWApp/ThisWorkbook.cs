using Newtonsoft.Json;
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
        private void ThisWorkbook_Startup(object sender, System.EventArgs e)
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
                form_LoadFile webOpen = new form_LoadFile();
                webOpen.ReadExcelAPI();
            }
            
            // throw new Exception("No network adapters with an IPv4 address in the system!");

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
