using Microsoft.Office.Tools.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;


namespace EWApp
{
    public partial class form_LoadFile : Form
    {

        static public string SelectedDropdown;
        static ListObject exelListObject = null;
        static int TotalRows = 0;
        //creating object of datatable present on clientside
        static private DataTable dt = new DataTable();
        static XElement xelement;
        static HttpClient client = new HttpClient();
        //LogView to Add Log into LogActionPane
        LogViewControl LogView = ThisWorkbook.logView;


        public form_LoadFile()
        {
            InitializeComponent();
        }



        public async void ErrorLogging(string errStr)
        {
            try
            {


                HttpContent content = new StringContent(errStr, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{ConfigurationManager.AppSettings["APIUrl"]}HandleClientLogs", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (Exception ex)
            {
                LogView.PrintErrorLog("Error Occure While Logging");
                MessageBox.Show("Network Connection Error");
            }
        }

        //after clicking load file button
        private void form_LoadFile_Load(object sender, EventArgs e)
        {
            try
            {
                //calling GetAllFiles to get all the stored files
                string[] allfiles = GetAllFiles();
                

                //after getting names of the files adding them on dropdown menu
                foreach (string file in allfiles)
                {
                    dropdown_fileSel.Items.Add(file);
                }
                dropdown_fileSel.Items.Add("* Import New File *");
               
            }
            catch (Exception ex)
            {
                LogView.PrintErrorLog(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public string[] GetAllFiles()
        {
            try { 
            
            //using API to get all files stroed 
            Task<string> response = client.GetStringAsync($"{ConfigurationManager.AppSettings["APIUrl"]}GetAllFiles/");
            string[] AllFiles = { };
            //api sends seralized json data 
            string JsonResponse = response.Result;
            var resultn = JsonConvert.DeserializeObject(JsonResponse).ToString();
            var result1 = JsonConvert.DeserializeObject<T>(resultn);
            AllFiles = result1.FileName;
            /*  MessageBox.Show(response.Result);
                MessageBox.Show(AllFiles[0]);*/
            return AllFiles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLogging(String.Format("{0} @ {1}", DateTime.Now, $"Client-Side : When Loading Load Form Error: "+ex.Message));
                return null;
            }
        }

        //reads and display excel data send by API 
        public void OldReadExcelAPI()
        {
            try
            {
                //sends req to service to get data from selected file
                Task<string> response = client.GetStringAsync($"{ConfigurationManager.AppSettings["APIUrl"]}GetExcelData?fileName={SelectedDropdown}");


                string JsonResponse = response.Result;
                var resultn = JsonConvert.DeserializeObject(JsonResponse).ToString();
                //using internal class 'T' for structure 
                //format : <"ExcelData",<"Row", RowData[] > >
                var resultm = JsonConvert.DeserializeObject<T>(resultn);

                //clearing columns ,rows and excelListObject for loading 2nd file
                dt.Columns.Clear();
                dt.Rows.Clear();
                if (exelListObject != null) { exelListObject.Delete(); }

                //storing data from dictionary 
                Dictionary<string, string[]> result = resultm.ExcelData;
                //getting the count of rows to color the cells in LoadXML
                TotalRows = result.Count;

                //using forEach to iterate through excel data
                //and storing row-wise into DataTable dt 
                foreach (string row in result.Keys)
                {
                    if (row == "row1")
                    {
                        /*DataColumn dc = new DataColumn();
                        dc.ColumnName = result[row];
                        dt.Columns.Add(dc);*/

                        for (int i = 0; i < result[row].Length; i++)
                        {
                            dt.Columns.Add(result[row][i]);
                        }
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr.ItemArray = result[row];
                        dt.Rows.Add(dr);
                    }

                    // MessageBox.Show(result[row][0]);
                }

                exelListObject = Globals.Sheet1.Controls.AddListObject(Globals.Sheet1.Range["A1"], SelectedDropdown);
                /*
                string[] columnNames = (from DataColumn x in dt.Columns
                                      select x.ColumnName
                                                ).ToArray();*/
                //dataBinding
                exelListObject.SetDataBinding(dt);
                exelListObject.AutoSetDataBoundColumnHeaders = true;

                LoadXML();
                LogView.PrintActionLog("File Loaded Successfully: " + SelectedDropdown);
            }
            catch (Exception ex)
            {
                LogView.PrintErrorLog(ex.Message);
                ErrorLogging(String.Format("{0} @ {1}", DateTime.Now, $"Client-Side : When Reading And Assigning ExcelData Method:Old-Rowwise Error: " + ex.Message));
                MessageBox.Show(ex.Message);
            }
        }


        public void ReadExcelAPI()
        {
        try
            {
                LogView.PrintActionLog("Loading File : " + SelectedDropdown);
                //clearing columns ,rows and excelListObject for loading 2nd file
                dt.Columns.Clear();
                dt.Rows.Clear();

                //sends req to service to get data from selected file
                var response = client.GetStringAsync($"{ConfigurationManager.AppSettings["APIUrl"]}NewGetExcelData?fileName={SelectedDropdown}");

                string JsonResponse = response.Result;
                var result = JsonConvert.DeserializeObject(JsonResponse).ToString();
                dt = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));

                // DataRow dr = dt.Rows[0];
                int ind = 0;
                foreach(DataColumn col in dt.Columns)
                {
                    col.ColumnName = dt.Rows[0].ItemArray[ind].ToString();
                    ind++;
                }
                dt.Rows[0].Delete();
                //getting TotalRows for LoadXML file
                TotalRows = dt.Rows.Count+1;

                if (exelListObject != null) { exelListObject.Delete(); }


                exelListObject = Globals.Sheet1.Controls.AddListObject(Globals.Sheet1.Range["A1"], SelectedDropdown);
                //dataBinding
                exelListObject.SetDataBinding(dt);
                exelListObject.AutoSetDataBoundColumnHeaders = true;
               // Excel.Worksheet ws = Globals.ThisWorkbook.Sheets.Add(Type.Missing, Globals.ThisWorkbook.Sheets[1], Type.Missing, Type.Missing);
                LoadXML();

                    /* ((Excel.DocEvents_Event)worksheet).Calculate +=
                     new Excel.DocEvents_CalculateEventHandler(worksheet_Calculate);*/
                    LogView.PrintActionLog("File Loaded Successfully: " + SelectedDropdown);
                }
                catch (Exception ex)
                {
                    LogView.PrintErrorLog(ex.Message);
                    ErrorLogging(String.Format("{0} @ {1}", DateTime.Now, $"Client-Side : When Getting And Assigning ExcelData Method:New DatatableAssigning Error: " + ex.Message));
                    MessageBox.Show(ex.Message);
                }
        }

        public void MultiReadExcelAPI()
        {
            try
            {

                //clearing columns ,rows and excelListObject for loading 2nd file
                dt.Columns.Clear();
                dt.Rows.Clear();

                //sends req to service to get data from selected file
                var response = client.GetStringAsync($"{ConfigurationManager.AppSettings["APIUrl"]}NewGetExcelData?fileName={SelectedDropdown}");

                string JsonResponse = response.Result;
                var result = JsonConvert.DeserializeObject(JsonResponse).ToString();
                var listOfWorksheets = (List<DataTable>)JsonConvert.DeserializeObject(result,typeof(List<DataTable>));
                if (exelListObject != null) { exelListObject.Delete(); }

                int sheetNo = 1;
                foreach(DataTable Sheet in listOfWorksheets)
                {
                    // if (exelListObject != null) { exelListObject.Delete(); }
                    //Globals.ThisWorkbook.Worksheets.Add(Type.Missing, Globals.ThisWorkbook.Sheets[sheetNo], Type.Missing, Type.Missing);
                    if (sheetNo > 1) { Globals.ThisWorkbook.Sheets.Add(After: Globals.ThisWorkbook.Sheets[Globals.ThisWorkbook.Sheets.Count]); }
                    //int q = Globals.ThisWorkbook.Worksheets.Count;
                    //Task.Delay(2000).Wait();

                    
                    Worksheet  NewXlWs  = Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application.ActiveSheet);
                    //Worksheet NewXlWs = Globals.Factory.GetVstoObject(Globals.Sheet1.Next);

                    int ind = 0;
                   
                   foreach (DataColumn col in Sheet.Columns)
                   {
                       col.ColumnName = Sheet.Rows[0].ItemArray[ind].ToString();
                       Sheet.Rows[0].Delete();
                       ind++;
                   }

                    
                   exelListObject = NewXlWs.Controls.AddListObject(NewXlWs.Range["A1"], SelectedDropdown + sheetNo);
                   //exelListObject = Globals.Sheet1.Controls.AddListObject(Globals.Sheet1.Range["A1"], SelectedDropdown+sheetNo);
                   exelListObject.SetDataBinding(Sheet);
                   exelListObject.AutoSetDataBoundColumnHeaders = true;
                   sheetNo++;
                }
                // DataRow dr = dt.Rows[0];
    
                //getting TotalRows for LoadXML file
                TotalRows = listOfWorksheets[0].Rows.Count+1;
                // Excel.Worksheet ws = Globals.ThisWorkbook.Sheets.Add(Type.Missing, Globals.ThisWorkbook.Sheets[1], Type.Missing, Type.Missing);
              // LoadXML();

                
            }
            catch (Exception ex)
            {
                LogView.PrintErrorLog(ex.Message);
                ErrorLogging(String.Format("{0} @ {1}", DateTime.Now,$"Client-Side : Getting And Assgining MultiSheet ExcelData , Error: " + ex.Message));
                MessageBox.Show(ex.Message);
            }
        }


        public void LoadXML()
        {
            try
            {

            
            Task<string> response = client.GetStringAsync($"{ConfigurationManager.AppSettings["APIUrl"]}ReadXML?fileName={SelectedDropdown}");

            string JsonResponse = response.Result;
            var resultn = JsonConvert.DeserializeObject(JsonResponse).ToString();
            var resultm = JsonConvert.DeserializeObject<T>(resultn);

            Dictionary<string, Dictionary<string, Boolean>> result = resultm.XMLData;
            Excel.Worksheet ws = Globals.ThisWorkbook.Worksheets[1];
            int ind = 1;
            foreach (var col in result)
            {
                //MessageBox.Show(col.Value["ReadOnly"].ToString());
                dt.Columns[ind-1].ReadOnly = Convert.ToBoolean(col.Value["ReadOnly"]);
                if (Convert.ToBoolean(col.Value["ReadOnly"]))
                {
                   /* ws.Cells[1, ind].AllowEdit = false;*/
                    ws.Cells[1, ind].Interior.Color = Excel.XlRgbColor.rgbRed;
                    ws.Range[ws.Cells[2, ind], ws.Cells[TotalRows, ind]].Interior.Color = Excel.XlRgbColor.rgbGray; //ColorTranslator.FromHtml("#52b69a")
                  
                }
                else
                {
                    ws.Range[ws.Cells[2, ind], ws.Cells[TotalRows, ind]].Interior.Color = Excel.XlRgbColor.rgbGhostWhite;
                    ws.Cells[1, ind].Interior.Color = Excel.XlRgbColor.rgbGreen;                   
                }
                ws.Columns[ind].Hidden = Convert.ToBoolean(col.Value["Hidden"]);
                ind++;

                
                    
                }
            Globals.Ribbons.Ribbon1.SetRibbonBtns(true);
            }catch (Exception ex)
            {
                LogView.PrintErrorLog(ex.Message);
                ErrorLogging(String.Format("{0} @ {1}", DateTime.Now, $"Client-Side : Loading XMlSetting And Appllying to Columns FileName:{SelectedDropdown} ,  Error: " + ex.Message));
                MessageBox.Show(ex.Message);
            }

        }

        public async void  ImportNewFile(string filePath)
        {
            try
            {
                LogView.PrintActionLog("Importing New File" );

                var message = new HttpRequestMessage();
                var content = new MultipartFormDataContent();
                var filestream = new FileStream(filePath, FileMode.Open);
                //var stream = new StreamContent(File.Open(filePath, FileMode.Open));
                var fileName = System.IO.Path.GetFileName(filePath);
                content.Add(new StreamContent(filestream), "file", fileName);
               /* SampleClass sampleClass = new SampleClass();
                JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter
                {
                    SerializerSettings = { TypeNameHandling = TypeNameHandling.Auto }
                };*/
                //content.Add(new ObjectContent(typeof(SampleClass), sampleClass, formatter), "SampleClass");
                //content.Add(stream,fileName);


                message.Method = HttpMethod.Post;
                message.Content = content;
            
                client.DefaultRequestHeaders.Accept.Clear();
                /*formatter = new JsonMediaTypeFormatter
                {
                    SerializerSettings = { TypeNameHandling = TypeNameHandling.Auto }
                };*/
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(@"application/x-www-form-urlencoded"));

                //Task<HttpResponseMessage> taskResponse = client.SendAsync(message);
                HttpResponseMessage taskResponse = await client.PostAsync($"{ConfigurationManager.AppSettings["APIUrl"]}HandleNewFile/", content);
                string result = taskResponse.Content.ReadAsStringAsync().Result.ToString();
           
                if(result== "\"Success\"")
                {
                    LogView.PrintActionLog("File Imported Successfully: " + SelectedDropdown);
                    MessageBox.Show("File Imported Successfully");
                    ReadExcelAPI();
                }
            }
            catch (Exception ex)
            {
                LogView.PrintErrorLog(ex.Message);
                ErrorLogging(String.Format("{0} @ {1}", DateTime.Now, $"Client-Side : Importing New File Error: " + ex.Message));
                MessageBox.Show(ex.Message);
            }
        }

        public async void Save()
        {
            try 
            {
                LogView.PrintActionLog("Saving... " + SelectedDropdown);
                Excel._Worksheet xlWorksheet = Globals.ThisWorkbook.Worksheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;
                // string filePath = "D:\\EW-WEB\\EWApp\\EWAppWorkbook.xlsx";
                List<string> list = new List<string>();
                string[] fileName = { SelectedDropdown };
            
                Dictionary<string, string[]> ExcelData = new Dictionary<string, string[]>();
               // Dictionary<string, Dictionary<string, string[]>> ExcelDataAsjson = new Dictionary<string, Dictionary<string, string[]>>();

                int row = 1;
                int col = 1;
                ExcelData.Add("FileName",fileName);
                //while loops to grab value from imported workbook
                //1st loop will loop till the last row
                //2nd loop will loop till the last columns

                while (xlRange.Cells[row, col] != null && xlRange.Cells[row, col].Value2 != null)
                {
                    //creating new row to assgning data to it.
                    //DataRow dr = dt.NewRow();
                    while (xlRange.Cells[row, col] != null && xlRange.Cells[row, col].Value2 != null)
                    {
                        list.Add(xlRange.Cells[row, col].Value.ToString());
                        col++;
                    }

                    ExcelData.Add($"row{row}", list.ToArray());
                    //clearing list so next row could have its own unique data
                    list.Clear();
                    col = 1;
                    row++;
                }

           
                //ExcelDataAsjson.Add("ExcelData", ExcelData);
           

                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{ConfigurationManager.AppSettings["APIUrl"]}HandleSaveFile");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(ExcelData);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    MessageBox.Show(result);
                }

                LogView.PrintActionLog("File Saved Successfully: " + SelectedDropdown);
                // var data = new StringContent(json, Encoding.UTF8, "application/json");


                /* var url = "http://localhost:8080/ExcelHandler/HandleSaveFile";

                 HttpResponseMessage response = await client.PostAsync(url, data);

                 string result = response.Content.ReadAsStringAsync().Result;
                 MessageBox.Show(result);*/
            }
            catch (Exception ex)
            {
                LogView.PrintErrorLog(ex.Message);
                ErrorLogging(String.Format("{0} @ {1}", DateTime.Now, $"Client-Side : Saving Excel Data Error: " + ex.Message));
                MessageBox.Show(ex.Message);
            }
        }


