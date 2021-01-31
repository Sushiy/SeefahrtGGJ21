
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityTemplateProjects.Boat;

[RequireComponent(typeof(BoatControl))]
public class BoatManager : MonoBehaviour
{
    public BoatControl m_boatController;
   
    public float m_steerFactor;

    public bool IsMoving;

    public float TurnAxis;
    
    // Start is called before the first frame update
    void Awake()
    {
        m_boatController = GetComponent<BoatControl>();
        gameObject.AddComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PopupOpenButton.IsAnyPopupOnStack()) return;
    }

    private void FixedUpdate()
    {        
        Move();
        Steer();
    }

    private void Move()
    {
        float forwardVel = m_boatController.m_windVelocity;
        IsMoving = forwardVel > 0;
        transform.position += transform.forward * forwardVel * Time.fixedDeltaTime;
    }

    public float slowTurnMultiplier;
    public AnimationCurve slowTurnFactorCurve;
    private void Steer()
    {
        float turnVel = m_boatController.TurnVel < 0 ? m_boatController.TurnVel * -1 : m_boatController.TurnVel;
        float slowTurnMultiplierT = (1.0f - (m_boatController.m_windVelocity) / m_boatController.m_maxVerticalSpeed);
        slowTurnMultiplier = slowTurnFactorCurve.Evaluate(slowTurnMultiplierT);

        m_steerFactor = Mathf.Lerp(m_steerFactor, TurnAxis, Time.fixedDeltaTime);
        
        transform.Rotate(Vector3.up, m_steerFactor * turnVel * slowTurnMultiplier);
    }
}
