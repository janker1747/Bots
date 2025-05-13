using System;
using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 0.5f;

    private float _stoppingDistanceSqr;
    private Vector3? _currentTarget;
    private Coroutine _moveRoutine;

    public event Action<Vector3> DestinationReached;

    private void Awake()
    {
        _stoppingDistanceSqr = _stoppingDistance * _stoppingDistance;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _currentTarget = targetPosition;

        if (_moveRoutine != null)
            StopCoroutine(_moveRoutine);

        _moveRoutine = StartCoroutine(MoveToTargetRoutine());
    }

    private IEnumerator MoveToTargetRoutine()
    {
        while (_currentTarget.HasValue)
        {
            Vector3 target = _currentTarget.Value;
            Vector3 currentPosition = transform.position;
            float distanceSqr = (target - currentPosition).sqrMagnitude;

            if (distanceSqr <= _stoppingDistanceSqr)
            {
                _currentTarget = null;
                DestinationReached?.Invoke(target);
                yield break;
            }

            transform.position = Vector3.MoveTowards(currentPosition, target, _moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
