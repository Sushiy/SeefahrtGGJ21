using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailAnimator : MonoBehaviour
{
    BoatControl controller;
    Animator anim;
    int sailId = Animator.StringToHash("Sail");
    int windId = Animator.StringToHash("Wind");

    public Transform sailParent;
    public Transform shipParent;
    Quaternion offset = Quaternion.Euler(0, 180, 0);

    public Vector3 localEuler = Vector3.zero;
    public float sailRotationSpeed = 1.0f;

    Vector3 localWindDirection;

    Vector3 leftMaxVector;
    float leftMaxDot;
    Vector3 rightMaxVector;
    float rightMaxDot;
    Vector3 lerpTarget;

    ISailLevelProvider sailProvider;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<BoatControl>();
        anim = GetComponent<Animator>();
        sailProvider = GetComponentInParent<ISailLevelProvider>();
    }

    // Update is called once per frame
    void Update()
    {
        localWindDirection = shipParent.InverseTransformDirection(WindSource.WindDirection);
        leftMaxVector = shipParent.InverseTransformDirection(Quaternion.Euler(0, -70, 0) * shipParent.forward);
        rightMaxVector = shipParent.InverseTransformDirection(Quaternion.Euler(0, 70, 0) * shipParent.forward);

        rightMaxDot = Vector3.Dot(rightMaxVector, Vector3.forward);

        lerpTarget = localWindDirection;

        if(Mathf.Abs(Vector3.Angle(lerpTarget, Vector3.forward)) > 70.0f)
        {
            if (Vector3.Dot(lerpTarget, Vector3.right) > 0)
            {
                //went over rightMax
                lerpTarget = rightMaxVector;
            }
            else
            {
                //went over leftMax
                lerpTarget = leftMaxVector;
            }
        }

        Vector3 t = shipParent.TransformDirection(-lerpTarget);
        sailParent.rotation = Quaternion.Slerp(sailParent.rotation, Quaternion.LookRotation(t, Vector3.up), 1 - Mathf.Exp(-1.0f * Time.deltaTime));

        Debug.DrawRay(sailParent.position, shipParent.TransformDirection(lerpTarget) * 2.0f, Color.red);
        Debug.DrawRay(sailParent.position, WindSource.WindDirection, Color.green);
        Debug.DrawRay(sailParent.position, shipParent.TransformDirection(leftMaxVector), Color.yellow);
        Debug.DrawRay(sailParent.position, shipParent.TransformDirection(rightMaxVector), Color.yellow);
        Debug.DrawRay(sailParent.position, -sailParent.forward, Color.blue);

        //AnimatorStuff
        anim.SetFloat(sailId, sailProvider.GetSailLevel());
        anim.SetFloat(windId, sailProvider.GetWindFactor());
    }
}
