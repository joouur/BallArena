using UnityEngine;
using System.Collections;

public class PlayerInformation : MonoBehaviour
{
  private string id;
  private GameObject playerObject;
  private CameraController cameraCon;
  private BallController ballController;

  public string ID { get { return id; } }
  public BallController Controller { get { return ballController; } }

  public void Initialize(string ID, GameObject PObject)
  {
    id = ID;
    playerObject = PObject;
    cameraCon = FindObjectOfType<CameraController>();
    ballController = GetComponent<BallController>();
  }

  public void DestroyPlayer()
  {
    Destroy(this.gameObject);
  }
}
