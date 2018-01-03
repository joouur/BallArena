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

  private JSONObject jIDPos;
  public JSONObject JSONID(Transform xform)
  {
    jIDPos.SetField("dx", xform.position.x);
    jIDPos.SetField("dy", xform.position.y);
    jIDPos.SetField("dz", xform.position.z);

    jIDPos.SetField("qx", xform.rotation.x);
    jIDPos.SetField("qy", xform.rotation.y);
    jIDPos.SetField("qz", xform.rotation.z);
    jIDPos.SetField("qw", xform.rotation.w);
    return jIDPos;
  }
  public void Initialize(string ID, GameObject PObject)
  {
    id = ID;
    playerObject = PObject;
    ballController = GetComponent<BallController>();
    jIDPos = new JSONObject(JSONObject.Type.OBJECT);
    jIDPos.AddField("ID", ID);
    jIDPos.AddField("dx", transform.position.x);
    jIDPos.AddField("dy", transform.position.y);
    jIDPos.AddField("dz", transform.position.z);

    jIDPos.AddField("qx", transform.rotation.x);
    jIDPos.AddField("qy", transform.rotation.y);
    jIDPos.AddField("qz", transform.rotation.z);
    jIDPos.AddField("qw", transform.rotation.w);
  }

  public void SetCamera()
  {
    cameraCon = FindObjectOfType<CameraController>();
    cameraCon.enabled = true;
  }
  public void DestroyPlayer()
  {
    Destroy(this.gameObject);
  }
}
