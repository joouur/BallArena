using UnityEngine;
using System;
using System.Collections;
using SocketIO;

public class OnUpdateTransform : NetworkFunctionComponent
{
  public Transform transform;
  public override void Function(SocketIOEvent NetworkEvent)
  {
    if (NCallback != null)
    {
      NCallback(NetworkEvent);
    }
    transform.position = transform.position.GetVector3FromJSon(NetworkEvent);
    transform.rotation = transform.rotation.GetQuaternionFromJSon(NetworkEvent);
  }

  public void SetTransform(Transform transformToSet)
  {
    transform = transformToSet;
  }
}

public class OnUpdateRotation : OnUpdateTransform
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    transform.rotation = transform.rotation.GetQuaternionFromJSon(NetworkEvent);
  }
}

public class OnUpdatePosition : OnUpdateTransform
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    transform.position = transform.position.GetVector3FromJSon(NetworkEvent);
  }
}

public class OnRequestTransform : NetworkFunctionComponent
{
  public Transform transform;
  public override void Function(SocketIOEvent NetworkEvent)
  {
    if (NCallback != null)
    {
      NCallback(NetworkEvent);
    }
    GameNetwork.socket.Emit(this.SocketOnName, Extensions.Vector3ToJson(transform.position));
    GameNetwork.socket.Emit(this.SocketOnName, Extensions.QuaternionToJson(transform.rotation));
  }

  public void SetTransform(Transform transformToSet)
  {
    transform = transformToSet;
  }
}

public class OnRequestRotation : OnUpdateTransform
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    GameNetwork.socket.Emit(this.SocketOnName, Extensions.QuaternionToJson(transform.rotation));
  }
}

public class OnRequestPosition : OnUpdateTransform
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    GameNetwork.socket.Emit(this.SocketOnName, Extensions.Vector3ToJson(transform.position));
  }
}