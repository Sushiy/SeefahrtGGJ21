using System;
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
    public float m_firstRotationDuration;
    public float m_secondRotationDuration;

    public Transform m_ship;
    private float m_firstTimeElapsed;
    private float m_secondTimeElapsed;
    private Quaternion m_frameRot;
    public Quaternion m_accumulatedQuat;
    private void Start()
    {
        m_currentDelta = Quaternion.identity;
        m_firstRotation = Quaternion.AngleAxis(-m_maxDelta, transform.forward);
        m_secondRotation = Quaternion.AngleAxis(m_maxSecondDelta, transform.forward);
        transform.rotation = m_firstRotation;
    }

    private void Update()
    {
        transform.rotation *= GetRotationDelta();
    }

    public Quaternion GetRotationDelta()
    {
        m_timeElapsed += Time.smoothDeltaTime;
        Quaternion rslt;
        if (m_timeElapsed < m_firstRotationDuration)
        {
            rslt = GetDeltaQuat(m_firstRotationDuration, m_firstTimeElapsed, m_firstRotation, m_secondRotation);
            m_firstTimeElapsed += Time.smoothDeltaTime;
        }
        else if (m_timeElapsed < m_firstRotationDuration + m_secondRotationDuration)
        {
            rslt = GetDeltaQuat(m_secondRotationDuration, m_secondTimeElapsed, m_secondRotation, m_firstRotation);
            m_secondTimeElapsed += Time.smoothDeltaTime;
        }
        else
        {
            m_firstTimeElapsed = 0;
            m_secondTimeElapsed = 0;
            m_timeElapsed = 0;
            rslt = Quaternion.identity;
        }
        m_lastRotation = m_frameRot;
        
        m_accumulatedQuat *= rslt;
        
        return rslt;

    }

    private Quaternion GetDeltaQuat(float duration, float timeElapsed, Quaternion targetRot, Quaternion currentRot)
    {
        m_frameRot = Quaternion.Lerp(currentRot, targetRot, timeElapsed / duration);
        return (m_lastRotation * Quaternion.Inverse(m_frameRot)).normalized;
        
    }
}
