using System;
using UnityEngine;

public class BaseTriggerZone : MonoBehaviour
{
    public event Action TriggerEntre;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            TriggerEntre?.Invoke();
            resource.ReturnToPool();
        }
    }
}
