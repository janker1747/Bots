using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private Transform _spawnPlane;
    [SerializeField] private float _delay = 10;

    private void Start()
    {
        StartCoroutine(SpawnForDelay());
    }

    private Vector3 GetRandomPointOnPlane()
    {
        Vector3 center = _spawnPlane.position;
        Vector3 scale = _spawnPlane.localScale;

        float spawnHeightOffset = 1.5f;
        float halfWidth = 5f * scale.x;
        float halfHeight = 5f * scale.z;

        float randomX = Random.Range(center.x - halfWidth, center.x + halfWidth);
        float randomZ = Random.Range(center.z - halfHeight, center.z + halfHeight);

        return new Vector3(randomX, center.y + spawnHeightOffset, randomZ);
    }

    private void Spawn()
    {
        Resource resource = _pool.GetObject();
        Vector3 spawnPoint = GetRandomPointOnPlane();
        resource.transform.position = spawnPoint;
    }

    private IEnumerator SpawnForDelay()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delay);

        while (enabled)
        {
            Spawn();
            yield return waitForSeconds;
        }
    }
}