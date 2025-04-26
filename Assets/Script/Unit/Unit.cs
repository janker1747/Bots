using UnityEngine;

public class Unit : MonoBehaviour
{
    private Mover _mover;
    private ResourceHandler _resourceHandler;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _resourceHandler = GetComponent<ResourceHandler>();
    }

    public void MoveTo(Vector3 target)
    {
        _mover.MoveTo(target);
    }

    public bool HasReachedDestination()
    {
        return _mover.HasReachedDestination();
    }

    public bool IsCarryingResource()
    {
        return _resourceHandler.HasResource();
    }

    public void SetResourceTarget(Transform resource)
    {
        _resourceHandler.SetTargetResource(resource);
    }

    public void SetDropOffPoint(Transform dropOff)
    {
        _resourceHandler.SetDropOffPoint(dropOff);
    }
}