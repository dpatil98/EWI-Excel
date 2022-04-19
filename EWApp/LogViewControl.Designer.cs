namespace EWApp
{
    partial class LogViewControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.LogViewerTabs = new System.Windows.Forms.TabControl();
            this.ActionResult = new System.Windows.Forms.TabPage();
            this.ActionRTextBox = new System.Windows.Forms.TextBox();
            this.ErrorLogs = new System.Windows.Forms.TabPage();
            this.ErrorLogsTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LogViewerTabs.SuspendLayout();
            this.ActionResult.SuspendLayout();
            this.ErrorLogs.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogViewerTabs
            // 
            this.LogViewerTabs.Controls.Add(this.ActionResult);
            this.LogViewerTabs.Controls.Add(this.ErrorLogs);
            this.LogViewerTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogViewerTabs.Location = new System.Drawing.Point(0, 0);
            this.LogViewerTabs.Name = "LogViewerTabs";
            this.LogViewerTabs.SelectedIndex = 0;
            this.LogViewerTabs.Size = new System.Drawing.Size(417, 150);
            this.LogViewerTabs.TabIndex = 0;
            // 
            // ActionResult
            // 
            this.ActionResult.Controls.Add(this.ActionRTextBox);
            this.ActionResult.Location = new System.Drawing.Point(4, 22);
            this.ActionResult.Name = "ActionResult";
            this.ActionResult.Padding = new System.Windows.Forms.Padding(3);
            this.ActionResult.Size = new System.Drawing.Size(409, 124);
            this.ActionResult.TabIndex = 0;
            this.ActionResult.Text = "ActionResult";
            this.ActionResult.UseVisualStyleBackColor = true;
            // 
            // ActionRTextBox
            // 
            this.ActionRTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActionRTextBox.Location = new System.Drawing.Point(3, 3);
            this.ActionRTextBox.Multiline = true;
            this.ActionRTextBox.Name = "ActionRTextBox";
            this.ActionRTextBox.Size = new System.Drawing.Size(403, 118);
            this.ActionRTextBox.TabIndex = 1;
            this.ActionRTextBox.TextChanged += new System.EventHandler(this.ActionRTextBox_TextChanged);
            // 
            // ErrorLogs
            // 
            this.ErrorLogs.Controls.Add(this.ErrorLogsTextBox);
            this.ErrorLogs.Location = new System.Drawing.Point(4, 22);
            this.ErrorLogs.Name = "ErrorLogs";
            this.ErrorLogs.Padding = new System.Windows.Forms.Padding(3);
            this.ErrorLogs.Size = new System.Drawing.Size(409, 124);
            this.ErrorLogs.TabIndex = 1;
            this.ErrorLogs.Text = "ErrorLogs";
            this.ErrorLogs.UseVisualStyleBackColor = true;
            // 
            // ErrorLogsTextBox
            // 
            this.ErrorLogsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorLogsTextBox.Location = new System.Drawing.Point(3, 3);
            this.ErrorLogsTextBox.Multiline = true;
            this.ErrorLogsTextBox.Name = "ErrorLogsTextBox";
            this.ErrorLogsTextBox.Size = new System.Drawing.Size(403, 112);
            this.ErrorLogsTextBox.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // LogViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LogViewerTabs);
            this.Name = "LogViewControl";
            this.Size = new System.Drawing.Size(417, 150);
            this.LogViewerTabs.ResumeLayout(false);
            this.ActionResult.ResumeLayout(false);
            this.ActionResult.PerformLayout();
            this.ErrorLogs.ResumeLayout(false);
            this.ErrorLogs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl LogViewerTabs;
        private System.Windows.Forms.TabPage ActionResult;
        private System.Windows.Forms.TextBox ActionRTextBox;
        private System.Windows.Forms.TabPage ErrorLogs;
        private System.Windows.Forms.TextBox ErrorLogsTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    }
}
