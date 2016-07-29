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
    protected Socket _socket;
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

      if(_socket != null && _socket.Connected)
        _socket.Close();

      var address = GetAddress(ipAddress);
      _localEndpoint = new IPEndPoint(address, port);
      _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

      _socketThread = new Thread(StartListening);

      SocketStatus = ServerStatuses.Running;
      _socketThread.Start();
    }
    
    public void Disconnect()
    {
      if (SocketStatus == ServerStatuses.Stopped) return;

      KillListener();
      _socketThread.Abort();
    }

    public event EventHandler<ServerStatuses> SocketStatusChanged;

    public void SendData(string data)
    {
      Socket connection;
      if (SocketStatus != ServerStatuses.Connected 
        || (connection = ConnectedSocket) == null
        || !connection.Connected) return;


      var dataEncoding = Encoding.UTF8.GetBytes(data);

      try
      {
        connection.Send(dataEncoding);
      }
      catch
      {
        KillListener();
      }
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
      try
      {
        _socket.Bind(_localEndpoint);
        _socket.Listen(100);

        while (true)
        {
          _socket.BeginAccept(AcceptConnection, _socket);
          while (ConnectedSocket == null || ConnectedSocket.Poll(-1, SelectMode.SelectWrite))
          {
            Thread.Sleep(500);
          }

          Disconnect();
        }
      }
      catch
      {
      }
    }

    protected void AcceptConnection(IAsyncResult result)
    {
      var listener = (Socket)result.AsyncState;
      if (SocketStatus == ServerStatuses.Stopped)
        return;

      try
      {
        ConnectedSocket = listener.EndAccept(result);
        SocketStatus = ServerStatuses.Connected;
      }
      catch
      {
        KillListener();
      }
    }

    protected void KillListener()
    {
      KillSocket(ConnectedSocket);
      KillSocket(_socket);

      SocketStatus = ServerStatuses.Stopped;
    }

    protected void KillSocket(Socket socket)
    {
      if (socket == null) return;
      
      try { socket.Shutdown(SocketShutdown.Both);} catch {}
      try { socket.Close();} catch {}
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
