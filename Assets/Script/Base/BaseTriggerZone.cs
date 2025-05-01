using System;
using UnityEngine;

public class BaseTriggerZone : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            _scoreCounter.AddPoint();
            resource.ReturnToPool();
        }
    }
}
