using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float _scanDelay = 0.5f;
    [SerializeField] private int _initialUnitCount = 3;

    [Header("Ссылки")]
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private Transform _dropOffPoint;

    private List<Unit> _units = new();

    private void Awake()
    {
        SpawnUnits();
        StartCoroutine(AssignResourcesLoop());
    }

    private void SpawnUnits()
    {
        _units = _unitSpawner.SpawnUnits(_initialUnitCount);
    }

    private IEnumerator AssignResourcesLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(_scanDelay);

        while (enabled)
        {
            foreach (Unit unit in _units)
            {
                if (unit == null || unit.IsBusy)
                    continue;

                Resource resource = _resourceManager.GetNearestAvailableResource(unit.transform.position);

                if (resource != null)
                    unit.AssignTask(resource, _dropOffPoint);
            }

            yield return wait;
        }
    }
}
