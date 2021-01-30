using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DaytimeComponent : MonoBehaviour
{
    public bool ShowDuringDayTime;
    public bool ShowDuringNighttime;
    public bool ShowDuringTransition;

    public float ThresholdForDayNight = 0.1F;

    public bool LastShouldShow { get; protected set;  }
    
    public UnityEvent<bool> ShouldShowChanged { get; protected set; }

    private void Awake()
    {
        ShouldShowChanged = new UnityEvent<bool>();
    }

    // Start is called before the first frame update
    void Start()
    {                
        var _renderer = GetComponent<Renderer>();

        SkyManager skyManager = FindObjectOfType<SkyManager>();
        if (skyManager)
        {
            bool bOnceInitial = true;
            skyManager.DayNightEvent.AddListener(light =>
            {
                bool shouldShow = ShowDuringNighttime && light <= ThresholdForDayNight
                                  || ShowDuringDayTime && light >= (1.0F - ThresholdForDayNight)
                                  || ShowDuringTransition && light > ThresholdForDayNight &&
                                  light < ThresholdForDayNight;
                
                ShouldShow(shouldShow, bOnceInitial);
                bOnceInitial = false;
                LastShouldShow = shouldShow;
            });
        }
    }

    protected virtual void ShouldShow(bool newShouldShow, bool bForceBroadcast = false)
    {
        if (newShouldShow != LastShouldShow || bForceBroadcast)
        {
           ShouldShowChanged.Invoke(newShouldShow);
        }
    }
}