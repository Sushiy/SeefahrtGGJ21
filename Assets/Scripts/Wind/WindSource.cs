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

    private void Awake()
    {
        Me = transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, transform.forward * 10);
    }
}
