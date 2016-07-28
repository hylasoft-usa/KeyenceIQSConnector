using KeyenceSimulation.Config;
using KeyenceSimulation.Interfaces;
using KeyenceSimulation.Managers;

namespace KeyenceSimulation.Factories
{
  public static class SimulationFactory
  {
    private static SimulationConfig _config;
    private static IConfigurationReader _reader;
    private static ISimulationManager _simulationManager;
    private static ISocketManager _socketManager;
    private static IKeyenceMessageManager _messageManager;

    public static IConfigurationReader ConfigurationReader
    {
      get { return _reader ?? (_reader = new ConfigurationReader()); }
    }

    public static ISocketManager SocketManager
    {
      get { return _socketManager ?? (_socketManager = new SocketManager(Config)); }
    }

    public static IKeyenceMessageManager MessageManager
    {
      get { return _messageManager ?? (_messageManager = new KeyenceMessageManager()); }
    }

    public static ISimulationManager SimulationManager
    {
      get
      {
        return _simulationManager ?? (_simulationManager = new SimulationManager(Config, SocketManager, MessageManager));
      }
    }

    public static SimulationConfig Config
    {
      get { return _config ?? (_config = ConfigurationReader.Read()); }
    }
  }
}
