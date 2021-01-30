using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomLocalRotator : MonoBehaviour
{
    public float Speed = 1.0F;

    public float SteeringChangeEverySeconds = 1;
    
    private bool _bCancelChangeSteering = false;
    
    
    private Quaternion _rotChange;

    private void Start()
    {
        StartSteering();
    }

    public void StartSteering()
    {
        _bCancelChangeSteering = false;
        StartCoroutine(ChangeSteering());
    }

    public void StopSteering()
    {
        _bCancelChangeSteering = true;
    }

    private IEnumerator ChangeSteering()
    {
        while (!_bCancelChangeSteering)
        {
            _rotChange = Quaternion.Euler(Random.onUnitSphere);
            yield return new WaitForSeconds(SteeringChangeEverySeconds);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.localRotation, transform.localRotation * _rotChange,
            Time.deltaTime * Speed);
    }
}