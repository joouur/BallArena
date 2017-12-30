using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public abstract class AbstractNetworkFunctions : INetworkFunctionFactory
{
  [SerializeField]
  protected string SocketName;
  public string SocketOnName { get { return SocketName; } }
  public abstract void Function(SocketIOEvent NetworkEvent);
  public abstract NetworkCallback NCallback();
}

[Serializable]
public class NetworkFunctionComponent : AbstractNetworkFunctions
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    Debug.LogFormat("Calling {0}", SocketOnName);
  }

  public override NetworkCallback NCallback()
  {
    throw new NotImplementedException();
  }
}