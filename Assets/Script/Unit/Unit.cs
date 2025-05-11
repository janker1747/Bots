using System.Collections;
using System.Xml.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Mover _mover;
    private ResourceHandler _resourceHandler;

    private Resource _resourceTarget;
    private Transform _dropOffPoint;
    private bool _isBusy = false;
    private bool _hasResource = false;

    public bool IsBusy { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _resourceHandler = GetComponent<ResourceHandler>();
    }

    private void Update()
    {
        if (_isBusy == false) return;

        if (_hasResource)
        {
            MoveToDropOff();
        }
        else if (_resourceTarget != null)
        {
            MoveToResource();
        }
    }

    public void AssignTask(Resource resource, Transform dropOff)
    {
        if (_isBusy || resource == null || dropOff == null)
            return;

        _resourceTarget = resource;
        _dropOffPoint = dropOff;
        _isBusy = true;
        _hasResource = false;

        _mover.MoveTo(resource.transform.position);
    }

    private void MoveToResource()
    {
        if (!_mover.IsMoving)
            _mover.MoveTo(_resourceTarget.transform.position);

        if (_mover.HasReachedDestination(_resourceTarget.transform.position))
        {
            StartCoroutine(CollectResource());
        }
    }

    private IEnumerator CollectResource()
    {
        yield return new WaitForSeconds(1f);

        if (_resourceTarget != null)
        {
            _resourceHandler.PickUpResource(_resourceTarget);
            _hasResource = true;
            _resourceTarget = null;
        }
    }

    private void MoveToDropOff()
    {
        if (!_mover.IsMoving)
            _mover.MoveTo(_dropOffPoint.position);

        if (_mover.HasReachedDestination(_dropOffPoint.position))
        {
            DropOffResource();
        }
    }

    private void DropOffResource()
    {
        _resourceHandler.DropOffResource();
        _hasResource = false;
        _isBusy = false;

        if (_resourceTarget != null)
        {
            ResourceManager resourceManager = FindObjectOfType<ResourceManager>();
            resourceManager.ReleaseResource(_resourceTarget);
        }
    }
}