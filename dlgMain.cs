using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Xml.Linq;

namespace Keyence2IQS
{
    /// <summary>
    /// This class contains all the methods that perform connections to Keyence Machines and Databases and performs the subgroup adding.
    /// </summary>
    public class dlgMain : Form
    {
        public static string logPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\HylaSoft\\Log.txt"; //"LOGS//Log.txt"
        //Add class variables.
        String Log_String;
        bool SetVisible = false;
        static DateTime EXP = new DateTime(2016, 7, 7, 0, 0, 0);
        public static String TestProc;

        /// <summary>The current subgroup parsed from the Keyence Machine.</summary>
        public IQS_Table SUBGROUP;

        /// <summary>A shorthand replacement for the "newline" character.</summary>
        static public String NL = Environment.NewLine;

        /// <summary>Keeps track of when the user wants  Keyence Data to be collected.</summary>
        bool collect_data = false;

        /// <summary>The program's "Start" button.</summary>
        private Button bnStart;

        /// <summary>The program's "Stop" button.</summary>
        private Button bnStop;

        /// <summary>The program's "Quit" button"</summary>
        private Button bnQuit;

        /// <summary>The program's event log.</summary>
        private ListBox lbLog;

        /// <summary>The notification icon used for the program's system tray setting.</summary>
        private NotifyIcon niTray;
        private IContainer components;

        /// <summary>The socket used to connect to the Keyence machine.</summary>
        Socket send_socket;
        private Button bnProc;
        private Timer tm_time;
        private Button bnPartGroup;

        /// <summary>A background thread that reads Keyence data and adds it to the respective IQS Database oDB.</summary>
        private BackgroundWorker Back_Thread;

