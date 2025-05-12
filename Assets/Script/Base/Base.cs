using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float _scanDelay = 0.5f;
    [SerializeField] private int _initialUnitCount = 3;

    [Header("Ссылки")]
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private Transform _dropOffPoint;

    private List<Unit> _units = new List<Unit>();

    private void Awake()
    {
        SpawnInitialUnits();
        StartCoroutine(ResourceAssignmentLoop());
    }

    private void SpawnInitialUnits()
    {
        _units = _unitSpawner.SpawnUnits(_initialUnitCount);
    }

    private IEnumerator ResourceAssignmentLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(_scanDelay);

        while (enabled)
        {
            foreach (Unit unit in _units)
            {
                if (unit == null || unit.IsBusy)
                    continue;

                Resource nearestResource = _resourceManager.GetNearestAvailableResource(unit.transform.position, unit);

                if (nearestResource != null)
                {
                    unit.AssignTask(nearestResource, _dropOffPoint);
                }
            }

            yield return wait;
        }
    }

}