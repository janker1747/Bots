using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private ScanAbility _scanAbility;

    private List<Transform> _resources = new List<Transform>();
    private Dictionary<Transform, Unit> _claimedResources = new Dictionary<Transform, Unit>();

    public int AvailableResourcesCount => _resources.Count;

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
        foreach (Transform position in scanned)
        {
            _resources.Add(position);
        }
    }

    public Transform GetNearestAvailableResource(Vector3 position, Unit requestingUnit)
    {
        if (_resources.Count == 0)
            return null;

        Transform nearestResource = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform resource in _resources)
        {
            if (_claimedResources.ContainsKey(resource) && _claimedResources[resource] != requestingUnit)
            {
                continue; 
            }

            float distance = Vector3.Distance(position, resource.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestResource = resource;
            }
        }

        if (nearestResource != null)
        {
            _claimedResources[nearestResource] = requestingUnit;
            _resources.Remove(nearestResource); 
            Debug.Log($"ResourceManager: Ресурс {nearestResource.name} зарезервирован для {requestingUnit.name}");
        }

        return nearestResource;
    }

    public void ReleaseResource(Transform resource)
    {
        if (resource == null)
        {
            Debug.LogWarning("ResourceManager: Попытка освободить null ресурс.");
            return;
        }

        if (_claimedResources.ContainsKey(resource))
        {
            _claimedResources.Remove(resource);
            _resources.Add(resource);
            Debug.Log($"ResourceManager: Ресурс {resource.name} освобожден.");
        }
        else
        {
            Debug.Log($"ResourceManager: Ресурс {resource.name} не был забран.");
        }
    }
}
