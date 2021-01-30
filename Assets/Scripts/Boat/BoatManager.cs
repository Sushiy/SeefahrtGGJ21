﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

[RequireComponent(typeof(BoatControl))]
public class BoatManager : MonoBehaviour
{
    public BoatControl m_boatController;
   
    public float m_steerFactor;

    // Start is called before the first frame update
    void Start()
    {
        m_boatController = GetComponent<BoatControl>();
    }

    // Update is called once per frame
    void Update()
    {
        var vertSpeed = Input.GetAxis("SpeedUp");
        if (vertSpeed != 0)
        {
            m_boatController.AddSpeed(vertSpeed);
        }

        var horizontSpeed = Input.GetAxis("Turn");
        if (horizontSpeed != 0)
        {
            m_boatController.AddTurnSpeed(horizontSpeed);
        }
    }

    private void FixedUpdate()
    {        
        Move();
        Steer();
    }

    private void Move()
    {
        float forwardVel = Mathf.Abs(m_boatController.ForwardVel) <= 0.5f ? 0 : m_boatController.ForwardVel;
        transform.position += transform.forward * forwardVel * Time.fixedDeltaTime;
    }

    public float slowTurnMultiplier;
    public AnimationCurve slowTurnFactorCurve;
    private void Steer()
    {
        float turnVel = m_boatController.TurnVel < 0 ? m_boatController.TurnVel * -1 : m_boatController.TurnVel;
        float slowTurnMultiplierT = (1.0f - (m_boatController.ForwardVel)/ m_boatController.m_maxVerticalSpeed);
        slowTurnMultiplier = slowTurnFactorCurve.Evaluate(slowTurnMultiplierT);

        m_steerFactor = Mathf.Lerp(m_steerFactor, Input.GetAxis("Turn"), Time.fixedDeltaTime);
        
        transform.Rotate(Vector3.up, m_steerFactor * turnVel * slowTurnMultiplier);
    }
}
