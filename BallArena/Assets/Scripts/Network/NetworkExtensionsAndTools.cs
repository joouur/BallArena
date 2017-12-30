using UnityEngine;
using System.Collections;

public static class NetworkExtensionsAndTools
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
  public static void SetUpdateForTransform(this Transform transform, UpdateTransform component)
  {
    component.transform = transform;
    transform.SocketEvent(component.SocketOnName);
  }
}