using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public partial class GameNetwork : MonoBehaviour
{
  private static GameNetwork _instance;
  public static GameNetwork Instance { get { if (_instance == null) { GameNetwork i = FindObjectOfType<GameNetwork>(); _instance = i; } return _instance; } }

  public static SocketIOComponent socket;
  public NetworkBroadcastingData Data;
  public Dictionary<string, NetworkFunctionComponent> SocketDictionary = new Dictionary<string, NetworkFunctionComponent>();

  public void Start()
  {
    SocketDictionary = CreateDictionary();
    AddNetworkEvents();
  }

  public void AddNetworkEvents()
  {
    if (SocketDictionary.Count <= 0)
    { return; }
    for (int i = 0; i < Data.NetworkSocketEventNames.Count; ++i)
    {
      GameNetwork.socket.On(Data.NetworkSocketEventNames[i], SocketDictionary[Data.NetworkSocketEventNames[i]].Function);
    }
  }
  public Dictionary<string, NetworkFunctionComponent> CreateDictionary()
  {
    Dictionary<string, NetworkFunctionComponent> SocketDictionary = new Dictionary<string, NetworkFunctionComponent>();
    for (int i = 0; i < Data.NetworkSocketEventNames.Count; ++i)
    {
      Debug.LogFormat("Finding {0}", Data.NetworkSocketEventNames[i]);

      NetworkFunctionComponent socketFunction = Data.LoadNetworkClass(Data.NetworkSocketEventNames[i]);
      if (socketFunction != null && SocketDictionary.ContainsKey(Data.NetworkSocketEventNames[i]) == false &&
        SocketDictionary.ContainsValue(socketFunction) == false)
      {
        SocketDictionary.Add(Data.NetworkSocketEventNames[i], socketFunction);
      }
    }
    return SocketDictionary;
  }
}