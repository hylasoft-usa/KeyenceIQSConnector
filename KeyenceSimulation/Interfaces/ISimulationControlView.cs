using System;
using KeyenceSimulation.Enumerations;

namespace KeyenceSimulation.Interfaces
{
  public interface ISimulationControlView
  {
    string ServerIp { get; set; }

    string ServerPort { get; set; }

    ServerStatuses ServerStatus { set; }

    event EventHandler StartClicked;

    event EventHandler StopClicked;
  }
}
