using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 0.5f;

    private Vector3? _currentTarget;

    public void MoveTo(Vector3 targetPosition)
    {
        _currentTarget = targetPosition;
    }

    private void Update()
    {
        if (_currentTarget.HasValue)
        {
            Vector3 target = _currentTarget.Value;
            float distance = Vector3.Distance(transform.position, target);

            if (distance <= _stoppingDistance)
            {
                _currentTarget = null;
                Debug.Log($"{name} достиг цели: {target}");
                return;
            }

            Vector3 direction = (target - transform.position).normalized;
            transform.position += direction * _moveSpeed * Time.deltaTime;
        }
    }

    public bool HasReachedDestination(Vector3 targetPosition)
    {
        return Vector3.Distance(transform.position, targetPosition) <= _stoppingDistance;
    }

    public bool IsMoving => _currentTarget.HasValue;
}
