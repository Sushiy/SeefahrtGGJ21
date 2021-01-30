using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloatyBoaty : MonoBehaviour
{
    public CurveAsset CurveAsset;
    public AnimationCurve FloatCurve;

    public float Scale = 1.0F;

    private float PersonalShift = 0.0F;
    private Vector3 basePosition;

    private void Start()
    {
        if (CurveAsset)
        {
            FloatCurve = CurveAsset.Curve;
        }

        basePosition = transform.localPosition;
        
        if (FloatCurve.length > 0)
        {
            PersonalShift = Random.Range(0.0F, FloatCurve[FloatCurve.length - 1].time * 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = basePosition + Vector3.up * (FloatCurve.Evaluate(Time.time + PersonalShift) * Scale);
        PersonalShift += Random.Range(0.9F, 1.1F) * Time.deltaTime * Scale;
    }
}