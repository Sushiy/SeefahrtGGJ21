﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyManager : MonoBehaviour
{
    public Transform sunParent;
    public float timeOfDay = 0.0f;
    public float dayLengthMinutes = 3.0f;
    private float timeSpeed;

    public Gradient skyGradient;

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
        if(timeOfDay > 24.0f)
        {
            timeOfDay -= 24.0f;
        }
        float timeOfDayNorm = timeOfDay / 24.0f;
        Shader.SetGlobalFloat(shader_TimeValue, timeOfDayNorm);
        RenderSettings.ambientLight = skyGradient.Evaluate(timeOfDayNorm);

        float sunRotation = Mathf.Lerp(0, 360, timeOfDayNorm);
        sunParent.localRotation = Quaternion.Euler(-30.0f, 0.0f, sunRotation);
    }
}
