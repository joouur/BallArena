﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public class BallController : MonoBehaviour
{
  private Rigidbody ballRigidbody;
  private SphereCollider mainBallCollider;
  public List<Collider> PowerUpColliders = new List<Collider>();

  public BallStats ballStats;
  private const float GroundRayLength = 1f;
  private Vector3 moveDirection;
  private bool jump;

  private PlayerInformation playerInformation;

  private OnUpdateTransform onUpdateTransform;
  private OnRequestTransform onRequestTransform;

  private void Start()
  {
    if (ballRigidbody == null)
    { ballRigidbody = GetComponent<Rigidbody>(); }
    ballRigidbody.maxAngularVelocity = ballStats.MaxAngularSpeed;
    playerInformation = GetComponent<PlayerInformation>();
    onUpdateTransform = GameNetwork.GetNetworkComponent("OnUpdateTransform") as OnUpdateTransform;
    onRequestTransform = GameNetwork.GetNetworkComponent("OnRequestTransform") as OnRequestTransform;
    onRequestTransform.transform = this.transform;
  }

  public virtual void FixedUpdate()
  {
    Move(moveDirection, jump);
    GameNetwork.socket.Emit("OnUpdateTransform", playerInformation.JSONID(this.transform));
  }

  public virtual void Update()
  {
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    jump = Input.GetButton("Jump");
    if (h != 0.0 || v != 0.0)
    {
      moveDirection = (v * CameraController.CameraForward + h * CameraController.CameraRight);
    }
  }

  public void OnCollisionEnter(Collision collision)
  {
    if (collision.collider.tag == "Player")
    {
      collision.rigidbody.AddForce(CalculateForceToSend(CameraController.CameraForward, this.ballRigidbody.velocity), ForceMode.Impulse);
    }
  }

  private void Move(Vector3 direction, bool jump)
  {
    if (Physics.Raycast(transform.position, Vector3.down, GroundRayLength) == false)
    {
      return;
    }
    if (ballStats.UsingTorque)
    { ballRigidbody.AddTorque(new Vector3(direction.z, 0, -direction.x) * ballStats.MovingPower); }
    else
    { ballRigidbody.AddTorque(direction * ballStats.MovingPower); }
    if (jump)
    {
      ballRigidbody.AddForce(Vector3.up * ballStats.MaxJumpPower, ForceMode.Impulse);
      jump = false;
    }
  }
  public Vector3 CalculateForceToSend(Vector3 Direction, Vector3 Velocity)
  {
    float forceLowEnd = 10, forceHighEnd = 250;

    float actorForceScaleLowEnd = 0, actorForceScaleHighEnd = 360;
    float actorForceScaleValue = Mathf.InverseLerp(actorForceScaleLowEnd, actorForceScaleHighEnd, Velocity.magnitude * ballStats.AvailableForce);

    Vector3 appliedForce = Direction;
    appliedForce = new Vector3(appliedForce.x, Mathf.Clamp(appliedForce.y, 0, 1), appliedForce.z).normalized * Mathf.Lerp(forceLowEnd, forceHighEnd, actorForceScaleValue);


    return appliedForce;
  }
}


[System.Serializable]
public class BallStats
{
  public float MovingPower;
  public bool UsingTorque = true;
  public float MaxAngularSpeed;
  public float MaxJumpPower;
  [Range(0.0f, 1.5f)]
  public float AvailableForce = 0.25f;
}

public static class BallControllerExtension
{
  public static void OnMove(this BallController controller, Vector3 position, Quaternion rotation, float speed)
  {
    controller.transform.position = Vector3.Lerp(controller.transform.position, position, Time.fixedDeltaTime * speed);
    controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, rotation, Time.deltaTime * speed);
  }
}