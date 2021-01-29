using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControl : MonoBehaviour
{

    public float m_forwardVel;
    public float m_turnVel;
    public float ForwardVel
    {
        get => m_forwardVel;
    }
    
    public float TurnVel
    {
        get => m_turnVel;
    }

    private Quaternion m_turnRotation;

    public Quaternion TurnRotation
    {
        get => m_turnRotation;
    }

    [SerializeField]private float m_speedModifier;
    [SerializeField] private float m_turnModifier;

    public float m_maxVerticalSpeed;
    public float m_maxTurnSpeed;
    
    public void AddSpeed(float speed)
    {
        
        m_forwardVel += (speed * m_speedModifier);
        m_forwardVel = Mathf.Clamp(m_forwardVel, -m_maxVerticalSpeed, m_maxVerticalSpeed);
    }

    public void AddTurnSpeed(float turnSpeed)
    {
        m_turnVel += (turnSpeed * m_turnModifier);
        m_turnVel = Mathf.Clamp(m_turnVel, -m_maxTurnSpeed, m_maxTurnSpeed);
        m_turnRotation = Quaternion.AngleAxis(m_turnVel * m_turnModifier, transform.up);
    }
}
