using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using SocketIO;

[CreateAssetMenu(fileName = "DATA", menuName = "Network/Broadcasting", order = 1)]
public class NetworkBroadcastingData : ScriptableObject
{
  public List<string> NetworkSocketEventNames = new List<string>();

  public void OnEnable()
  {
   
  }

  public Dictionary<string, NetworkFunctionComponent> CreateDictionary()
  {
    Dictionary<string, NetworkFunctionComponent> SocketDictionary = new Dictionary<string, NetworkFunctionComponent>();
    for (int i = 0; i < NetworkSocketEventNames.Count; ++i)
    {
      Debug.LogFormat("Finding {0}", NetworkSocketEventNames[i]);

      NetworkFunctionComponent socketFunction = LoadNetworkClass(NetworkSocketEventNames[i]);
      if (socketFunction != null && SocketDictionary.ContainsKey(NetworkSocketEventNames[i]) == false &&
        SocketDictionary.ContainsValue(socketFunction) == false)
      {
        SocketDictionary.Add(NetworkSocketEventNames[i], socketFunction);
      }
    }
    return SocketDictionary;
  }
  public NetworkFunctionComponent LoadNetworkClass(string name)
  {
    return Assembly.GetExecutingAssembly().CreateInstance(name) as NetworkFunctionComponent;
  }
}