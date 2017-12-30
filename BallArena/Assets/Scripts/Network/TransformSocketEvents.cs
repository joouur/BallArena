using UnityEngine;
using System;
using System.Collections;
using SocketIO;

public class UpdateTransform : NetworkFunctionComponent
{
  public Transform transform;
  public override void Function(SocketIOEvent NetworkEvent)
  {
    if(NCallback() != null)
    {

    }
    transform.position = transform.position.GetVector3FromJSon(NetworkEvent);
    transform.rotation = transform.rotation.GetQuaternionFromJSon(NetworkEvent);
  }

  public override NetworkCallback NCallback(params System.Object[] ObjectsToAdd)
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

  public override NetworkCallback NCallback(params System.Object[] ObjectsToAdd)
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

  public override NetworkCallback NCallback(params System.Object[] ObjectsToAdd)
  {
    throw new NotImplementedException();
  }
}