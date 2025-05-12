using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 0.5f;

    private float _stoppingDistanceSqr;
    private Vector3? _currentTarget;

    public bool IsMoving => _currentTarget.HasValue;
    public event Action<Vector3> DestinationReached;

    private void Awake()
    {
        _stoppingDistanceSqr = _stoppingDistance * _stoppingDistance;
    }

    private void Update()
    {
        if (!_currentTarget.HasValue)
            return;

        Vector3 target = _currentTarget.Value;
        Vector3 direction = target - transform.position;
        float distanceSqr = direction.sqrMagnitude;

        if (distanceSqr <= _stoppingDistanceSqr)
        {
            _currentTarget = null;
            DestinationReached?.Invoke(target);
            return;
        }

        direction.Normalize();
        transform.position += direction * _moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _currentTarget = targetPosition;
    }

    public bool HasReachedDestination(Vector3 targetPosition)
    {
        float distanceSqr = (transform.position - targetPosition).sqrMagnitude;
        return distanceSqr <= _stoppingDistanceSqr;
    }
}
