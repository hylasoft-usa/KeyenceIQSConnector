using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using KeyenceSimulation.Config;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Managers
{
  public class SocketManager : ISocketManager
  {
    protected const int MaxSocketBacklog = 10;

    protected readonly SimulationConfig _config;
    protected readonly string _hostname;
    protected readonly IPHostEntry _hostEntry;
    protected readonly IPEndPoint _localEndpoint;
    protected readonly Socket _socket;
    protected readonly Thread _socketThread;

    protected bool IsConnected { get; set; }

    public SocketManager(SimulationConfig config)
    {
      _config = config;
      _hostname = Dns.GetHostName();
      _hostEntry = Dns.GetHostEntry(_hostname);
      
      var address = GetAddress();
      var port = config.Port;
      
      _localEndpoint = new IPEndPoint(address, port);
      _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

      _socketThread = new Thread(StartListening);
      IsConnected = false;
    }

    public void Connect()
    {
      _socketThread.Start();
    }

    public void Disconnect()
    {
      _socketThread.Abort();
      IsConnected = false;
    }

    public event EventHandler<Socket> DataRequested;

    public void SendData(Socket connection, string data)
    {
      if (!IsConnected) return;

      var dataEncoding = Encoding.UTF8.GetBytes(data);
      connection.Send(dataEncoding);
    }


    protected bool IsIpv4Address(IPAddress address)
    {
      return address != null && address.AddressFamily == AddressFamily.InterNetwork;
    }

    protected void StartListening()
    {
      _socket.Bind(_localEndpoint);
      _socket.Listen(MaxSocketBacklog);
      IsConnected = true;

      while (true)
      {
        var connection = _socket.Accept();
        
        TriggerDataRequested(connection);

        connection.Shutdown(SocketShutdown.Both);
        connection.Close();
      }
    }

    protected IPAddress GetAddress()
    {
      const int loopback = 0x0100007F;
      var defaultAddress = new IPAddress(loopback);

      string configIpAddress;
      IPAddress parsedIp;
      return _config == null
        || string.IsNullOrEmpty(configIpAddress = _config.IpAddress)
        || !IPAddress.TryParse(configIpAddress, out parsedIp)
          ? defaultAddress
          : parsedIp;
    }

    protected void TriggerDataRequested(Socket connection)
    {
      if(DataRequested != null) DataRequested(this, connection);
    }
  }
}
