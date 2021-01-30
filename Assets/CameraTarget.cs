using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraTarget : MonoBehaviour
{
    string shader_SphereOrigin = "_SphereOrigin";
    int shader_SphereOriginID;

    public Material[] dioramaMaterials;

    public BoatControl ship;
    public float followSpeed;

    public float aheadFactor = 5.0f;

    // Start is called before the first frame update
    void Awake()
    {
        shader_SphereOriginID = Shader.PropertyToID(shader_SphereOrigin);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Shader.SetGlobalVector(shader_SphereOriginID, transform.position);

        transform.position = Vector3.Lerp(transform.position, ship.transform.position + ship.transform.forward * ship.ForwardVel * aheadFactor, 1 - Mathf.Exp(-followSpeed * Time.deltaTime));

    }
}
