using System;
using SocketIO;
public interface INetworkFunctionFactory
{
  void Function(SocketIOEvent NetworkEvent);
}