﻿using System;
using UnityEngine;

public class BoatRotator : MonoBehaviour
{
    private Quaternion m_currentDelta;
    private Quaternion m_firstRotation;
    private Quaternion m_secondRotation;
    private Quaternion m_lastRotation;
    public float m_maxDelta;
    public float m_maxSecondDelta;
    public float m_timeElapsed;
    public AnimationCurve pingPong;
    public float m_firstRotationDuration;
    public float m_secondRotationDuration;
    private float m_adjustedFirstDuration;
    private float m_adjustedSecondDuration;
    
    public BoatManager m_boatManager;
    private float m_firstTimeElapsed;
    private float m_secondTimeElapsed;
    private Quaternion m_frameRot;
    private Quaternion m_accumulatedQuat;

    enum ShipState
    {
        ShipMoving,
        ShipHalting
    };

    private ShipState m_shipState;

    private void Awake()
    {
        m_boatManager = GetComponentInParent<BoatManager>();
    }

    private void Start()
    {
        m_firstRotation = Quaternion.Euler(m_maxDelta, 90, 0);
        m_secondRotation = Quaternion.Euler(m_maxSecondDelta, 90, 0);

        m_accumulatedQuat = Quaternion.identity;
        
        m_adjustedFirstDuration = m_firstRotationDuration + m_boatManager.m_boatController.m_forwardVel;
        m_adjustedSecondDuration = m_secondRotationDuration + m_boatManager.m_boatController.m_forwardVel;
    }

    private void Update()
    {
        if (m_boatManager.IsMoving && m_shipState == ShipState.ShipHalting)
        {
            transform.localRotation = m_secondRotation;
            m_shipState = ShipState.ShipMoving;
        }
        else if(!m_boatManager.IsMoving)
        {
            m_shipState = ShipState.ShipHalting;
        }
        if (m_shipState == ShipState.ShipMoving)
        {
            GetRotationDelta();
            float swayT = m_boatManager.m_steerFactor;

            float swayAngle = swayT * -20.0f;

            transform.localRotation = Quaternion.Euler(swayAngle, 90, 0) * m_accumulatedQuat;
        }

        if (m_shipState == ShipState.ShipHalting)
        {
           
            m_firstTimeElapsed = 0;
            m_secondTimeElapsed = 0;
            m_timeElapsed = 0;
            m_accumulatedQuat = Quaternion.identity;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 90, 0),
                1 - Mathf.Exp(-1 * Time.deltaTime));
        }
    }

    public void GetRotationDelta()
    {
        m_timeElapsed += Time.smoothDeltaTime;
        if (m_timeElapsed < m_adjustedFirstDuration)
        {
            m_accumulatedQuat *= GetDeltaQuat(m_adjustedFirstDuration, m_firstTimeElapsed, m_firstRotation, m_secondRotation);
            m_firstTimeElapsed += Time.smoothDeltaTime;
        }
        else if (m_timeElapsed < m_adjustedFirstDuration + m_adjustedSecondDuration)
        {
            m_accumulatedQuat *= GetDeltaQuat(m_adjustedSecondDuration, m_secondTimeElapsed, m_secondRotation, m_firstRotation);
            m_secondTimeElapsed += Time.smoothDeltaTime;
        }
        else
        {
            m_adjustedFirstDuration = m_firstRotationDuration + m_boatManager.m_boatController.m_forwardVel;
            m_adjustedSecondDuration = m_secondRotationDuration + m_boatManager.m_boatController.m_forwardVel;
            
            m_firstTimeElapsed = 0;
            m_secondTimeElapsed = 0;
            m_timeElapsed = 0;
        }
        m_lastRotation = m_frameRot;
    }

    private Quaternion GetDeltaQuat(float duration, float timeElapsed, Quaternion targetRot, Quaternion currentRot)
    {
        m_frameRot = Quaternion.Lerp(currentRot, targetRot, timeElapsed / duration);
        return (m_lastRotation * Quaternion.Inverse(m_frameRot)).normalized;
    }
}
