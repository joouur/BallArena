using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
  private static CameraController _instance;
  public static CameraController Instance
  {
    get
    {
      if (_instance == null)
      {
        CameraController i = FindObjectOfType<CameraController>();
        _instance = i;
      }
      return _instance;
    }
  }

  private Camera mainCamera;
  public static Camera MainCamera
  {
    get
    {
      if (Instance.mainCamera == null)
      {
        Camera cam = Instance.GetComponentInChildren<Camera>();
        Instance.mainCamera = cam;
      }
      return Instance.mainCamera;
    }
  }

  public static Vector3 CameraForward
  { get { return Vector3.Scale(Instance.mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized; } }
  public static Vector3 CameraRight
  { get { return Instance.mainCamera.transform.right; } }

  public Transform TargetTransform;
  private BallController TargetController;

  private Transform PIVOT;
  private Vector3 pivotEuler;
  private Quaternion pivotTargetRot;
  private Quaternion TransformTargetRot;

  public float CameraMoveSpeed = 1.0f;
  public float CameraTurnSpeed = 1.5f;
  public float TurnSmooth = 0.0f;
  [Range(45, 120)]
  public float TiltMax = 75.0f;
  [Range(0, 60)]
  public float TiltMin = 45.0f;
  [Range(-20, 5)]
  public float Distance = 5;
  public Vector3 Offset;

  private float TiltAngle;
  private float LookAngle;

  public void Awake()
  {
    PIVOT = MainCamera.transform.parent;
    pivotEuler = PIVOT.rotation.eulerAngles;
    pivotTargetRot = PIVOT.transform.localRotation;
    TransformTargetRot = transform.localRotation;
  }

  private void FollowTargets(float deltaTime)
  {
    if (!(deltaTime > 0) || TargetTransform == null)
    {
      TargetTransform = MainGameManager.Instance.OwnerPlayer.transform;
      return;
    }
    Vector3 averageTransform = TargetTransform.transform.position;
    Vector3 lookAhead = Vector3.zero;
    averageTransform += Offset;
    Vector3 offsetPosition = averageTransform;
    offsetPosition.z += Distance;
    Vector3 offsetVector = averageTransform - offsetPosition;
    offsetVector = Quaternion.Euler(TiltMin, 0.0f, 0) * offsetVector;
    averageTransform += offsetVector;
    transform.position = Vector3.Lerp(transform.position, averageTransform, deltaTime * CameraMoveSpeed);
  }

  private void HandleRotationMovement()
  {
    if (Time.timeScale < float.Epsilon)
    { return; }

    float x = Input.GetAxis("RightLeft");
    float y = Input.GetAxis("UpDown");

    LookAngle += x * CameraTurnSpeed;
    TiltAngle -= y * CameraTurnSpeed;
    TiltAngle = Mathf.Clamp(TiltAngle, -TiltMin, TiltMax);

    TransformTargetRot = Quaternion.Euler(0.0f, LookAngle, 0.0f);

    pivotTargetRot = Quaternion.Euler(TiltAngle, pivotEuler.y, pivotEuler.z);
    if (TurnSmooth > 0)
    {
      PIVOT.localRotation = Quaternion.Slerp(PIVOT.localRotation, pivotTargetRot, TurnSmooth * Time.deltaTime);
      transform.localRotation = Quaternion.Slerp(transform.localRotation, TransformTargetRot, TurnSmooth * Time.deltaTime);
    }
    else
    {
      PIVOT.localRotation = pivotTargetRot;
      transform.localRotation = TransformTargetRot;
    }
  }

  private void FixedUpdate()
  {
    HandleRotationMovement();

    FollowTargets(Time.deltaTime);
  }
}
