using UnityEngine;
using System.Collections;

public static class Extensions
{
  public static bool AlmostEquals(this float target, float second, float floatDiff)
  {
    return Mathf.Abs(target - second) < floatDiff;
  }
  public static Vector3 GetVector3FromJSon(this Vector3 vector, SocketIO.SocketIOEvent Event)
  {
    vector.x = Event.data["x"].n;
    vector.y = Event.data["y"].n;
    vector.z = Event.data["z"].n;
    return vector;
  }
  public static Vector3 GetVector3FromJSon(SocketIO.SocketIOEvent Event)
  { return new Vector3(Event.data["x"].n, Event.data["y"].n, Event.data["z"].n); }
  public static Vector2 GetVector2FromJSon(this Vector3 vector, SocketIO.SocketIOEvent Event)
  {
    vector.x = Event.data["x"].n;
    vector.y = Event.data["y"].n;
    return vector;
  }
  public static Vector2 GetVector2FromJSon(SocketIO.SocketIOEvent Event)
  {
    return new Vector2(Event.data["x"].n, Event.data["y"].n);
  }
  public static Quaternion GetQuaternionFromJSon(this Quaternion quaternion, SocketIO.SocketIOEvent Event)
  {
    quaternion.x = Event.data["x"].n;
    quaternion.y = Event.data["y"].n;
    quaternion.z = Event.data["z"].n;
    quaternion.w = Event.data["w"].n;
    return quaternion;
  }
  public static Quaternion GetQuaternionFromJSon(SocketIO.SocketIOEvent Event)
  { return new Quaternion(Event.data["x"].n, Event.data["y"].n, Event.data["z"].n, Event.data["w"].n); }
  public static JSONObject QuaternionToJson(this Quaternion quaternion)
  {
    JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
    j.AddField("x", quaternion.x);
    j.AddField("y", quaternion.y);
    j.AddField("z", quaternion.z);
    j.AddField("w", quaternion.w);
    return j;
  }
  public static JSONObject VectorToJson(this Vector3 vector)
  {
    JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
    j.AddField("x", vector.x);
    j.AddField("y", vector.y);
    j.AddField("z", vector.z);
    return j;
  }
}
