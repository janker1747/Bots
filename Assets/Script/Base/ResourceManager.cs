using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    private List<Transform> _resources = new();

    public IReadOnlyList<Transform> Resources => _resources;

    public void UpdateResources(List<Transform> newResources)
    {
        _resources = newResources;
        Debug.Log($"Обновлено: найдено {_resources.Count} ресурсов.");
    }

    public Transform GetNearestResource(Vector3 position)
    {
        Transform nearestResource = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform resource in _resources)
        {
            if (resource != null)
            {
                float distance = Vector3.Distance(position, resource.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestResource = resource;
                }
            }
        }

        return nearestResource;
    }
}