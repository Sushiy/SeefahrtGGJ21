using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Vector3 eulerOffset;
    Quaternion eulerOffsetR;

    public Transform lookAtTarget;
    private Vector3 lookAtVector;
    // Start is called before the first frame update
    void Start()
    {
        if (lookAtTarget == null)
            lookAtTarget = Camera.main.transform;
        eulerOffsetR = Quaternion.Euler(eulerOffset);
    }

    // Update is called once per frame
    void Update()
    {
        lookAtVector = lookAtTarget.position - transform.position;
        transform.rotation = Quaternion.LookRotation(lookAtVector, Vector2.up) * eulerOffsetR;
        transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0);
    }
}
