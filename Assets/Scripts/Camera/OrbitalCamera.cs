﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OrbitalCamera : MonoBehaviour
{
    public Transform m_target;


    [SerializeField] private float m_armLength = 5f;

    public Vector2 m_orbitAngles = new Vector2(35,0);

    public float m_rotationSpeed = 90f;
    public float m_slerpSpeed = 1.0f;
    public bool m_shouldRotate;

    public bool invertY = true;
    
    void Start()
    {
        m_target = FindObjectOfType<BoatManager>().transform;
        transform.LookAt(m_target);
        m_shouldRotate = false;
        Quaternion lookRot = Quaternion.Euler(m_orbitAngles);
        Vector3 lookDir = lookRot * Vector3.forward;
        Vector3 lookPos = m_target.position - lookDir * m_armLength;
        transform.position = lookPos;
        transform.rotation = lookRot;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(m_target);
        
        Quaternion lookRot = Quaternion.Slerp(transform.rotation, Quaternion.Euler(m_orbitAngles), 1 - Mathf.Exp(-m_slerpSpeed * Time.deltaTime));
        Vector3 lookDir = lookRot * Vector3.forward;
        Vector3 lookPos = m_target.position - lookDir * m_armLength;
        transform.position = lookPos;
        transform.rotation = lookRot;
    }

    public void RotateCamera(Vector2 input, float magnitude, bool ignoreMousePress)
    {
       
        if (magnitude > 0.1f && (m_shouldRotate || ignoreMousePress))
        {
            m_orbitAngles += m_rotationSpeed * Time.fixedDeltaTime * input;
            m_orbitAngles.x = Mathf.Clamp(m_orbitAngles.x, 0, 90);
        }
    }
}
