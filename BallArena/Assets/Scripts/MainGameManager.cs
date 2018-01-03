using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MainGameManager : MonoBehaviour
{
  private static MainGameManager _instance;
  public static MainGameManager Instance { get { if (_instance == null) { MainGameManager i = FindObjectOfType<MainGameManager>(); _instance = i; } return _instance; } }

  public Dictionary<string, PlayerInformation> CurrentPlayersLUT = new Dictionary<string, PlayerInformation>();

  public ResourcesReferences references;
  public GameObject OwnerPlayer;

  public static GameObject SpawnPlayer(string id)
  {
    Debug.LogFormat("Spawning {0}", id);
    GameObject player = Instantiate(Resources.Load(Instance.references.Player.name), Vector3.zero, Quaternion.identity) as GameObject;
    PlayerInformation info = player.GetComponent<PlayerInformation>();
    info.Initialize(id, player);
    if (Instance.OwnerPlayer == null)
    {
      Instance.OwnerPlayer = player;
      info.Controller.enabled = true;
    } else { info.Controller.enabled = false; }

    return player;
  }

  public void AddPlayer(string id, PlayerInformation Info = null)
  {
    if (OwnerPlayer == null)
    {
      OwnerPlayer = SpawnPlayer(id);
      Info = OwnerPlayer.GetComponent<PlayerInformation>();
    }
    if (CurrentPlayersLUT.ContainsKey(id) == false)
    {
      CurrentPlayersLUT.Add(id, Info);
    }
    else { id = GetNewID(Info); AddPlayer(id, Info); }
  }

  public string GetNewID(PlayerInformation info)
  {
    return null;
  }

  public PlayerInformation FindPlayerInformation(int ID)
  { return null; }

  public void RemovePlayer(string id)
  {
    if (CurrentPlayersLUT.ContainsKey(id) == false)
    {
      PlayerInformation info = CurrentPlayersLUT[id];
      CurrentPlayersLUT.Remove(id);
      info.DestroyPlayer();
    }
  }

  public static bool CheckForExistingPlayer(string ID)
  {
    return false;
  }
  public static bool PopulateExistingPlayerInformation(string ID)
  {
    return false;
  }
  public static bool CreateNewPlayerInformation(string ID)
  {
    return false;
  }
  public static PlayerInformation GetPlayer(string ID)
  {
    if(Instance.CurrentPlayersLUT.ContainsKey(ID) && Instance.CurrentPlayersLUT[ID] != null)
    {
      return Instance.CurrentPlayersLUT[ID];
    }
    return null;
  }
}
