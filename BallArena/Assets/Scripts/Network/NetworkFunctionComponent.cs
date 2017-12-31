using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public delegate void NetworkCallback(SocketIOEvent NetworkEvent);
public abstract class AbstractNetworkFunctions : INetworkFunctionFactory
{
  [SerializeField]
  protected string SocketName;
  public abstract string SocketOnName { get; }
  public abstract void Function(SocketIOEvent NetworkEvent);
  public NetworkCallback NCallback;
}

[Serializable]
public class NetworkFunctionComponent : AbstractNetworkFunctions
{
  public override string SocketOnName { get { return null; } }
  public override void Function(SocketIOEvent NetworkEvent)
  {
    Debug.LogFormat("Calling {0}", SocketOnName);
  }
}