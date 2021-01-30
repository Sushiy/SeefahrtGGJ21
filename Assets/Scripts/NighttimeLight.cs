using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NighttimeLight : MonoBehaviour
{
    public AnimationCurve ActivationCurve;
    public bool _isNighttime;

    private float Alpha = 0.0F;
    private Light light;

    
    
    [SerializeField] private float MaxFlicker = 1.0F;
    [SerializeField] private float MinFlicker;


    [SerializeField] private float FlickerValue;
    [SerializeField] private float FlickerSpeed = 1.0F;
    private float FlickerTarget;
    [SerializeField] private float FlickerTiming = 0.5F;

    private SkyManager _skyManager;
    
    private void Start()
    {
        light = GetComponent<Light>();

        StartCoroutine(StartFlickering());
        FlickerValue = FlickerTarget;

        _skyManager = FindObjectOfType<SkyManager>();

        _skyManager.DayNightEvent.AddListener((float V) =>
        {
            _isNighttime = V < 0.5F;
        });
    }

    private IEnumerator StartFlickering()
    {
        while (true)
        {
            FlickerTarget = Random.Range(MinFlicker, MaxFlicker);
            yield return new WaitForSeconds(FlickerTiming);
        }
    }

    private void Update()
    {
        if (_isNighttime)
        {
            Alpha += Time.deltaTime;
        }
        else
        {
            Alpha -= Time.deltaTime;
        }

        Alpha = Mathf.Clamp(Alpha, ActivationCurve[0].time, ActivationCurve[ActivationCurve.length - 1].time);
        light.intensity = FlickerValue * ActivationCurve.Evaluate(Alpha);

        FlickerValue = Mathf.Lerp(FlickerValue, FlickerTarget, Time.deltaTime * FlickerSpeed);
    }
}