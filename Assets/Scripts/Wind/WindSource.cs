using System;
using UnityEngine;

public class WindSource : MonoBehaviour
{
    private static Transform Me;
    
    public static Vector3 WindDirection
    {
        get => Me.transform.forward;
    }

    public static float WindForce;

    public float windPower = 20.0f;

    private float targetAngle;
    public float currentAngle;
    public float windChangeSpeed = 0.5f;
    private bool changing = false;
    private void Awake()
    {
        Me = transform;
        currentAngle = transform.parent.localRotation.eulerAngles.y;
    }

    private void Update()
    {
        if (changing)
        {
            transform.parent.localRotation = Quaternion.Slerp(Quaternion.Euler(0, currentAngle, 0), Quaternion.Euler(0, targetAngle, 0), 1 - Mathf.Exp(-windChangeSpeed * Time.deltaTime));
            currentAngle = transform.parent.localRotation.eulerAngles.y;
            if (Mathf.Approximately(currentAngle, targetAngle))
            {
                changing = false;
                currentAngle = targetAngle;
            }
        }

        WindForce = windPower;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, transform.forward * 10);
    }

    public void SetWindDirection(float angle)
    {
        currentAngle = transform.parent.localRotation.eulerAngles.y;
        targetAngle = angle;
        changing = true;
    }
}
