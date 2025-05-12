using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    [SerializeField] private Transform _resourceHolder;
    private Transform _currentResource;

    public bool HasResource => _currentResource != null;

    public void PickUpResource(Resource resource)
    {
        if (resource == null || _currentResource != null)
            return;

        _currentResource = resource.transform;
        resource.transform.SetParent(_resourceHolder != null ? _resourceHolder : transform);
        resource.transform.localPosition = Vector3.zero;
        resource.transform.localRotation = Quaternion.identity;
    }

    public void DropOffResource()
    {
        if (_currentResource == null)
            return;

        _currentResource.position = _resourceHolder.position;

        _currentResource.SetParent(null);
        _currentResource = null;
    }
}