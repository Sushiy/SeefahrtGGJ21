using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForwardManager : MonoBehaviour
{
    public static bool isActive = false;
    private float fastForwardSpeed = 20.0f;
    private float timeOfDayFastForwardTarget = -1.0f;

    public SkyManager skyManager;

    public void SetFastForwardSpeed(float speed)
    {
        fastForwardSpeed = speed;
    }

    public void SetFastForwardUntil(float targetTime)
    {
        timeOfDayFastForwardTarget = targetTime;
        ActivateFF();
    }

    public void ActivateFF()
    {
        isActive = true;
        Time.timeScale = fastForwardSpeed;
    }

    private void DeactivateFF()
    {
        isActive = false;
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (isActive && timeOfDayFastForwardTarget > 0.0f)
        {
            if (Mathf.Abs(timeOfDayFastForwardTarget - skyManager.timeOfDay) < 0.1f)
            {
                skyManager.SetTime(timeOfDayFastForwardTarget);
                timeOfDayFastForwardTarget = -1.0f;
                DeactivateFF();
            }
        }
    }
}
