using System;
using System.Windows.Forms;
using KeyenceSimulation.Config;
using KeyenceSimulation.Interfaces;
using KeyenceSimulation.Managers;

namespace KeyenceSimulation
{
  static class KeyenceSimulation
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      StartSimulation();
      RunApplication();
    }

    private static void StartSimulation()
    {
      var config = GetConfiguration();
      var manager = GetSimulationManager(config);
      manager.Start();
    }

    private static void RunApplication()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run();
    }

    private static SimulationConfig GetConfiguration()
    {
      const int port = 55055;
      const string address = "127.0.0.1";

      return new SimulationConfig(port, address);
    }

    private static ISimulationManager GetSimulationManager(SimulationConfig config)
    {
      var socketManager = GetSocketManager(config);
      var messageManager = GetMessageManager();

      return new SimulationManager(config, socketManager, messageManager);
    }

    private static ISocketManager GetSocketManager(SimulationConfig config)
    {
      return new SocketManager(config);
    }

    private static IKeyenceMessageManager GetMessageManager()
    {
      return new KeyenceMessageManager();
    }
  }
}
