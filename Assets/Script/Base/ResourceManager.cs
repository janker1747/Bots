using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private ScanAbility _scanAbility;

    private readonly List<Resource> _discoveredResources = new();
    private readonly Dictionary<Resource, Unit> _claimedResources = new();

    public int AvailableResourcesCount => _discoveredResources.Count;

    private void OnEnable()
    {
        _scanAbility.ResourcesScanned += HandleScannedResources;
    }

    private void OnDisable()
    {
        _scanAbility.ResourcesScanned -= HandleScannedResources;
    }

    private void HandleScannedResources(List<Resource> scanned)
    {
        foreach (Resource resource in scanned)
        {
            if (_claimedResources.ContainsKey(resource))
            {
                continue;
            }

            if (_discoveredResources.Contains(resource))
            {
                continue;
            }

            _discoveredResources.Add(resource);
        }
    }

    public Resource GetNearestAvailableResource(Vector3 position, Unit requestingUnit)
    {
        if (_discoveredResources.Count == 0)
            return null;

        Resource nearestResource = null;
        float closestSqrDistance = float.MaxValue;

        foreach (Resource resource in _discoveredResources)
        {
            if (_claimedResources.ContainsKey(resource) && _claimedResources[resource] != requestingUnit)
            {
                continue;
            }

            float sqrDistance = (position - resource.transform.position).sqrMagnitude;

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

    public void ReleaseResource(Resource resource)
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