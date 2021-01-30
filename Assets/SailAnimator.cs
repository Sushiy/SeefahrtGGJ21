using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailAnimator : MonoBehaviour
{
    BoatControl controller;
    Animator anim;
    int sailId = Animator.StringToHash("Sail");
    int windId = Animator.StringToHash("Wind");

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<BoatControl>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat(sailId, controller.ForwardVel/controller.m_maxVerticalSpeed);
        anim.SetFloat(windId, 1.0f);
    }
}
