using System;
using System.Windows.Forms;
using KeyenceSimulation.Enumerations;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Forms
{
  public partial class SimulationControl : Form, ISimulationControlView
  {
    public SimulationControl()
    {
      InitializeComponent();
      WireEvents();
    }

    #region ISimulationControlView Implementation
    public string ServerIp
    {
      get { return IpInput.Text; }
      set { IpInput.Text = value; }
    }

    public string ServerPort
    {
      get { return PortInput.Text; }
      set { PortInput.Text = value; }
    }

    public bool ServiceRunning
    {
      set
      {
        SetInputAccess(!value);

        StartButton.Enabled = !value;
        StopButton.Enabled = value;
      }
    }

    public ServerStatuses ServerStatus
    {
      set
      {
        UpdateServiceStatus(value);
      }
    }


    public event EventHandler StartClicked;
    public event EventHandler StopClicked;
    public event EventHandler SendClicked;
    #endregion

    #region Protected Methods
    protected void WireEvents()
    {
      StopButton.Click += TriggerStop;
      StartButton.Click += TriggerStart;
      SendButton.Click += TriggerSend;
    }

    protected void TriggerStart(object sender, EventArgs e)
    {
      if(StartClicked != null)
        StartClicked(this, e);
    }

    protected void TriggerStop(object sender, EventArgs e)
    {
      if(StopClicked != null)
        StopClicked(this, e);
    }

    protected void TriggerSend(object sender, EventArgs e)
    {
      if(SendClicked != null)
        SendClicked(this, e);
    }

    protected void SetInputAccess(bool isEnabled)
    {
      IpInput.Enabled = PortInput.Enabled = isEnabled;
    }

    protected void UpdateServiceStatus(ServerStatuses status)
    {
      if (InvokeRequired)
      {
        var invoker = new MethodInvoker(() => UpdateServiceStatus(status));
        Invoke(invoker);
        return;
      }

      ThreadSafeUpdateServiceStatus(status);
    }

    protected void ThreadSafeUpdateServiceStatus(ServerStatuses status)
    {
      var serverRunning = status == ServerStatuses.Running || status == ServerStatuses.Connected;
      SendButton.Enabled = status == ServerStatuses.Connected;

      ServiceRunning = serverRunning;
      StatusInput.Text = status.ToString();
    }
    #endregion
  }
}
