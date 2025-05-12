using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [Header("трансформы")]
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private Transform _spawnPlane;
    [SerializeField] private Transform _baseTransform;

    [Header("тайминги")]
    [SerializeField] private float _delay = 10f;

    [Header("дистанции")]
    [SerializeField] private float _minDistanceFromBase = 3f;
    [SerializeField] private float _minDistanceBetweenResources = 2f;

    [Header("ссылки")]
    [SerializeField] private BaseTriggerZone _baseTriggerZone;

    private readonly List<Vector3> _usedPositions = new();
    private const int MaxAttempts = 10;

    private void Start()
    {
        StartCoroutine(SpawnForDelay());
    }

    private void OnEnable()
    {
        _baseTriggerZone.ResourceDelivered += OnResourceDelivered;
    }

    private void OnDisable()
    {
        _baseTriggerZone.ResourceDelivered -= OnResourceDelivered;
    }

    private IEnumerator SpawnForDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        Resource resource = _pool.GetObject();
        Vector3 spawnPoint = GetRandomPointOnPlane();
        resource.transform.position = spawnPoint;
    }

    private void OnResourceDelivered(Resource resource)
    {
        _usedPositions.Remove(resource.transform.position);
        _pool.ReturnObject(resource);
    }

    private Vector3 GetRandomPointOnPlane()
    {
        Vector3 center = _spawnPlane.position;
        Vector3 scale = _spawnPlane.localScale;
        float halfWidth = 5f * scale.x;
        float halfHeight = 5f * scale.z;
        float heightOffset = 1.5f;

        for (int i = 0; i < MaxAttempts; i++)
        {
            float x = Random.Range(center.x - halfWidth, center.x + halfWidth);
            float z = Random.Range(center.z - halfHeight, center.z + halfHeight);
            Vector3 candidate = new Vector3(x, center.y + heightOffset, z);

            bool tooCloseToBase = (candidate - _baseTransform.position).sqrMagnitude < _minDistanceFromBase * _minDistanceFromBase;
            bool tooCloseToOthers = _usedPositions.Exists(pos => (candidate - pos).sqrMagnitude < _minDistanceBetweenResources * _minDistanceBetweenResources);

            if (tooCloseToBase == false && tooCloseToOthers == false)
            {
                _usedPositions.Add(candidate);
                return candidate;
            }
        }

        return center + Vector3.up * heightOffset;
    }
}
