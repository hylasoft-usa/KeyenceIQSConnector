using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using KeyenceSimulation.Enumerations;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Managers
{
  public class SocketManager : ISocketManager
  {
    protected const int MaxSocketBacklog = 10;

    protected readonly string _hostname;
    protected readonly IPHostEntry _hostEntry;
    protected IPEndPoint _localEndpoint;
    protected TcpListener _socket;
    protected Thread _socketThread;
    private ServerStatuses _socketStatus;

    protected Socket ConnectedSocket { get; private set; }

    public SocketManager()
    {
      _hostname = Dns.GetHostName();
      _hostEntry = Dns.GetHostEntry(_hostname);

      SocketStatus = ServerStatuses.Stopped;
    }

    public void Connect(int port, string ipAddress)
    {
      if (SocketStatus != ServerStatuses.Stopped) return;

      var address = GetAddress(ipAddress);
      _localEndpoint = new IPEndPoint(address, port);
      _socket = new TcpListener(_localEndpoint);
      _socketThread = new Thread(StartListening);

      SocketStatus = ServerStatuses.Running;
      _socketThread.Start();
    }

    public void Disconnect()
    {
      if (SocketStatus == ServerStatuses.Stopped) return;

      if (SocketStatus == ServerStatuses.Connected || ConnectedSocket != null)
      {
        SocketStatus = ServerStatuses.Running;
        while(SocketStatus != ServerStatuses.Stopped) Thread.Sleep(500);
      }

      _socket.Stop();
      _socketThread.Abort();

      SocketStatus = ServerStatuses.Stopped;
    }

    public event EventHandler<ServerStatuses> SocketStatusChanged;

    public void SendData(string data)
    {
      Socket connection;
      if (SocketStatus != ServerStatuses.Connected 
        || (connection = ConnectedSocket) == null
        || !connection.Connected) return;

      var dataEncoding = Encoding.UTF8.GetBytes(data);
      connection.Send(dataEncoding);
    }


    public ServerStatuses SocketStatus
    {
      get { return _socketStatus; }
      protected set
      {
        if(_socketStatus != value) TriggerSocketStatusChanged(_socketStatus = value);
      }
    }

    protected bool IsIpv4Address(IPAddress address)
    {
      return address != null && address.AddressFamily == AddressFamily.InterNetwork;
    }

    protected void StartListening()
    {
      _socket.Start();

      ConnectedSocket = _socket.AcceptSocket();
      SocketStatus = ServerStatuses.Connected;

      while (SocketStatus == ServerStatuses.Connected) Thread.Sleep(500);

      ConnectedSocket.Close();
      SocketStatus = ServerStatuses.Stopped;
    }

    protected IPAddress GetAddress(string ipAddress)
    {
      const int loopback = 0x0100007F;
      var defaultAddress = new IPAddress(loopback);

      IPAddress parsedIp;
      return string.IsNullOrEmpty(ipAddress)
        || !IPAddress.TryParse(ipAddress, out parsedIp)
          ? defaultAddress
          : parsedIp;
    }

    protected void TriggerSocketStatusChanged(ServerStatuses status)
    {
      if (SocketStatusChanged != null) SocketStatusChanged(this, status);
    }
  }
}
