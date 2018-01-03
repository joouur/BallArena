using UnityEngine;
using System;
using System.Collections;
using SocketIO;

public class OnUpdateTransform : NetworkFunctionComponent
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    PlayerInformation player = MainGameManager.GetPlayer(NetworkEvent.data["ID"].str);
    if (NCallback != null)
    {
      NCallback(NetworkEvent);
    }
    player.transform.position = player.transform.position.GetVector3FromJSon(NetworkEvent);
    player.transform.rotation = player.transform.rotation.GetQuaternionFromJSon(NetworkEvent);
  }
}

public class OnUpdateRotation : OnUpdateTransform
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    PlayerInformation player = MainGameManager.GetPlayer(NetworkEvent.data["ID"].str);
    player.transform.rotation = player.transform.rotation.GetQuaternionFromJSon(NetworkEvent);
  }
}

public class OnUpdatePosition : OnUpdateTransform
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    PlayerInformation player = MainGameManager.GetPlayer(NetworkEvent.data["ID"].str);
    player.transform.position = player.transform.position.GetVector3FromJSon(NetworkEvent);
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
    GameNetwork.socket.Emit("OnUpdatePosition", Extensions.Vector3ToJson(transform.position));
    GameNetwork.socket.Emit("OnUpdateRotation", Extensions.QuaternionToJson(transform.rotation));
  }

  public void SetTransform(Transform transformToSet)
  {
    transform = transformToSet;
  }
}

public class OnRequestRotation : OnRequestTransform
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    GameNetwork.socket.Emit(this.SocketOnName, Extensions.QuaternionToJson(transform.rotation));
  }
}

public class OnRequestPosition : OnRequestTransform
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    GameNetwork.socket.Emit(this.SocketOnName, Extensions.Vector3ToJson(transform.position));
  }
}