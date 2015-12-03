namespace WindowsFormsApplication1
{
    partial class dlgProc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer proc_components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected void Dispose_Proc(bool disposing)
        {
            if (disposing && (proc_components != null))
            {
                proc_components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponentProc()
        {
            bnCancel = new System.Windows.Forms.Button();
            bnOK = new System.Windows.Forms.Button();
            niTray2 = new System.Windows.Forms.NotifyIcon();
            cbList = new System.Windows.Forms.ComboBox();
            SuspendLayout();
            // 
            // bnCancel
            // 
            bnCancel.Location = new System.Drawing.Point(373, 66);
            bnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            bnCancel.Name = "bnCancel";
            bnCancel.Size = new System.Drawing.Size(100, 28);
            bnCancel.TabIndex = 0;
            bnCancel.Text = "Cancel";
            bnCancel.UseVisualStyleBackColor = true;
            bnCancel.Click += new System.EventHandler(bnCancel_Click);
            // 
            // bnOK
            // 
            bnOK.Location = new System.Drawing.Point(137, 66);
            bnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            bnOK.Name = "bnOK";
            bnOK.Size = new System.Drawing.Size(100, 28);
            bnOK.TabIndex = 0;
            bnOK.Text = "OK";
            bnOK.UseVisualStyleBackColor = true;
            bnOK.Click += new System.EventHandler(bnOK_Click);
            //
            // cbList.
            //
            cbList.Location = new System.Drawing.Point(4, 26);
            cbList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            cbList.Name = "cbList";
            cbList.Size = new System.Drawing.Size(591, 251);
            cbList.TabIndex = 0;
            for (int i = 1; true; i++)
            {
                string s = System.Configuration.ConfigurationManager.AppSettings["Proc" + i];
                if (s != null)
                {
                    cbList.Items.Add(s);
                }
                else
                {
                    break;
                }
            }
            //
            // niTray2
            //
            niTray2.DoubleClick += new System.EventHandler(notifyIcon_MouseDoubleClick);
            niTray2.Icon = ((System.Drawing.Icon)(Properties.Resources.Icon)); //The Tray2 icon to use
            niTray2.BalloonTipText = "Minimized to System Tray.";
            niTray2.BalloonTipTitle = "Double click to restore.";
            // 
            // dlgProc
            // 
            Resize += new System.EventHandler(frmMain_Resize);
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(601, 297);
            Controls.Add(bnCancel);
            Controls.Add(bnOK);
            Controls.Add(cbList);
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            Name = "dlgProc";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "KeyenceToCSV Process Selection";
            ResumeLayout(false);
        }

        private System.Windows.Forms.ComboBox cbList;
        private System.Windows.Forms.Button bnOK;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.NotifyIcon niTray2;
        private System.ComponentModel.IContainer components;
    }
}

