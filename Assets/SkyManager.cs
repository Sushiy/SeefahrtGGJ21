﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkyManager : MonoBehaviour
{
    public Transform sunParent;
    public float timeOfDay = 0.0f;
    public float dayLengthMinutes = 3.0f;
    private float timeSpeed;

    public Gradient skyGradient;
    public float skyLightIntensityControl = 1.0f;

    public UnityEvent<float> DayNightEvent { get; protected set; }
    private bool broadcasted;


    private int shader_TimeValue;

    // Start is called before the first frame update
    void Start()
    {
        timeSpeed = 24.0f / dayLengthMinutes / 60.0f;
        shader_TimeValue = Shader.PropertyToID("_TimeValue");
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay += timeSpeed * Time.deltaTime;
        if (timeOfDay > 24.0f)
        {
            timeOfDay -= 24.0f;
        }

        float timeOfDayNorm = timeOfDay / 24.0f;
        Shader.SetGlobalFloat(shader_TimeValue, timeOfDayNorm);
        RenderSettings.ambientLight = skyGradient.Evaluate(timeOfDayNorm) * skyLightIntensityControl;
        
        /// broadcast anything between 0-1 but >=0 and <=1 only once
        if (timeOfDayNorm >= 1.0F && !broadcasted)
        {
            broadcasted = true;
            DayNightEvent.Invoke(timeOfDayNorm);
        }
        else if (timeOfDayNorm <= 0.0F && !broadcasted)
        {
            broadcasted = true;
            DayNightEvent.Invoke(timeOfDayNorm);
        }
        else
        {
            DayNightEvent.Invoke(timeOfDayNorm);
            broadcasted = false;
        }

        float sunRotation = Mathf.Lerp(0, 360, timeOfDayNorm);
        sunParent.localRotation = Quaternion.Euler(-30.0f, 0.0f, sunRotation);
    }

    public float TimeOfDayNormalized => timeOfDay / 24.0F;

    bool IsNight(float Threshold = 0.5F)
    {
        return skyGradient.Evaluate(TimeOfDayNormalized).r <= Threshold;
    }

    bool IsDay(float Threshold = 0.5F)
    {
        return skyGradient.Evaluate(TimeOfDayNormalized).r >= Threshold;
    }
}