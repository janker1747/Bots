using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(ResourceCollector))]
public class Unit : MonoBehaviour
{
    private Mover _mover;
    private ResourceCollector _resourceCollector;

    private Resource _resourceTarget;
    private Transform _dropOffPoint;
    private WaitForSeconds _collectionDelay;

    public bool IsBusy { get; private set; }

    [SerializeField] private float _collectionTime = 1f;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _resourceCollector = GetComponent<ResourceCollector>();
        _collectionDelay = new WaitForSeconds(_collectionTime);
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

        _mover.MoveTo(resource.transform.position);
    }

    private void OnDestinationReached(Vector3 position)
    {
        if (!_resourceCollector.HasResource && _resourceTarget != null)
        {
            StartCoroutine(CollectResource());
        }
        else if (_resourceCollector.HasResource)
        {
            _resourceCollector.DropAt(position);
            IsBusy = false;
        }
    }

    private IEnumerator CollectResource()
    {
        yield return _collectionDelay;

        if (_resourceCollector.TryCollect(_resourceTarget))
        {
            _mover.MoveTo(_dropOffPoint.position);
            _resourceTarget = null;
        }
        else
        {
            IsBusy = false;
        }
    }
}