        /// <summary>
        /// Takes a string representation of machine output and creates an IQS_Table Object from it.
        /// </summary>
        /// <param name="DATA">The string that the IM machine sends to its clients via TCP/IP.</param>
        /// <returns>An IQS_Table object that contains all information given in DATA.</returns>
        static IQS_Table Parse(String DATA)
        {
            //Declare necessary variables, then split lines.
            bool exitLoop = false;//Used to exit loop when "EN" tag is encountered.
            bool failFlag = false;//Used to determine if a measurement failed in a given "IT" line.
            //Includes all possible units of measurement.  Used to check if unit of measurement exists in an "IT" line.
            String[] Lines = DATA.Split(new String[] { NL }, StringSplitOptions.RemoveEmptyEntries);
            IQS_Table TABLE = new IQS_Table();
            //Check each line's identity and arguments.
            int cur_test = 0;//Used to keep track of test number during "IT" lines.
            foreach (String s in Lines)
            {
                DATA = DATA.Remove(0, s.Length).Trim(NL.ToCharArray()); //removed what is being read
                String[] Fields = (ConfigurationManager.AppSettings["Delimiter"].Equals("\\t"))
                        ? s.Replace('\t', ';').Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                        : s.Split(new[] { ConfigurationManager.AppSettings["Delimiter"] }, StringSplitOptions.RemoveEmptyEntries);
                switch (Fields[0])//Check tag of data line:
                {
                    case "ST"://Start of data: Make new Table.
                        TABLE = new IQS_Table();//There are always 7 more lines than there are tests in a measurement result.
                        break;
                    case "SE"://Serial Number + Version
                        //TABLE.Serial = Fields[1];
                        //TABLE.Version = Fields[2];
                        break;
                    case "DA"://Timestamp
                        //TABLE.Time = Fields[1] + " " + Fields[2];
                        break;
                    case "MS"://Part Name
                        //TABLE.Part = Fields[1];
                        //for (int i = 2; i < Fields.Length - 1; i++)
                        //{
                        //    TABLE.Part += " " + Fields[i];
                        //}
                        break;
                    case "LO"://Lot Number
                        //TABLE.Lot = String.Empty;
                        //if (Fields.Length > 2)
                        //{
                        //    TABLE.Lot += Fields[1];
                        //    for (int i = 2; i < Fields.Length - 1; i++)
                        //    {
                        //        TABLE.Lot += " " + Fields[i];
                        //    }
                        //}
                        break;
                    case "CH"://Operator's Name
                        //TABLE.Employee = String.Empty;
                        //if (Fields.Length > 2)
                        //{
                        //    TABLE.Employee += Fields[1];
                        //    for (int i = 2; i < Fields.Length - 1; i++)
                        //    {
                        //        TABLE.Employee += " " + Fields[i];
                        //    }
                        //}
                        break;
                    case "IT"://Measurement Value
                        Test T = new Test();
                        int size = Fields.Length;//Length is used to find which section of the line a certain number or string is in.
                        //Index:  | 0 |   1   |   2  |   3  |     4      | ... |   size-6   | size-5 |    size-4     |      size-3    |  size-2 |  size-1 |
                        //Field:  |Tag| TestNo| Value| Unit | Test_Name_1| ... | Test_Name_n| Target |Upper_Tolerance| Lower_Tolerance| Judgment| Checksum|
                        double t = Convert.ToDouble(Fields[size - 5]);
                        double v = -1;//Used to indicate failure.
                        //T.Target = t;
                        //T.USL = (Convert.ToDouble(Fields[size - 4]));
                        //T.LSL = (Convert.ToDouble(Fields[size - 3]));
                        try
                        {
                            v = Convert.ToDouble(Fields[2]);
                        }
                        catch (FormatException)//No measurement - Failure.
                        {
                            failFlag = true;
                            T.Value = v;
                            //    T.Unit = Fields[3];
                            T.Name = Fields[4];
                            for (int i = 5; i < (Fields.Length - 5); i++)
                            {
                                T.Name += " " + Fields[i];
                            }
                        }
                        if (failFlag)//Reset and skip the rest of this block.
                        {
                            cur_test++;
                            failFlag = false;
                            break;
                        }
                        T.Value = v;
                        //T.Unit = Fields[3];
                        T.Name = Fields[4];
                        for (int i = 5; i < (Fields.Length - 5); i++)
                        {
                            T.Name += " " + Fields[i];
                        }
                        if (ConfigurationManager.AppSettings["GetIQSGroups"].ToLower().Equals("false"))
                        {
                            //    T.Group = LookupTestGroupXML(T.Name);
                        }
                        //TABLE.Tests.Add(T);
                        cur_test++;
                        break;
                    case "EN"://End of data.  Exit foreach statement.
                        exitLoop = true;
                        break;
                    default://Unrecognized data.  Do nothing.
                        break;
                }
                if (exitLoop)
                    break;
            }
            return TABLE;
        }

        private static string LookupTestGroupXML(string testName)
        {
            try
            {
                var xDoc = XDocument.Load("TestGroupLookup.xml");
                var tests = xDoc.Descendants("Test").Where(i => i.Attribute("Name") != null && i.Attribute("Name").Value.ToLower().Equals(testName.ToLower())).ToList();
                if (tests.Count > 0)
                {
                    var parent = tests.First().Ancestors("TestGroup");
                    if (parent.Any())
                    {
                        return (parent.First().Attribute("Name") != null) ? parent.First().Attribute("Name").Value : string.Empty;
                    }
                }
            }
            catch { }
            return string.Empty;
        }

        /// <summary>
        /// Creates the initial Windows Form and sets up the Back_Thread properties and events.
        /// </summary>
        public dlgMain()
        {
            //Create initial Windows Form.
            InitializeComponent();
            //Set Back_Thread properties and events.
            Back_Thread = new BackgroundWorker();
            Back_Thread.WorkerReportsProgress = true;
            Back_Thread.WorkerSupportsCancellation = true;
            Back_Thread.DoWork += Add_Data;
            Back_Thread.RunWorkerCompleted += Stop_Collection;
            Back_Thread.ProgressChanged += Add_To_List;
            if (ConfigurationManager.AppSettings["TestIQSConnection"].ToLower().Equals("true"))
            {
                bnProc.Visible = true;
            }
        }

