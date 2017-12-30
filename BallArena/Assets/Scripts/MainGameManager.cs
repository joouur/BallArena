using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MainGameManager : MonoBehaviour
{
  private static MainGameManager _instance;
  public static MainGameManager Instance { get { if (_instance == null) { MainGameManager i = FindObjectOfType<MainGameManager>(); _instance = i; } return _instance; } }

  private void Awake()
  {
    
  }

  private void Start()
  {
    
  }
}