        private  void  dropdown_fileSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            
                if (dropdown_fileSel.SelectedItem.ToString() == "* Import New File *")
                {
                    // MessageBox.Show("Imprting New File");
                    var fileContent = string.Empty;
                    var filePath = string.Empty;

                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.InitialDirectory = "c:\\";
                        /*openFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx|All files (*.*)|*.*";*/
                        openFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                        openFileDialog.FilterIndex = 1;
                        openFileDialog.RestoreDirectory = true;

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //Get the path of specified file
                            filePath = openFileDialog.FileName;
                            SelectedDropdown = System.IO.Path.GetFileNameWithoutExtension(filePath);
                        
                            ImportNewFile(filePath);
                            this.Close();
                            //for new file we are sending 'true' as 2nd parameter.
                            // ReadExcel(filePath,String.Empty,true);


                        }
                    }
                }
                else
                {
                    SelectedDropdown = dropdown_fileSel.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                LogView.PrintErrorLog(ex.Message);
                ErrorLogging(String.Format("{0} @ {1}", DateTime.Now, $"Client-Side : Selecting File from Dropdown " + ex.Message));
                MessageBox.Show(ex.Message);
            }
        }


        //after clicking on import button
        private void btn_Import_Click(object sender, EventArgs e)
        {
            try
            { 
                //MessageBox.Show("Importing File : " + SelectedDropdown);
                //string filePath = "C:\\CSharp\\EWApp\\EWApp\\bin\\AllFiles\\"+ SelectedDropdown +"\\"+ SelectedDropdown+".xlsx";
                // string xmlfilePath = "C:\\CSharp\\EWApp\\EWApp\\bin\\AllFiles\\" + SelectedDropdown + "\\" + SelectedDropdown + "_setting.xml";
                ReadExcelAPI();
                LogView.PrintActionLog("Importing File: "+SelectedDropdown);
                this.Close();
                //ReadExcel(filePath,xmlfilePath,false);
            }
            catch (Exception ex)
            {
                LogView.PrintErrorLog(ex.Message);
                ErrorLogging(String.Format("{0} @ {1}", DateTime.Now, $"Client-Side : Clicked On Import Button Error: " + ex.Message));
                MessageBox.Show(ex.Message);
            }

        }


        

       /* private void ReadExcel(string filePath, string xmlfilePath, Boolean isNew)
        {
            //opning selected excel file in background 
            Excel.Application app = new Excel.Application();
            Excel.Workbook xlWorkbook = app.Workbooks.Open(filePath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            if (exelListObject != null) { exelListObject.Delete(); }

            //dt was created here 


            //initial values for rows and columns
            int col = 1;
            int row = 1;
            //temporary list to grab values and insert into row
            List<string> list = new List<string>();

            List<string> Clist = new List<string>();

            //while loops to grab value from imported workbook
            //1st loop will loop till the last row
            //2nd loop will loop till the last columns

            while (xlRange.Cells[row, col] != null && xlRange.Cells[row, col].Value2 != null)
            {
                //creating new row to assgning data to it.
                DataRow dr = dt.NewRow();

                while (xlRange.Cells[row, col] != null && xlRange.Cells[row, col].Value2 != null)
                {
                    //if row is 1 then it must be carring columns names.
                    if (row == 1)
                    {
                        dt.Columns.Add(xlRange.Cells[row, col].Value.ToString());
                        Clist.Add(xlRange.Cells[row, col].Value.ToString());
                    }
                    else
                    {
                        //stcking the list with value form every column
                        list.Add(xlRange.Cells[row, col].Value.ToString());
                    }

                    col++;
                }



                if (row != 1)
                {
                    //now converting the stacked list into array..assgning tht array to newly created row
                    dr.ItemArray = list.ToArray();
                    //clearing list so next row could have its own unique data
                    list.Clear();
                    // adding the new row to the datatable
                    dt.Rows.Add(dr);
                }




                col = 1;
                row++;
            }

            // binding the data

            exelListObject = Globals.Sheet1.Controls.AddListObject(Globals.Sheet1.Range["A1"], "Data");

           // string[] columnNames = (from DataColumn x in dt.Columns select x.ColumnName ).ToArray();
            //dataBinding
            exelListObject.SetDataBinding(dt);
            exelListObject.AutoSetDataBoundColumnHeaders = true;
            exelListObject.ListColumns.ToString();

            xelement = XElement.Load(xmlfilePath);
            Reload();



            //if new file is getting imported
            if (isNew)
            {
                CreateEntry(filePath, xlWorkbook, Clist);
            }

            //closing the imported file..after fetching data from it.
            xlWorkbook.Close(true);
        }

        private void CreateEntry(string filePath, Excel.Workbook xlWorkbook, List<string> colNames)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string dir = @"C:\CSharp\EWApp\EWApp\bin\AllFiles\" + fileName;

            // If directory does not exist, create it
            //MessageBox.Show("Dir:"+dir+"FileName"+fileName);
            if (!Directory.Exists(dir))
            {
                //create file in our Database
                Directory.CreateDirectory(dir);
                xlWorkbook.SaveAs(dir + $@"\{fileName}", Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);

                //creating new xml file
                XDocument doc = new XDocument(new XElement("SettingGroup"));

                foreach (string colName in colNames)
                {
                    doc.Element("SettingGroup").Add(new XElement(colName, new XAttribute("colName", colName)
                                                                          , new XAttribute("ReadOnly", false)
                                                                          , new XAttribute("Hidden", false)
                                                    ));
                }

                doc.Save(dir + $@"\{fileName}_setting.xml");
            }
            else
            {
                MessageBox.Show("File With Same Name Already Present😅");
            }
        }

        public void Reload()
        {

            string xmlfilePath = "C:\\CSharp\\EWApp\\EWApp\\bin\\AllFiles\\" + SelectedDropdown + "\\" + SelectedDropdown + "_setting.xml";
            xelement = XElement.Load(xmlfilePath);
            IEnumerable<XElement> settingGroup = xelement.Elements();
            Excel.Worksheet ws = Globals.ThisWorkbook.Worksheets[1];
            int ind = 1;

            foreach (var setting in settingGroup)
            {
                dt.Columns[setting.Attribute("colName").Value.ToString()].ReadOnly = Convert.ToBoolean(setting.Attribute("ReadOnly").Value);
                //if column is readonly change the BGcolor of cell
                if (Convert.ToBoolean(setting.Attribute("ReadOnly").Value))
                {
                    var columnHeadingsRange = ws.Cells[1, ind];
                    columnHeadingsRange.Interior.Color = Excel.XlRgbColor.rgbRed;
                }
                else
                {
                    var columnHeadingsRange = ws.Cells[1, ind];
                    columnHeadingsRange.Interior.Color = Excel.XlRgbColor.rgbGreen;
                }

                ws.Columns[ind].Hidden = Convert.ToBoolean(setting.Attribute("Hidden").Value);


                ind++;
            }



        }*/


    }
    internal class T
    {
        public string[] FileName { get; set; }
        public Dictionary<string, string[]> ExcelData { get; set; }
        public Dictionary<string, Dictionary<string, Boolean>> XMLData { get; set; }

        public Dictionary<string, string[][]> XlData { get; set; }
    }

   

}

//a < b * a + b < a * b 