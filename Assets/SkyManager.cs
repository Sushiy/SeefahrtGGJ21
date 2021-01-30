using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyManager : MonoBehaviour
{
    public Transform sunParent;
    public float timeOfDay = 0.0f;
    public float timeSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay += timeSpeed * Time.deltaTime;
        if(timeOfDay > 24.0f)
        {
            timeOfDay -= 24.0f;
        }

        float sunRotation = Mathf.Lerp(0, 360, timeOfDay / 24.0f);
        sunParent.localRotation = Quaternion.Euler(-30.0f, 0.0f, sunRotation);
    }
}
