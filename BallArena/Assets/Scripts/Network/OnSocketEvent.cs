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
    Debug.Log("Player has connected, checking registry...");
    string ID = NetworkEvent.data["ID"].str;
    if (MainGameManager.CheckForExistingPlayer(ID))
    {
      MainGameManager.PopulateExistingPlayerInformation(ID);
    }
    else
    {
      MainGameManager.CreateNewPlayerInformation(ID);
    }
  }
}

public class OnRegister : NetworkFunctionComponent
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    Debug.LogFormat("Registering {0}", NetworkEvent.data["ID"].str);
    MainGameManager.Instance.AddPlayer(NetworkEvent.data["ID"].str);
  }
}

public class OnDisconnect : NetworkFunctionComponent
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    Debug.LogFormat("Calling {0}", SocketOnName);
  }
}

public class OnSpawn : NetworkFunctionComponent
{
  public override void Function(SocketIOEvent NetworkEvent)
  {
    Debug.LogFormat("Calling {0}", SocketOnName);
    GameObject player = MainGameManager.SpawnPlayer(NetworkEvent.data["ID"].str);

    if(NetworkEvent.data["x"])
    {
      Vector3 position = player.transform.position.GetVector3FromJSon(NetworkEvent);
      player.transform.position = position;
    }
  }
}