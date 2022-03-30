namespace EWApp
{
    partial class form_LoadFile
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dropdown_fileSel = new System.Windows.Forms.ComboBox();
            this.btn_Import = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dropdown_fileSel
            // 
            this.dropdown_fileSel.FormattingEnabled = true;
            this.dropdown_fileSel.Location = new System.Drawing.Point(85, 119);
            this.dropdown_fileSel.Name = "dropdown_fileSel";
            this.dropdown_fileSel.Size = new System.Drawing.Size(436, 21);
            this.dropdown_fileSel.TabIndex = 0;
            this.dropdown_fileSel.SelectedIndexChanged += new System.EventHandler(this.dropdown_fileSel_SelectedIndexChanged);
            // 
            // btn_Import
            // 
            this.btn_Import.Location = new System.Drawing.Point(527, 117);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(75, 23);
            this.btn_Import.TabIndex = 1;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // form_LoadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 271);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.dropdown_fileSel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "form_LoadFile";
            this.Text = "Load File";
            this.Load += new System.EventHandler(this.form_LoadFile_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox dropdown_fileSel;
        private System.Windows.Forms.Button btn_Import;
    }
}