using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnInWindDirection : MonoBehaviour
{
    public Vector3 angleOffset;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(WindSource.WindDirection, transform.parent.up) * Quaternion.Euler(angleOffset);
    }
}
