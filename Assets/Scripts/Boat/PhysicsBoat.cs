using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBoat : MonoBehaviour, ISailLevelProvider
{
    [Header("Runtime Variables")]
    /// <summary>
    /// [0,1] how far down is the sail
    /// </summary>
    public float sailPower;
    private Vector3 acceleration;

    Vector3 localRudderDirection;
    private Vector3 localRudderForce;

    private Vector3 localKeelStraighteningForce;

    public float currentSteeringValue;

    [Header("Boat Settings")]
    public float sailRaisingSpeed;
    public float sailEfficiencyFactor;

    public float windFactor;
    public float effectiveWind;
    public float keelStraghteningPower;

    [Header("Turning Settings")]
    public float rudderPower;
    public float turnResetSpeed;

    public float minAngularDrag = 0.4f;
    public float maxAngularDrag = 0.9f;
    public float maxAngularDragSpeed = 3.0f;

    public Transform rudderPosition;
    public Transform keelPosition;

    public Transform child;

    Rigidbody rigid;

    private float v, h;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        v = Input.GetAxis("SpeedUp");
        h = -Input.GetAxis("Turn");

        sailPower += v * sailRaisingSpeed * Time.deltaTime;
        sailPower = Mathf.Clamp(sailPower, 0, 1);
        currentSteeringValue = Mathf.Lerp(currentSteeringValue, h, 1 - Mathf.Exp(-turnResetSpeed * Time.deltaTime));
        if(Mathf.Abs(currentSteeringValue - h) < 0.01f)
        {
            currentSteeringValue = h;
        }

        windFactor = 1.0f;// Vector3.Dot(transform.forward, WindSource.WindDirection);

        DrawArrowHelper.DrawRay(transform.position, rigid.velocity * 1.0f, Color.yellow);

        DrawArrowHelper.DrawRay(rudderPosition.position, transform.TransformDirection(-localRudderDirection), Color.green);
        DrawArrowHelper.DrawRay(rudderPosition.position + transform.TransformDirection(-localRudderDirection)/2, transform.TransformDirection(localRudderForce), Color.red);

        DrawArrowHelper.DrawRay(keelPosition.position, transform.TransformDirection(localKeelStraighteningForce), Color.red);
        
        DrawArrowHelper.DrawRay(transform.position, WindSource.WindDirection, Color.blue);

        float swayT = currentSteeringValue;

        float swayAngle = swayT * 20.0f;
        //child.localRotation = Quaternion.Euler(swayAngle, 90, 0);
    }

    private void FixedUpdate()
    {
        //Calculate RudderForce
        localRudderDirection = Quaternion.Euler(0, Mathf.LerpUnclamped(0.0f, 35.0f, currentSteeringValue), 0) * Vector3.forward;
        localRudderForce = currentSteeringValue * Vector3.Cross(-localRudderDirection, Vector3.up);
        //DrawArrowHelper.DrawRay(rudderPosition.position, transform.TransformDirection(rudderForceDirection), Color.magenta);
        //localRudderForce = rudderForceDirection * (1.0f - Vector3.Dot(transform.TransformDirection(localRudderDirection), (rigid.GetRelativePointVelocity(Vector3.zero).magnitude < 0.1f) ? transform.forward : rigid.velocity));

        //Calculate Sailacceleration
        effectiveWind = WindSource.WindForce * windFactor;
        acceleration = transform.forward * (sailPower * effectiveWind);

        //Calculate Keel straighteningForce
        localKeelStraighteningForce = rigid.velocity.sqrMagnitude * keelStraghteningPower * Vector3.right * (Vector3.Dot(rigid.velocity, transform.right));

        //Update the rigidBody
        rigid.AddForceAtPosition(transform.TransformDirection(localRudderForce) * rudderPower * rigid.velocity.sqrMagnitude * Time.fixedDeltaTime, rudderPosition.position, ForceMode.Acceleration);

        rigid.AddForceAtPosition(transform.TransformDirection(localKeelStraighteningForce) * Time.fixedDeltaTime, keelPosition.position, ForceMode.Acceleration);
        rigid.AddForce(acceleration * Time.fixedDeltaTime, ForceMode.Acceleration);
        rigid.angularDrag = Mathf.Lerp(minAngularDrag, maxAngularDrag, rigid.velocity.sqrMagnitude / maxAngularDragSpeed);
    }

    public float GetSailLevel()
    {
        return sailPower;
    }
    public float GetWindFactor()
    {
        return windFactor;
    }
}
