using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnShow : MonoBehaviour
{
    MeshRenderer m;
    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<MeshRenderer>();
        GetComponent<DaytimeComponent>().ShouldShowChanged.AddListener(Toggle); 
    }

    private void Toggle(bool active)
    {
        m.enabled = active;
    }
}
