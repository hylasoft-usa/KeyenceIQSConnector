using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml;

namespace Keyence2IQS
{
	/// <summary>
    /// This class contains all the methods that allow you to select the right process when exporting Keyence Data to CSV.
    /// </summary>
    public partial class dlgShift : Form
    {
        static string logPath = dlgMain.logPath;
        /// <summary>A list of all processes that can be selected.</summary>
        private ComboBox cbList;
        /// <summary>This window's "OK" button.</summary>
        private Button bnOK;
        /// <summary>This window's "Cancel" button.</summary>
        private Button bnCancel;
        /// <summary>This window's system tray handler.</summary>
        private NotifyIcon niTray2;
        private IContainer components;

        /// <summary>
        /// Creates the Process Selection Window and inserts the processes from the config file into cbList.
        /// </summary>
        public dlgShift()
        {
            //Create initial Windows Form.
            InitializeComponent();
            if (ConfigurationManager.AppSettings["UseIQSProcesses"].ToLower().Equals("true"))
            {
                try
                {
                    var adapter = new IQSTableAdapters.PRCS_DATTableAdapter();
                    var table = new IQS.PRCS_DATDataTable();
                    adapter.Fill(table);
                    table.OrderBy(i => i.F_NAME).ToList().ForEach(i => cbList.Items.Add(i.F_NAME));
                }
                catch (Exception ex)
                {
                    File.AppendAllText(logPath, DateTime.Now + ": " + ex.Message + Environment.NewLine);
                    if (ex.InnerException != null)
                        File.AppendAllText(logPath, DateTime.Now + ": " + ex.InnerException.Message + Environment.NewLine);
                    //Assumes Process Names do not have ";" in them.
                    ConfigurationManager.AppSettings["Proc"].Split(';').OrderBy(i => i).ToList().ForEach(i => cbList.Items.Add(i));
                }
            }
            else
            {
                //Assumes Process Names do not have ";" in them.
                ConfigurationManager.AppSettings["Proc"].Split(';').OrderBy(i => i).ToList().ForEach(i => cbList.Items.Add(i));
            }
        }

        /// <summary>
        /// Completes process selection for CSV conversion.  Will not complete if no process is selected.
        /// </summary>
        private void bnOK_Click(object sender, EventArgs e)
        {
            if (cbList.SelectedItem == null)
            {
                MessageBox.Show("Please select a part group and press OK to continue, or click Cancel to abort part group selection.");
            }
            else
            {
                dlgMain.TestProc = cbList.SelectedItem.ToString();
                this.Close();
            }
        }

        /// <summary>
        /// Aborts CSV conversion.  Also occurs if the window is closed prematurely.
        /// </summary>
        private void bnCancel_Click(object sender, EventArgs e)
        {
            dlgMain.TestProc = null;
            Close();
        }

        /// <summary>
        /// Sets the window into the system tray.
        /// </summary>
        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                niTray2.Visible = true;
                niTray2.ShowBalloonTip(500);
                Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                niTray2.Visible = false;
            }
        }

        /// <summary>
        /// When double-clicked, the system tray icon will restore the window.
        /// </summary>
        private void notifyIcon_MouseDoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            niTray2.Visible = false;
        }

        /// <summary>
        /// Creates the initial window for selecting the Keyence process.
        /// </summary>
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgShift));
      this.cbList = new System.Windows.Forms.ComboBox();
      this.bnOK = new System.Windows.Forms.Button();
      this.bnCancel = new System.Windows.Forms.Button();
      this.niTray2 = new System.Windows.Forms.NotifyIcon(this.components);
      this.SuspendLayout();
      // 
      // cbList
      // 
      this.cbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbList.FormattingEnabled = true;
      this.cbList.Location = new System.Drawing.Point(12, 12);
      this.cbList.Name = "cbList";
      this.cbList.Size = new System.Drawing.Size(260, 21);
      this.cbList.TabIndex = 0;
      // 
      // bnOK
      // 
      this.bnOK.Location = new System.Drawing.Point(12, 39);
      this.bnOK.Name = "bnOK";
      this.bnOK.Size = new System.Drawing.Size(75, 23);
      this.bnOK.TabIndex = 1;
      this.bnOK.Text = "OK";
      this.bnOK.UseVisualStyleBackColor = true;
      this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
      // 
      // bnCancel
      // 
      this.bnCancel.Location = new System.Drawing.Point(197, 39);
      this.bnCancel.Name = "bnCancel";
      this.bnCancel.Size = new System.Drawing.Size(75, 23);
      this.bnCancel.TabIndex = 2;
      this.bnCancel.Text = "Cancel";
      this.bnCancel.UseVisualStyleBackColor = true;
      this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
      // 
      // niTray2
      // 
      this.niTray2.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
      this.niTray2.BalloonTipText = "Double-Click to restore.";
      this.niTray2.BalloonTipTitle = "Minimized to system tray.";
      this.niTray2.Icon = ((System.Drawing.Icon)(resources.GetObject("niTray2.Icon")));
      this.niTray2.Text = "notifyIcon1";
      this.niTray2.Visible = true;
      this.niTray2.DoubleClick += new System.EventHandler(this.notifyIcon_MouseDoubleClick);
      // 
      // dlgPartGroup
      // 
      this.ClientSize = new System.Drawing.Size(284, 69);
      this.Controls.Add(this.bnCancel);
      this.Controls.Add(this.bnOK);
      this.Controls.Add(this.cbList);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "dlgPartGroup";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Keyence Part Group Selector";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.dlgPartGroup_Load);
      this.Resize += new System.EventHandler(this.frmMain_Resize);
      this.ResumeLayout(false);

        }

        private void dlgPartGroup_Load(object sender, EventArgs e)
        {

        }

    }
}
