using System.Collections;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public float SpawnDistance;
    public float SpawnLocalRadius;

    public GameObject[] Clouds;

    public float SpawnIntervalMin = 5.0F;
    public float SpawnIntervalMax = 6.0F;
    [SerializeField] private float LifetimeFactor;

    private Vector3 SpawnDirection => -WindSource.WindDirection;

    GameObject SelectRandomPrefab()
    {
        return Clouds[Random.Range(0, Clouds.Length - 1)];
    }

    private void Start()
    {
        StartCoroutine(StartSpawnInterval());
    }

    private IEnumerator StartSpawnInterval()
    {
        if (Clouds.Length == 0) yield break;
        while (true)
        {
            SpawnNewCloud();
            yield return new WaitForSeconds(Random.Range(SpawnIntervalMin, SpawnIntervalMax));
        }
    }

    private void SpawnNewCloud()
    {
        var lp = GetPossibleSpawnPosition(out var offset);
        // transform.localPosition = lp;

        Vector3 dir = new Vector3(1, 0, 1);
        dir.Scale(Vector3.zero - lp);
        transform.forward = WindSource.WindDirection;

        var go = Instantiate(SelectRandomPrefab(), transform.position + offset, transform.rotation);

        var cloud = go.GetComponent<Cloud>();
        cloud.Speed = cloud.Speed;
        cloud.StartCloud(lp.magnitude / cloud.Speed * LifetimeFactor);
    }

    private Vector3 GetPossibleSpawnPosition(out Vector3 offset)
    {
        var lp = transform.localPosition;
        var y = lp.y;
        lp = Random.onUnitSphere * SpawnDistance;
        // lp = SpawnDirection * SpawnDistance;
        lp.y = y;

        offset = Random.insideUnitSphere * SpawnLocalRadius;
        return lp;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        float Angle = 0.0F;
        int Segments = 16;
        float R = SpawnDistance;
        float Steps = 360.0F / Segments;

        float S = 0.0F;
        var p = R * (Quaternion.Euler(0, S, 0) * Vector3.forward);
        var e = p;
        for (int i = 0; i < Segments; ++i)
        {
            S = ((i + 1) * Steps);
            e = R * ( Quaternion.Euler(0, S, 0) * Vector3.forward );
            Gizmos.DrawLine(transform.position + p, transform.position + e);
            p = e;
        }
    }
}