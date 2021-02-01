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

    public float m_windVelocity;
    
    public float TurnVel
    {
        get => m_turnVel;
    }

    private float m_lastInput;
    [SerializeField]private float m_speedModifier;
    [SerializeField] private float m_turnModifier;

    [Header("Limits")]
    public float m_maxVerticalSpeed;
    public float m_maxTurnSpeed;

    [Header ("Wind")]
    public float m_maxDeaccelerationPercentage;
    public float m_maxSpeedReductionPercentage;

    public float m_currentWindFactor;

    QuestSubsystem subsystem;

    private void Start()
    {
        GetComponent<QuestSubsystem>().onNewQuestFinished.AddListener((a) => {StopBoat(); });
    }

    private void Update()
    {
        m_currentWindFactor = (Vector3.Dot(WindSource.WindDirection, transform.forward) + 1) * 0.5f;
        m_windVelocity = CheckWindForward(m_forwardVel, 1 - m_maxSpeedReductionPercentage);
    }

    public void AddSpeed(float speed)
    {
        var accel = speed * m_speedModifier * Time.deltaTime;
        // Make stopping faster against the wind too
        m_forwardVel += accel > 0 ?
            CheckWindForward(accel, m_maxDeaccelerationPercentage) :
            CheckWindForward(accel, 1 + m_maxDeaccelerationPercentage);
        m_forwardVel = Mathf.Clamp(m_forwardVel, 0, m_maxVerticalSpeed);
    }

    public void AddTurnSpeed(float turnSpeed)
    {
        float sign = Mathf.Sign(turnSpeed);
        m_turnVel = (sign == m_lastInput)
            ? m_turnVel + (turnSpeed * m_turnModifier * Time.deltaTime)
            : 0;
        m_turnVel = Mathf.Clamp(m_turnVel, -m_maxTurnSpeed, m_maxTurnSpeed);
        m_lastInput = sign;
    }
    
    private float CheckWindForward(float rawVelocity, float decreasePercentage)
    {
        return Mathf.Lerp(rawVelocity, rawVelocity * decreasePercentage, 1 - m_currentWindFactor);
    }

    public void StopBoat()
    {
        m_forwardVel = 0;
        m_turnVel = 0;
    }
}
