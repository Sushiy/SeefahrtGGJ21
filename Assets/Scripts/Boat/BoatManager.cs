
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

[RequireComponent(typeof(BoatControl))]
public class BoatManager : MonoBehaviour
{
    [SerializeField] private BoatControl m_boatController;
    private BoatRotator m_boatRotator;

   
    private Quaternion m_shakeRot;
    public float m_steerFactor;
    private Vector3 m_forwardVec;

    // Start is called before the first frame update
    void Start()
    {
        m_boatController = GetComponent<BoatControl>();
        m_boatRotator = GetComponentInParent<BoatRotator>();
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
       
        m_forwardVec = -transform.right;
        
        Move();
        Steer();
    }

    private void Move()
    {
        float forwardVel = Mathf.Abs(m_boatController.ForwardVel) <= 0.5f ? 0 : m_boatController.ForwardVel;
        transform.position += m_forwardVec * forwardVel * Time.fixedDeltaTime;
    }

    private void Steer()
    {
        float turnVel = m_boatController.TurnVel < 0 ? m_boatController.TurnVel * -1 : m_boatController.TurnVel;
        float offset = Mathf.Clamp(m_boatController.m_maxVerticalSpeed - m_boatController.ForwardVel, 0,
            m_boatController.m_maxTurnSpeed);
        m_steerFactor = Mathf.Lerp(m_steerFactor, Input.GetAxis("Turn"), Time.fixedDeltaTime * offset);
        
        transform.Rotate(Vector3.up, m_steerFactor * turnVel);
    }
}
