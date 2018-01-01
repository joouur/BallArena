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
}