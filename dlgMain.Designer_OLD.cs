namespace WindowsFormsApplication1
{
    partial class dlgMain
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
            this.bnStart = new System.Windows.Forms.Button();
            this.bnStop = new System.Windows.Forms.Button();
            this.bnQuit = new System.Windows.Forms.Button();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.niTray = new System.Windows.Forms.NotifyIcon();
            this.SuspendLayout();
            // 
            // bnStart
            // 
            this.bnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bnStart.Name = "bnSave";
            this.bnStart.Size = new System.Drawing.Size(100, 28);
            this.bnStart.TabIndex = 0;
            this.bnStart.Text = "Start";
            this.bnStart.UseVisualStyleBackColor = true;
            this.bnStart.Click += new System.EventHandler(this.bnStart_Click);
            // 
            // bnStop
            // 
            this.bnStop.Location = new System.Drawing.Point(137, 26);
            this.bnStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bnStop.Name = "bnStop";
            this.bnStop.Size = new System.Drawing.Size(100, 28);
            this.bnStop.TabIndex = 0;
            this.bnStop.Text = "Stop";
            this.bnStop.UseVisualStyleBackColor = true;
            this.bnStop.Click += new System.EventHandler(this.bnStop_Click);
            this.bnStop.Visible = false;
            // 
            // bnQuit
            // 
            this.bnQuit.Location = new System.Drawing.Point(373, 26);
            this.bnQuit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bnQuit.Name = "bnQuit";
            this.bnQuit.Size = new System.Drawing.Size(100, 28);
            this.bnQuit.TabIndex = 0;
            this.bnQuit.Text = "Quit";
            this.bnQuit.UseVisualStyleBackColor = true;
            this.bnQuit.Click += new System.EventHandler(this.bnQuit_Click);
            //
            // lbLog
            //
            this.lbLog.Location = new System.Drawing.Point(4, 66);
            this.lbLog.Size = new System.Drawing.Size(591, 211);
            this.lbLog.Items.Add("Event Log");
            //
            // niTray
            //
            this.niTray.DoubleClick += new System.EventHandler(this.notifyIcon_MouseDoubleClick);
            this.niTray.Icon = ((System.Drawing.Icon)(Properties.Resources.Icon)); //The tray icon to use
            this.niTray.BalloonTipText = "Minimized to System Tray.";
            this.niTray.BalloonTipTitle = "Double click to restore.";
            // 
            // dlgMain
            // 
            this.Resize += new System.EventHandler(frmMain_Resize);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 297);
            this.Controls.Add(this.bnStart);
            this.Controls.Add(this.bnStop);
            this.Controls.Add(this.bnQuit);
            this.Controls.Add(this.lbLog);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "dlgMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Keyence-to-IQS Automatic Data Collector";
            this.ResumeLayout(false);
        }
        

        #endregion

        private System.Windows.Forms.Button bnStart;
        private System.Windows.Forms.Button bnStop;
        private System.Windows.Forms.Button bnQuit;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.NotifyIcon niTray; 
    }
}

