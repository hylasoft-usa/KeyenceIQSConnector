using System;
using System.Windows.Forms;
using KeyenceSimulation.Factories;

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
      var manager = SimulationFactory.SimulationManager;
      manager.Start();
    }

    private static void RunApplication()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run();
    }
  }
}
