using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISailLevelProvider
{
    float GetSailLevel();

    float GetWindFactor();
}
