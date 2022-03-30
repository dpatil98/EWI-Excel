namespace EWApp
{
    partial class SettingsForm
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
            this.checkedList_visible = new System.Windows.Forms.CheckedListBox();
            this.colNamesBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedList_readOnly = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedList_visible
            // 
            this.checkedList_visible.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkedList_visible.FormattingEnabled = true;
            this.checkedList_visible.Location = new System.Drawing.Point(209, 77);
            this.checkedList_visible.Name = "checkedList_visible";
            this.checkedList_visible.Size = new System.Drawing.Size(56, 199);
            this.checkedList_visible.TabIndex = 0;
            // 
            // colNamesBox
            // 
            this.colNamesBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.colNamesBox.FormattingEnabled = true;
            this.colNamesBox.Location = new System.Drawing.Point(16, 77);
            this.colNamesBox.Name = "colNamesBox";
            this.colNamesBox.Size = new System.Drawing.Size(118, 199);
            this.colNamesBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Settings :";
            // 
            // checkedList_readOnly
            // 
            this.checkedList_readOnly.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkedList_readOnly.FormattingEnabled = true;
            this.checkedList_readOnly.Location = new System.Drawing.Point(148, 77);
            this.checkedList_readOnly.Name = "checkedList_readOnly";
            this.checkedList_readOnly.Size = new System.Drawing.Size(51, 199);
            this.checkedList_readOnly.TabIndex = 3;
            this.checkedList_readOnly.SelectedIndexChanged += new System.EventHandler(this.checkedList_readOnly_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Columns";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(145, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "ReadOnly";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(206, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Hidden";
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_Save.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btn_Save.ForeColor = System.Drawing.Color.LavenderBlush;
            this.btn_Save.Location = new System.Drawing.Point(103, 316);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 7;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 400);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkedList_readOnly);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colNamesBox);
            this.Controls.Add(this.checkedList_visible);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedList_visible;
        private System.Windows.Forms.ListBox colNamesBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedList_readOnly;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Save;
    }
}