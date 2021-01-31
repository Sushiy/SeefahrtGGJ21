using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float Lifetime;

    public float Speed = 1.0F;

    public bool rotateYRandomly;

    // Start is called before the first frame update
    void Start()
    {
        if (rotateYRandomly)
        {
            transform.rotation = Quaternion.Euler(0.0F, Random.Range(0, 360.0F), 0.0F);
        }
    }

    public void StartCloud(float Lifetime)
    {
        this.Lifetime = Mathf.Max(this.Lifetime, Lifetime);
        Invoke("FadeoutCloud", Lifetime);
    }

    private void FadeoutCloud()
    {
        StartCoroutine(Shrink());
    }

    private IEnumerator Shrink()
    {
        float Alpha = 1.0F;
        Vector3 StartScale = transform.localScale;

        while (Alpha >= 0.0F)
        {
            transform.localScale = Vector3.Slerp(Vector3.zero, StartScale, Alpha);
            yield return null;
            Alpha -= Time.deltaTime * 2.0F;
            transform.localPosition += Vector3.up * (Time.deltaTime);
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (Speed * Time.deltaTime);
    }
}