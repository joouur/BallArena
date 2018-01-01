using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Reflection;

public partial class GameNetwork : MonoBehaviour
{
  private static GameNetwork _instance;
  public static GameNetwork Instance { get { if (_instance == null) { GameNetwork i = FindObjectOfType<GameNetwork>(); _instance = i; } return _instance; } }

  public static SocketIOComponent socket;
  public NetworkBroadcastingData Data;
  public Dictionary<string, NetworkFunctionComponent> SocketDictionary = new Dictionary<string, NetworkFunctionComponent>();

  public void Start()
  {
    socket = GetComponent<SocketIOComponent>();
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

      NetworkFunctionComponent socketFunction = LoadNetworkClass(Data.NetworkSocketEventNames[i]);
      if (socketFunction != null && SocketDictionary.ContainsKey(Data.NetworkSocketEventNames[i]) == false &&
        SocketDictionary.ContainsValue(socketFunction) == false)
      {
        SocketDictionary.Add(Data.NetworkSocketEventNames[i], socketFunction);
      }
    }
    return SocketDictionary;
  }

  public NetworkFunctionComponent LoadNetworkClass(string name)
  {
    return Assembly.GetExecutingAssembly().CreateInstance(name) as NetworkFunctionComponent;
  }

  public static NetworkFunctionComponent GetNetworkComponent(string name)
  {
    if (Instance.SocketDictionary.ContainsKey(name))
    { return Instance.SocketDictionary[name]; }
    NetworkFunctionComponent component = Instance.LoadNetworkClass(name);
    if (component != null)
    {
      Instance.SocketDictionary.Add(name, component);
      GameNetwork.socket.On(name, Instance.SocketDictionary[name].Function);
      return component;
    }
    Debug.LogWarningFormat("Compoenent {0} does not exist", name);
    return null;
  }

}