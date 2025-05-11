using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Resource _prefab;
    [SerializeField] private int _maxSize = 10;

    private readonly Queue<Resource> _pool = new();

    private void Awake()
    {
        for (int i = 0; i < _maxSize; i++)
        {
            Resource resource = CreateResource();
            resource.gameObject.SetActive(false);
            _pool.Enqueue(resource);
        }
    }

    public Resource GetObject()
    {
        Resource resource;

        if (_pool.Count > 0)
        {
            resource = _pool.Dequeue();
        }
        else
        {
            resource = CreateResource();
        }

        resource.gameObject.SetActive(true);
        return resource;
    }

    public void ReturnObject(Resource resource)
    {
        resource.gameObject.SetActive(false);
        _pool.Enqueue(resource);
    }

    private Resource CreateResource()
    {
        return Instantiate(_prefab, transform);
    }
}
