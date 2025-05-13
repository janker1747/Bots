using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
    [SerializeField] private Transform _resourceHolder;
    private Transform _currentResource;

    public bool HasResource => _currentResource != null;

    public bool TryCollect(Resource resource)
    {
        if (resource == null || _currentResource != null)
            return false;

        _currentResource = resource.transform;
        _currentResource.SetParent(_resourceHolder != null ? _resourceHolder : transform);
        _currentResource.localPosition = Vector3.zero;
        _currentResource.localRotation = Quaternion.identity;

        return true;
    }

    public void DropAt(Vector3 dropPosition)
    {
        if (_currentResource == null)
            return;

        _currentResource.position = dropPosition;
        _currentResource.SetParent(null);
        _currentResource = null;
    }
}
