using UnityEngine;
using System.Collections;

public class PlayerInformation : MonoBehaviour
{
  private string id;
  private GameObject playerObject;
  private CameraController cameraCon;


  public void Initialize(string ID, GameObject PObject)
  {
    id = ID;
    playerObject = PObject;
    cameraCon = FindObjectOfType<CameraController>();
    cameraCon.TargetTransform = this.transform;
  }
  public void DestroyPlayer()
  { }
}
