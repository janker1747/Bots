using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(ResourceHandler))]
public class Unit : MonoBehaviour
{
    private Mover _mover;
    private ResourceHandler _resourceHandler;

    private Resource _resourceTarget;
    private Transform _dropOffPoint;
    private bool _hasResource = false;
    private float _time = 1f;
    private WaitForSeconds _collectedTime;

    public bool IsBusy { get; private set; }

    private const float _reachedThresholdSqr = 0.01f;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _resourceHandler = GetComponent<ResourceHandler>();
        _collectedTime = new WaitForSeconds(_time);
    }

    private void OnEnable()
    {
        _mover.DestinationReached += OnDestinationReached;
    }

    private void OnDisable()
    {
        _mover.DestinationReached -= OnDestinationReached;
    }

    public void AssignTask(Resource resource, Transform dropOff)
    {
        if (IsBusy || resource == null || dropOff == null)
            return;

        _resourceTarget = resource;
        _dropOffPoint = dropOff;
        IsBusy = true;
        _hasResource = false;

        _mover.MoveTo(resource.transform.position);
    }

    private void OnDestinationReached(Vector3 reachedPosition)
    {
        if (_resourceTarget != null && !_hasResource &&
            (reachedPosition - _resourceTarget.transform.position).sqrMagnitude < _reachedThresholdSqr)
        {
            StartCoroutine(CollectResource());
        }
        else if (_hasResource &&
            (_dropOffPoint.position - reachedPosition).sqrMagnitude < _reachedThresholdSqr)
        {
            DropOffResource();
        }
    }

    private IEnumerator CollectResource()
    {
        yield return _collectedTime;

        if (_resourceTarget != null)
        {
            _resourceHandler.PickUpResource(_resourceTarget);
            _hasResource = true;
            _mover.MoveTo(_dropOffPoint.position);
            _resourceTarget = null;
        }
    }

    private void DropOffResource()
    {
        _resourceHandler.DropOffResource();
        _hasResource = false;
        IsBusy = false;
    }
}
