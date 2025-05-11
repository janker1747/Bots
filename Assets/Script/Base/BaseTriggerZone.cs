using UnityEngine;

public class BaseTriggerZone : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;

    public event System.Action<Resource> ResourceDelivered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            _scoreCounter.AddPoint();
            ResourceDelivered?.Invoke(resource);
        }
    }
}
