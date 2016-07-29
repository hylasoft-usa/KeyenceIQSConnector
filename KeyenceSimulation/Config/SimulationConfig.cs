namespace KeyenceSimulation.Config
{
  public class SimulationConfig
  {
    public int Port { get; private set; }
    public string IpAddress { get; private set; }

    public SimulationConfig(int port, string ipAddress)
    {
      IpAddress = ipAddress;
      Port = port;
    }
  }
}
