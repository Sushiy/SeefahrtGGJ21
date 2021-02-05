using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullRotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f;

    private void Update()
    {
        transform.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
    }
}
