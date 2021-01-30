using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnShow : MonoBehaviour
{
    public CurveAsset ScaleCurve;

    private DaytimeComponent _daytimeComponent;
    private float _alpha;
    private Vector3 _startingScale;

    private float ScalingDir = -1.0F;
    
    private void Start()
    {
        _startingScale = transform.localScale;
        StartCoroutine(Scaling());
        _daytimeComponent = GetComponent<DaytimeComponent>();
        if (_daytimeComponent && ScaleCurve)
        {
            _daytimeComponent.ShouldShowChanged.AddListener(shouldShow =>
            {
                if (shouldShow)
                {
                    ScalingDir = 1.0F;

                }
                else
                {
                    ScalingDir = -1.0F;
                }
            });
        }
    }

    protected IEnumerator Scaling()
    {
        float max = ScaleCurve.MaxTime;
        _alpha = Mathf.Clamp(_alpha, 0.0F, ScaleCurve.MaxTime);
        while (true)
        {
            transform.localScale = _startingScale * ScaleCurve.Curve.Evaluate(_alpha);

            yield return null;
            _alpha += Time.deltaTime * ScalingDir;
            _alpha = Mathf.Clamp(_alpha, 0.0F, ScaleCurve.MaxTime);
        }
    }
}