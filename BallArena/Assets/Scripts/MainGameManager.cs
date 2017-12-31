using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MainGameManager : MonoBehaviour
{
  private static MainGameManager _instance;
  public static MainGameManager Instance { get { if (_instance == null) { MainGameManager i = FindObjectOfType<MainGameManager>(); _instance = i; } return _instance; } }

  public Dictionary<int, PlayerInformation> CurrentPlayersLUT = new Dictionary<int, PlayerInformation>();
  public ResourcesReferences references;

  public static GameObject SpawnPlayer(string id)
  {
    GameObject player = Instantiate(Instance.references.Player, Vector3.zero, Quaternion.identity) as GameObject;
    PlayerInformation info = player.GetComponent<PlayerInformation>();
    info.Initialize(id, player);

    return player;
  }

  public void AddPlayer(string id, PlayerInformation Info = null)
  {
    int ID = 0;
    Int32.TryParse(id, out ID);
    if(Info == null)
    {
      AddPlayer(ID, FindPlayerInformation(ID));
    }
  }

  public void AddPlayer(int id, PlayerInformation Info = null)
  {
    if (CurrentPlayersLUT.ContainsKey(id) == false)
    {
      CurrentPlayersLUT.Add(id, Info);
    }
    else { id = GetNewID(Info); AddPlayer(id, Info); }
  }

  public int GetNewID(PlayerInformation info)
  {
    return 0;
  }

  public PlayerInformation FindPlayerInformation(int ID)
  { return null; }

  public void RemovePlayer(int id)
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
}
