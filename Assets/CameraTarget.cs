using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    int shader_SphereOriginID;
    int shader_useClip;

    public Material[] dioramaMaterials;

    public BoatControl ship;
    public float followSpeed;

    public float aheadFactor = 5.0f;

    // Start is called before the first frame update
    void Awake()
    {
        shader_SphereOriginID = Shader.PropertyToID("_SphereOrigin");
        shader_useClip = Shader.PropertyToID("_UseClip");
    }

    private void Start()
    {
        Shader.SetGlobalFloat(shader_useClip, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector(shader_SphereOriginID, transform.position);

        transform.position = Vector3.Lerp(transform.position, ship.transform.position + ship.transform.forward * ship.m_windVelocity * aheadFactor, 1 - Mathf.Exp(-followSpeed * Time.deltaTime));
    }

    private void OnDisable()
    {
        Shader.SetGlobalFloat(shader_useClip, 0);
    }
}
