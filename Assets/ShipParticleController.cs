using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipParticleController : MonoBehaviour
{
    BoatControl controller;

    public ParticleSystem bugWellenPS_L;
    ParticleSystem.VelocityOverLifetimeModule velocity_L;
    public ParticleSystem bugWellenPS_R;
    ParticleSystem.VelocityOverLifetimeModule velocity_R;
    public ParticleSystem trail;
    ParticleSystem.VelocityOverLifetimeModule velocity_Trail;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<BoatControl>();
        velocity_L = bugWellenPS_L.velocityOverLifetime;
        velocity_R = bugWellenPS_R.velocityOverLifetime;
        velocity_Trail = trail.velocityOverLifetime;

    }

    // Update is called once per frame
    void Update()
    {
        float normalizedSpeed = controller.ForwardVel/ controller.m_maxVerticalSpeed;
        bugWellenPS_L.emissionRate = 200 * normalizedSpeed;
        velocity_L.speedModifierMultiplier = normalizedSpeed;
        bugWellenPS_R.emissionRate = 200 * normalizedSpeed;
        velocity_R.speedModifierMultiplier = normalizedSpeed;

        velocity_Trail.speedModifierMultiplier = normalizedSpeed;
    }
}
