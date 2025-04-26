using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _agent.SetDestination(targetPosition);
    }

    public bool HasReachedDestination()
    {
        return !_agent.pathPending && _agent.remainingDistance < 0.5f;
    }
}