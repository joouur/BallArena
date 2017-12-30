﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public partial class GameNetwork : MonoBehaviour
{
  private static GameNetwork _instance;
  public static GameNetwork Instance { get { if (_instance == null) { GameNetwork i = FindObjectOfType<GameNetwork>(); _instance = i; } return _instance; } }

  public static SocketIOComponent socket;
  public NetworkBroadcastingData Data;
  public Dictionary<string, INetworkFunctionFactory> SocketDictionary = new Dictionary<string, INetworkFunctionFactory>();

  public void Start()
  {
    Data.CreateDictionary(out SocketDictionary);
  }

  public void AddNetworkEvents()
  {
    if (SocketDictionary.Count <= 0)
    { return; }
    for (int i = 0; i < Data.NetworkSocketEventNames.Count; ++i)
    {
      GameNetwork.socket.On(SocketDictionary[Data.NetworkSocketEventNames[i]].SocketOnName, SocketDictionary[Data.NetworkSocketEventNames[i]].Function);
    }
  }
}

public static class NetworkExtensionsAndtools
{
  public static void SocketEvent(this MonoBehaviour behaviour, string SocketName)
  {
    GameNetwork.socket.Emit(SocketName);
  }
  public static void SocketEvent(this Transform behaviour, string SocketName)
  {
    GameNetwork.socket.Emit(SocketName);
  }
  public static void SocketEvent(this GameObject behaviour, string SocketName)
  {
    GameNetwork.socket.Emit(SocketName);
  }
}