        /// <summary>
        /// When flagged to do work, the background thread Back_Thread will perform this method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">The event handler created in dlgMain.</param>
        private void Add_Data(object sender, DoWorkEventArgs e)
        {
            collect_data = true;
            //Set initial ip and port.
            String IPString = ConfigurationManager.AppSettings["IP"];
            IPAddress IP;
            try { IP = IPAddress.Parse(IPString); }//Try to parse IP Address in config file.
            catch (FormatException)//IP Address invalid.  Cancel entry and inform user.
            {
                MessageBox.Show("Invalid IP Address in config file.  Please quit the application, change your config file's IP Address, and try again.");
                collect_data = false;
                return;
            }
            int Port;
            try { Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]); }//Try to convert config file's port to int.
            catch (FormatException)//Port number invalid.  Cancel entry and inform user.
            {
                MessageBox.Show("Invalid Port in config file.  Please quit the application, change your config file's Port number, and try again.");
                collect_data = false;
                return;
            }
            IPEndPoint IPEnd = new IPEndPoint(IP, Port);
            //Initialize socket and socket properties.
            send_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//Used to receive TCP/IP data.
            send_socket.ReceiveTimeout = 1000;
            String result = String.Empty;//Holds machine output.
            try//Connect to Keyence Machine.
            {
                send_socket.Connect(IPEnd);
                Log_String = DateTime.Now + ": Connected to Keyence Machine - " + IPString + ":" + Port;
                Back_Thread.ReportProgress(1);
            }
            catch (SocketException)//Connection unsuccessful.  Abort.
            {
                Log_String = DateTime.Now + ": Failed to connect to Keyence Machine - " + IPString + ":" + Port;
                Back_Thread.ReportProgress(1);
                collect_data = false;
                return;
            }
            while (collect_data)//Begin waiting for Keyence data.
            {
                Byte[] input = new Byte[1024];
                int Data = 0;
                try { Data = send_socket.Receive(input); }//Read Keyence data.  Variable Data keeps track of how many bytes were read.
                catch (SocketException) { }//Read timeout.  Try again.
                result += System.Text.Encoding.UTF8.GetString(input, 0, Data);//Only adds that many bytes.
                if (result.Contains(NL + "EN"))//Activated when an "EN" tag has been encountered.
                {
                    //Log raw collected data.
                    Log_String = DateTime.Now + ": Collected Data" + NL + result;
                    Back_Thread.ReportProgress(1);
                    System.Threading.Thread.Sleep(100);
                    SUBGROUP = Parse(result);
                    result = String.Empty;
                    //Switch to process selection screen.
                    SetVisible = true;
                    Back_Thread.ReportProgress(0);
                    System.Threading.Thread PSelect = new System.Threading.Thread(new System.Threading.ThreadStart(ProcSelect));
                    PSelect.Start();
                    PSelect.Join();//Wait until process is selected or canceled.
                    Back_Thread.ReportProgress(0);
                    System.Threading.Thread.Sleep(100);
                    SetVisible = false;
                    //Check entered process.
                    if (TestProc == null)//No process selected.  Cancel CSV conversion.
                    {
                        Log_String = DateTime.Now + ": Cancelled Data Conversion.";
                        Back_Thread.ReportProgress(1);
                    }
                    else//Process selected.  Convert to CSV.
                    {
                        //SUBGROUP.Process = TestProc;
                        Log_String = DateTime.Now + ": Converted Data to CSV file.";
                        Back_Thread.ReportProgress(1);
                        WriteSubgroup(SUBGROUP);
                    }
                }
            }
        }

        /// <summary>
        /// Opens the Process Selection window.
        /// </summary>
        private void ProcSelect()
        {
            Application.Run(new dlgShift());
        }

        /// <summary>
        /// Switches the collect_data flag, telling the program to disconnect from the Keyence Machine.
        /// </summary>
        private void bnStop_Click(object sender, EventArgs e)
        {
            collect_data = false;
            send_socket.Close();
        }

        /// <summary>
        /// Quits the program.  Cannot be done while collecting data.
        /// </summary>
        private void bnQuit_Click(object sender, EventArgs e)
        {
            if (collect_data == true)
            {
                MessageBox.Show("Still collecting data!  Please disable data collection before quitting");
                return;
            }
            Close();
        }

        /// <summary>
        /// Begins data collection from the Keyence Machine.  Will continue to wait for data until the "Stop"  button is clicked.
        /// </summary>
        private void bnStart_Click(object sender, EventArgs e)
        {
            lbLog.Items.Add(DateTime.Now + ": Attempting to collect data from Keyence Machine.");
            File.AppendAllText(logPath, DateTime.Now + ": Attempting to collect data from Keyence Machineaaa." + NL);
            Back_Thread.RunWorkerAsync();
            bnStart.Visible = false;
            bnStop.Visible = true;
            bnQuit.Enabled = false;
        }

        /// <summary>
        /// Sets the window into the system tray.
        /// </summary>
        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                niTray.Visible = true;
                niTray.ShowBalloonTip(500);
                Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                niTray.Visible = false;
            }
        }

        /// <summary>
        /// When double-clicked, the system tray icon will restore the window.
        /// </summary>
        private void notifyIcon_MouseDoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            niTray.Visible = false;
        }

        /// <summary>
        /// Activated when the program stops waiting for Keyence data.
        /// </summary>
        private void Stop_Collection(object sender, RunWorkerCompletedEventArgs e)
        {
            lbLog.Items.Add(DateTime.Now + ": Data collection halted.");
            File.AppendAllText(logPath, DateTime.Now + ": Data collection halted." + NL);
            bnStop.Visible = false;
            bnStart.Visible = true;
            bnQuit.Enabled = true;
        }

        /// <summary>
        /// Either adds to the event log and log file, or switches between Process Selection and Keyence Connection Mode.
        /// </summary>
        private void Add_To_List(object sender, ProgressChangedEventArgs e)
        {
            if (SetVisible)
                Visible = !Visible;
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings["LogSetting"].Equals("All") || !Log_String.Contains(NL))
                {
                    lbLog.Items.Add(Log_String);
                    File.AppendAllText(logPath, Log_String + NL);
                }
                else
                    File.AppendAllText(logPath, Log_String);
            }
        }

        /// <summary>
        /// Turns an IQS_Table object into a CSV string and writes it to a specified area.
        /// </summary>
        /// <param name="SG">The IQS_Table object to be converted.</param>
        public static void WriteSubgroup(IQS_Table SG)
        {
            StringBuilder S = new StringBuilder();
            if (ConfigurationManager.AppSettings["GetIQSGroups"].ToLower().Equals("true"))
            {
                //var partG = LookupPartGroup(SG.Part);
                //var prcsG = LookupProcessGroup(SG.Process);
                //for (int i = 0; i < SG.Tests.Count; i++)//Turn each test into a CSV line.
                {
                    //    Test T = SG.Tests[i];
                    //    var testG = LookupTestGroup(T.Name);
                    //    if (testG.Equals(string.Empty)) continue;
                    //    S.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", (i + 1), T.Value, T.Name, T.Target, T.USL, T.LSL, testG, SG.Part, partG, SG.Process, prcsG, NL);
                    //"Test_Number,Test_Value,Test_Name,Test_Target_Value,Test_Upper_Tolerance,Test_Lower_Tolerance,Test_Part,Test_Process,\n"
                }
            }
            else
            {
                //for (int i = 0; i < SG.Tests.Count; i++)//Turn each test into a CSV line.
                //{
                //    Test T = SG.Tests[i];
                //    if (T.Group.Equals(string.Empty)) continue;
                //    S.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", (i + 1), T.Value, T.Name, T.Target, T.USL, T.LSL, T.Group, SG.Part, SG.Process, NL);
                //"Test_Number,Test_Value,Test_Name,Test_Target_Value,Test_Upper_Tolerance,Test_Lower_Tolerance,Test_Part,Test_Process,\n"
                //}
            }
            //string filename = ConfigurationManager.AppSettings["Dest"].Replace("%process", SG.Process).Replace("%part", SG.Part);
            //File.AppendAllText(filename + ".csv", S.ToString());
        }

        private static string LookupProcessGroup(string process)
        {
            var adapter = new IQSTableAdapters.PRCS_DATTableAdapter();
            var table = new IQS.PRCS_DATDataTable();
            adapter.Fill(table);
            var prcs_dat = table.Where(i => i.F_NAME.ToLower().Equals(process.ToLower())).ToList();
            if (prcs_dat.Any())
            {
                var adapter2 = new IQSTableAdapters.PRCS_GRPTableAdapter();
                var table2 = new IQS.PRCS_GRPDataTable();
                adapter2.Fill(table2);
                var prcs_grp = table2.Where(i => i.F_PRGP.Equals(prcs_dat.First().F_PRGP)).ToList();
                if (prcs_grp.Any())
                {
                    return prcs_grp.First().F_NAME;
                }
            }
            return string.Empty;
        }

        private static string LookupPartGroup(string part)
        {
            var adapter = new IQSTableAdapters.PART_DATTableAdapter();
            var table = new IQS.PART_DATDataTable();
            adapter.Fill(table);
            var part_dat = table.Where(i => i.F_NAME.ToLower().Equals(part.ToLower())).ToList();
            if (part_dat.Any())
            {
                var adapter2 = new IQSTableAdapters.PART_GRPTableAdapter();
                var table2 = new IQS.PART_GRPDataTable();
                adapter2.Fill(table2);
                var part_grp = table2.Where(i => i.F_PTGP.Equals(part_dat.First().F_PTGP)).ToList();
                if (part_grp.Any())
                {
                    return part_grp.First().F_NAME;
                }
            }
            return string.Empty;
        }

        private static string LookupTestGroup(string test)
        {
            var adapter = new IQSTableAdapters.TEST_DATTableAdapter();
            var table = new IQS.TEST_DATDataTable();
            adapter.Fill(table);
            var test_dat = table.Where(i => i.F_NAME.ToLower().Equals(test.ToLower())).ToList();
            if (test_dat.Any())
            {
                var adapter2 = new IQSTableAdapters.TEST_GRPTableAdapter();
                var table2 = new IQS.TEST_GRPDataTable();
                adapter2.Fill(table2);
                var test_grp = table2.Where(i => i.F_TSGP.Equals(test_dat.First().F_TSGP)).ToList();
                if (test_grp.Any())
                {
                    return test_grp.First().F_NAME;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Creates the initial window for connecting to the Keyence Machine.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgMain));
            this.bnStart = new System.Windows.Forms.Button();
            this.bnQuit = new System.Windows.Forms.Button();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.bnStop = new System.Windows.Forms.Button();
            this.bnProc = new System.Windows.Forms.Button();
            this.tm_time = new System.Windows.Forms.Timer(this.components);
            this.bnPartGroup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bnStart
            // 
            this.bnStart.Location = new System.Drawing.Point(12, 12);
            this.bnStart.Name = "bnStart";
            this.bnStart.Size = new System.Drawing.Size(150, 23);
            this.bnStart.TabIndex = 0;
            this.bnStart.Text = "Start";
            this.bnStart.UseVisualStyleBackColor = true;
            this.bnStart.Click += new System.EventHandler(this.bnStart_Click);
            // 
            // bnQuit
            // 
            this.bnQuit.Location = new System.Drawing.Point(12, 334);
            this.bnQuit.Name = "bnQuit";
            this.bnQuit.Size = new System.Drawing.Size(150, 23);
            this.bnQuit.TabIndex = 1;
            this.bnQuit.Text = "Quit";
            this.bnQuit.UseVisualStyleBackColor = true;
            this.bnQuit.Click += new System.EventHandler(this.bnQuit_Click);
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.HorizontalScrollbar = true;
            this.lbLog.ItemHeight = 16;
            this.lbLog.Items.AddRange(new object[] {
            "                              --------------------EVENT LOG--------------------"});
            this.lbLog.Location = new System.Drawing.Point(168, 15);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(532, 340);
            this.lbLog.TabIndex = 2;
            this.lbLog.SelectedIndexChanged += new System.EventHandler(this.lbLog_SelectedIndexChanged);
            // 
            // niTray
            // 
            this.niTray.BalloonTipText = "Double-Click to restore.";
            this.niTray.BalloonTipTitle = "Minimized to system tray.";
            this.niTray.Icon = ((System.Drawing.Icon)(resources.GetObject("niTray.Icon")));
            this.niTray.Text = "notifyIcon1";
            this.niTray.Visible = true;
            this.niTray.DoubleClick += new System.EventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // bnStop
            // 
            this.bnStop.Location = new System.Drawing.Point(12, 12);
            this.bnStop.Name = "bnStop";
            this.bnStop.Size = new System.Drawing.Size(150, 23);
            this.bnStop.TabIndex = 3;
            this.bnStop.Text = "Stop";
            this.bnStop.UseVisualStyleBackColor = true;
            this.bnStop.Visible = false;
            this.bnStop.Click += new System.EventHandler(this.bnStop_Click);
            // 
            // bnProc
            // 
            this.bnProc.Location = new System.Drawing.Point(12, 41);
            this.bnProc.Name = "bnProc";
            this.bnProc.Size = new System.Drawing.Size(150, 23);
            this.bnProc.TabIndex = 4;
            this.bnProc.Text = "Show Processes";
            this.bnProc.UseVisualStyleBackColor = true;
            this.bnProc.Visible = false;
            this.bnProc.Click += new System.EventHandler(this.bnProc_Click);
            // 
            // tm_time
            // 
            this.tm_time.Interval = 3600000;
            this.tm_time.Tick += new System.EventHandler(this.tm_time_Tick);
            // 
            // bnPartGroup
            // 
            this.bnPartGroup.Location = new System.Drawing.Point(12, 70);
            this.bnPartGroup.Name = "bnPartGroup";
            this.bnPartGroup.Size = new System.Drawing.Size(150, 23);
            this.bnPartGroup.TabIndex = 5;
            this.bnPartGroup.Text = "Show Processes";
            this.bnPartGroup.UseVisualStyleBackColor = true;
            this.bnPartGroup.Visible = false;
            this.bnPartGroup.Click += new System.EventHandler(this.bnShiftClick);
            // 
            // dlgMain
            // 
            this.ClientSize = new System.Drawing.Size(712, 378);
            this.Controls.Add(this.bnPartGroup);
            this.Controls.Add(this.bnProc);
            this.Controls.Add(this.bnStop);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.bnQuit);
            this.Controls.Add(this.bnStart);
            this.Name = "dlgMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Keyence Automatic Data Collector";
            this.Load += new System.EventHandler(this.dlgMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.ResumeLayout(false);

        }

        private void bnProc_Click(object sender, EventArgs e)
        {
            var prcs = new dlgShift();//TODO: why not dlgProc??
            prcs.ShowDialog();
        }

        private void tm_time_Tick(object sender, EventArgs e)
        {
            if (EXP < DateTime.Now)
            {
                MessageBox.Show("Sorry, but this application is past its expiration date.  the application will now exit.");
                this.Close();
            }
        }

        private void lbLog_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bnShiftClick(object sender, EventArgs e)
        {
            var prtshift = new dlgShift();
            prtshift.ShowDialog();
        }

        private void dlgMain_Load(object sender, EventArgs e)
        {

        }
    }
}
