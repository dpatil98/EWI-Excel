namespace EWApp
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ribbon1));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btn_Load = this.Factory.CreateRibbonButton();
            this.btn_Save = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btn_Setting = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.group2);
            this.tab1.Label = "EWApp";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btn_Load);
            this.group1.Items.Add(this.btn_Save);
            this.group1.Label = "Functions";
            this.group1.Name = "group1";
            // 
            // btn_Load
            // 
            this.btn_Load.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btn_Load.Image = ((System.Drawing.Image)(resources.GetObject("btn_Load.Image")));
            this.btn_Load.Label = "Load";
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.ShowImage = true;
            this.btn_Load.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_Load_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.Label = "Save";
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.ShowImage = true;
            this.btn_Save.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_Save_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.btn_Setting);
            this.group2.Label = "group2";
            this.group2.Name = "group2";
            // 
            // btn_Setting
            // 
            this.btn_Setting.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btn_Setting.Image = ((System.Drawing.Image)(resources.GetObject("btn_Setting.Image")));
            this.btn_Setting.Label = "Settings";
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.ShowImage = true;
            this.btn_Setting.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_Setting_Click_1);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Load;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Setting;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Save;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
