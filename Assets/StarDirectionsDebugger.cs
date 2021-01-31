using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDirectionsDebugger : MonoBehaviour
{
    public Transform[] children;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        for (int i = 0; i < children.Length; i++)
        {
            Debug.DrawLine(transform.position, children[i].position);
        }
    }
}
