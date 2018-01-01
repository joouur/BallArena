using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public delegate void NetworkCallback(SocketIOEvent NetworkEvent);
public abstract class AbstractNetworkFunctions : INetworkFunctionFactory
{
  public abstract void Function(SocketIOEvent NetworkEvent);
  public NetworkCallback NCallback;
}

[Serializable]
public class NetworkFunctionComponent : AbstractNetworkFunctions
{
  private string socketName;
  public string SocketOnName { get { return socketName; } set { socketName = value; } }
  public override void Function(SocketIOEvent NetworkEvent)
  {
    Debug.LogFormat("Calling {0}", SocketOnName);
  }
}