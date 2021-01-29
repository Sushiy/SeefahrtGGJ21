using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OrbitalCamera : MonoBehaviour
{
    public Transform m_target;


    [SerializeField] private float m_armLength = 5f;

    public Vector2 m_orbitAngles;

    public float m_rotationSpeed = 90f;
    
    
    void Start()
    {
        m_target = FindObjectOfType<BoatManager>().transform;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(m_target);
        
        RotateCamera();
        Quaternion lookRot = Quaternion.Euler(m_orbitAngles);
        Vector3 lookDir = lookRot * Vector3.forward;
        Vector3 lookPos = m_target.position - lookDir * m_armLength;
        transform.SetPositionAndRotation(lookPos, lookRot);
    }

    private void RotateCamera()
    {
        Vector2 input = new Vector2(Input.GetAxis("HorCam"), Input.GetAxis("VertCam"));
        if (input.sqrMagnitude > 0.1f)
        {
            m_orbitAngles += m_rotationSpeed * Time.fixedDeltaTime * input;
        }
    }
}
