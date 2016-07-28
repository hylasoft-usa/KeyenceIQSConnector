using System;
using System.Net.Sockets;

namespace KeyenceSimulation.Interfaces
{
  public interface ISocketManager
  {
    void Connect();
    
    void Disconnect();

    event EventHandler<Socket> DataRequested;

    void SendData(Socket connection, string data);
  }
}
