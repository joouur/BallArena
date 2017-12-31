using System;
using SocketIO;
public interface INetworkFunctionFactory
{
  string SocketOnName { get; }
  void Function(SocketIOEvent NetworkEvent);
}