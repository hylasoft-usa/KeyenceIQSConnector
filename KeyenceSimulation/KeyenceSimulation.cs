using System;
using System.Windows.Forms;
using KeyenceSimulation.Factories;
using KeyenceSimulation.Forms;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation
{
  static class KeyenceSimulation
  {

    private static ISimulationManager _simulationManager;

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      try
      {
        RunApplication();
      }
      catch
      {
        // TODO: Error reporting or validation.
      }
      finally
      {
        if (_simulationManager != null)
          _simulationManager.Stop();

        Application.Exit();
      }
    }

    private static void RunApplication()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      var controlView = new SimulationControl();
      _simulationManager = SimulationFactory.BuildSimulationManager(controlView);
      
      Application.Run(controlView);
    }
  }
}
