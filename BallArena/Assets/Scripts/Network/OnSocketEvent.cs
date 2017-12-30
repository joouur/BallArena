using UnityEngine;
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

  public override NetworkCallback NCallback()
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

  public override NetworkCallback NCallback()
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

  public override NetworkCallback NCallback()
  {
    throw new NotImplementedException();
  }
}

public class UpdateTransform : NetworkFunctionComponent
{
  public Transform transform;
  public override void Function(SocketIOEvent NetworkEvent)
  {
    transform.position = transform.position.GetVector3FromJSon(NetworkEvent);
    transform.rotation = transform.rotation.GetQuaternionFromJSon(NetworkEvent);
  }

  public override NetworkCallback NCallback()
  {
    throw new NotImplementedException();
  }
}

public class UpdateRotation : UpdateTransform
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    transform.rotation = transform.rotation.GetQuaternionFromJSon(NetworkEvent);
  }

  public override NetworkCallback NCallback()
  {
    throw new NotImplementedException();
  }
}

public class UpdatePosition : UpdateTransform
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    transform.position = transform.position.GetVector3FromJSon(NetworkEvent);
  }

  public override NetworkCallback NCallback()
  {
    throw new NotImplementedException();
  }
}