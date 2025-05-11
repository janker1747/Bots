using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private ScanAbility _scanAbility;

    private readonly List<Transform> _discoveredResources = new();
    private readonly Dictionary<Transform, Unit> _claimedResources = new();

    public int AvailableResourcesCount => _discoveredResources.Count;

    private void OnEnable()
    {
        _scanAbility.ResourcesScanned += HandleScannedResources;
    }

    private void OnDisable()
    {
        _scanAbility.ResourcesScanned -= HandleScannedResources;
    }

    private void HandleScannedResources(List<Transform> scanned)
    {
        foreach (Transform resource in scanned)
        {
            if (_claimedResources.ContainsKey(resource)) continue;
            if (_discoveredResources.Contains(resource)) continue;

            _discoveredResources.Add(resource);
        }
    }

    public Transform GetNearestAvailableResource(Vector3 position, Unit requestingUnit)
    {
        if (_discoveredResources.Count == 0)
            return null;

        Transform nearestResource = null;
        float closestSqrDistance = float.MaxValue;

        foreach (Transform resource in _discoveredResources)
        {
            if (_claimedResources.ContainsKey(resource) && _claimedResources[resource] != requestingUnit)
                continue;

            float sqrDistance = (position - resource.position).sqrMagnitude;

            if (sqrDistance < closestSqrDistance)
            {
                closestSqrDistance = sqrDistance;
                nearestResource = resource;
            }
        }

        if (nearestResource != null)
        {
            _claimedResources[nearestResource] = requestingUnit;
            _discoveredResources.Remove(nearestResource);
        }

        return nearestResource;
    }

    public void ReleaseResource(Transform resource)
    {
        if (resource == null)
        {
            return;
        }

        if (_claimedResources.ContainsKey(resource))
        {
            _claimedResources.Remove(resource);
        }
    }
}
