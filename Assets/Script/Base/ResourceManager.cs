using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private ScanAbility _scanAbility;

    private readonly List<Resource> _discoveredResources = new();

    public Resource GetNearestAvailableResource(Vector3 position)
    {
        if (_discoveredResources.Count == 0)
            return null;

        Resource nearest = null;
        float shortestSqrDistance = float.MaxValue;

        foreach (Resource resource in _discoveredResources)
        {
            float sqrDistance = (position - resource.transform.position).sqrMagnitude;

            if (sqrDistance < shortestSqrDistance)
            {
                shortestSqrDistance = sqrDistance;
                nearest = resource;
            }
        }

        if (nearest != null)
            _discoveredResources.Remove(nearest);

        return nearest;
    }

    private void OnEnable()
    {
        _scanAbility.ResourcesScanned += AddScannedResources;
    }

    private void OnDisable()
    {
        _scanAbility.ResourcesScanned -= AddScannedResources;
    }

    private void AddScannedResources(List<Resource> scanned)
    {
        foreach (Resource resource in scanned)
        {
            if (_discoveredResources.Contains(resource))
                continue;

            _discoveredResources.Add(resource);
        }
    }
}
