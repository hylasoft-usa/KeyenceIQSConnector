using System.Net.Sockets;
using KeyenceSimulation.Config;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Managers
{
  public class SimulationManager : ISimulationManager
  {
    protected readonly SimulationConfig _config;
    protected readonly ISocketManager _socketManager;
    protected readonly IKeyenceMessageManager _messageManager;

    public SimulationManager(SimulationConfig config, ISocketManager socketManager, IKeyenceMessageManager messageManager)
    {
      _config = config;
      _socketManager = socketManager;
      _messageManager = messageManager;

      WireComponents();
    }

    #region ISimulationManager Implementation
    public void Start()
    {
      _socketManager.Connect();
    }

    public void Stop()
    {
      _socketManager.Disconnect();
    }
    #endregion

    #region Domain Methods
    protected void WireComponents()
    {
      _socketManager.DataRequested += SendSimultion;
    }

    protected void SendSimultion(object sender, Socket connection)
    {
      IKeyenceMessage message;
      string messageStream;

      if ((message = _messageManager.BuildMessage()) == null || string.IsNullOrEmpty(messageStream = message.ToCharStream()))
        return;

      _socketManager.SendData(connection, messageStream);
    }
    #endregion
  }
}
