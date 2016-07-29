using System;
using KeyenceSimulation.Enumerations;

namespace KeyenceSimulation.Interfaces
{
  public interface ISocketManager
  {
    void Connect(int port, string ipAddress);
    
    void Disconnect();

    event EventHandler<ServerStatuses> SocketStatusChanged;

    void SendData(string data);

    ServerStatuses SocketStatus { get; }

  }
}
