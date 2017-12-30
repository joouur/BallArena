using System;
using SocketIO;
public delegate void NetworkCallback(Action<SocketIOEvent> NetworkEvent);
public interface INetworkFunctionFactory
{
  string SocketOnName { get; }
  void Function(SocketIOEvent NetworkEvent);
  NetworkCallback NCallback();
}