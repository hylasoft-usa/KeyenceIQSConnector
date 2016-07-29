using System;
using System.Net.Sockets;
using KeyenceSimulation.Config;
using KeyenceSimulation.Enumerations;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Managers
{
  public class SimulationManager : ISimulationManager
  {
    protected readonly SimulationConfig _config;
    protected readonly ISocketManager _socketManager;
    protected readonly IKeyenceMessageManager _messageManager;
    protected readonly ISimulationControlView _controlView;

    public SimulationManager(SimulationConfig config, ISocketManager socketManager, IKeyenceMessageManager messageManager, ISimulationControlView controlView)
    {
      _config = config;
      _socketManager = socketManager;
      _messageManager = messageManager;
      _controlView = controlView;

      WireComponents();
    }

    #region ISimulationManager Implementation
    public void Start()
    {
      int port;
      var ipAddress = _controlView.ServerIp;

      var targetPort = int.TryParse(_controlView.ServerPort, out port)
        ? port
        : _config.Port;
      
      _socketManager.Connect(targetPort, ipAddress);
    }

    public void Stop()
    {
      _socketManager.Disconnect();
    }
    #endregion

    #region Domain Methods
    protected void WireComponents()
    {
      _socketManager.SocketStatusChanged += ChangeViewStatus;

      _controlView.StartClicked += (o, e) => SetServerStatus(Start);
      _controlView.StopClicked += (o, e) => SetServerStatus(Stop);
      _controlView.SendClicked += SendSimultion;

      _controlView.ServerIp = _config.IpAddress;
      _controlView.ServerPort = _config.Port.ToString("D");
      _controlView.ServerStatus = _socketManager.SocketStatus;
    }

    protected void ChangeViewStatus(object o, ServerStatuses status)
    {
      _controlView.ServerStatus = status;
    }

    protected void SendSimultion(object sender, EventArgs e)
    {
      IKeyenceMessage message;
      string messageStream;

      if ((message = _messageManager.BuildMessage()) == null || string.IsNullOrEmpty(messageStream = message.ToCharStream()))
        return;

      _socketManager.SendData(messageStream);
      _controlView.DisplayMessage(messageStream);
    }

    protected void SetServerStatus(Action set)
    {
      set();
    }
    #endregion
  }
}
