﻿using UnityEngine;
using System;
using System.Collections;
using SocketIO;

public class OnSocketEvent : NetworkFunctionComponent
{
  
}

public class OnConnect : NetworkFunctionComponent
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    Debug.LogFormat("Calling {0}", SocketOnName);
  }

  public override NetworkCallback NCallback(params System.Object[] ObjectsToAdd)
  {
    throw new NotImplementedException();
  }
}

public class OnRegister : NetworkFunctionComponent
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    Debug.LogFormat("Calling {0}", SocketOnName);
  }

  public override NetworkCallback NCallback(params System.Object[] ObjectsToAdd)
  {
    throw new NotImplementedException();
  }
}

public class OnDisconnect : NetworkFunctionComponent
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    Debug.LogFormat("Calling {0}", SocketOnName);
  }

  public override NetworkCallback NCallback(params System.Object[] ObjectsToAdd)
  {
    throw new NotImplementedException();
  }
}