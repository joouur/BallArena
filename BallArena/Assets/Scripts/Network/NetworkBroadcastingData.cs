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

  public void CreateDictionary(out Dictionary<string, INetworkFunctionFactory> SocketDictionary)
  {
    SocketDictionary = new Dictionary<string, INetworkFunctionFactory>();
    for (int i = 0; i < NetworkSocketEventNames.Count; ++i)
    {
      INetworkFunctionFactory socketFunction = LoadNetworkClass(NetworkSocketEventNames[i]);
      if (socketFunction != null && SocketDictionary.ContainsKey(NetworkSocketEventNames[i]) == false &&
        SocketDictionary.ContainsValue(socketFunction) == false)
      {
        SocketDictionary.Add(NetworkSocketEventNames[i], socketFunction);
      }
    }
  }
  static INetworkFunctionFactory LoadNetworkClass(string name)
  {
    return Assembly.GetExecutingAssembly().CreateInstance(name) as INetworkFunctionFactory;
  }
}