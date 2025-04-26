using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    private Transform _targetResource;
    private Transform _dropOffPoint;
    private bool _hasResource = false;

    public bool HasResource()
    {
        return _hasResource;
    }

    public void SetTargetResource(Transform resource)
    {
        _targetResource = resource;
    }

    public void SetDropOffPoint(Transform dropOff)
    {
        _dropOffPoint = dropOff;
    }

    public void PickUpResource()
    {
        if (_targetResource != null)
        {
            Debug.Log("Ресурс поднят!");
            _hasResource = true;
            _targetResource.gameObject.SetActive(false); 
        }
    }

    public void DropOffResource()
    {
        if (_hasResource)
        {
            Debug.Log("Ресурс доставлен!");
            _hasResource = false;
        }
    }
}