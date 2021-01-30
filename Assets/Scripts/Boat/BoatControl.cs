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

    private float m_lastInput;
    [SerializeField]private float m_speedModifier;
    [SerializeField] private float m_turnModifier;

    public float m_maxVerticalSpeed;
    public float m_maxTurnSpeed;
    
    public void AddSpeed(float speed)
    {
        
        m_forwardVel += (speed * m_speedModifier);
        m_forwardVel = Mathf.Clamp(m_forwardVel, 0, m_maxVerticalSpeed);
    }

    public void AddTurnSpeed(float turnSpeed)
    {
        float sign = Mathf.Sign(turnSpeed);
        m_turnVel = sign == m_lastInput
            ? m_turnVel + (turnSpeed * m_turnModifier)
            : 0;
        m_turnVel = Mathf.Clamp(m_turnVel, -m_maxTurnSpeed, m_maxTurnSpeed);
        m_lastInput = sign;
    }
}
