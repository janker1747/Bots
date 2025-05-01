using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    [SerializeField] private Transform _resourceHolder;
    private Transform _currentResource;

    public bool HasResource => _currentResource != null;

    public void PickUpResource(Transform resource)
    {
        if (resource == null || _currentResource != null) return;

        _currentResource = resource;
        resource.SetParent(_resourceHolder != null ? _resourceHolder : transform);
        resource.localPosition = Vector3.zero;
        resource.localRotation = Quaternion.identity;

        MeshRenderer mesh = resource.GetComponent<MeshRenderer>();

        if (mesh != null)
        {
            mesh.enabled = true;
        }

        Debug.Log($"{name} подобрал ресурс {resource.name}");
    }

    public void DropOffResource()
    {
        if (_currentResource == null) return;

        _currentResource.position = _resourceHolder.position;

        MeshRenderer mesh = _currentResource.GetComponent<MeshRenderer>();

        if (mesh != null)
        {
            mesh.enabled = true;
        }

        _currentResource.SetParent(null);
        _currentResource = null;
    }
}
