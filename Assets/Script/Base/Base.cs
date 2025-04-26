using UnityEngine;
using System.Collections.Generic;

public class Base : MonoBehaviour
{
    [SerializeField] private UnitSpawner unitSpawner; 
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private Transform dropOffPoint;
    [SerializeField] private Transform spawnPoint; 
    [SerializeField] private int unitCount = 3;

    private List<Unit> _units = new();

    private void Awake()
    {
        _units = unitSpawner.SpawnUnits(unitCount, spawnPoint);
        AssignInitialTasks();
    }

    private void AssignInitialTasks()
    {
        foreach (Unit unit in _units)
        {
            AssignTaskToUnit(unit);
        }
    }

    private void AssignTaskToUnit(Unit unit)
    {
        Transform nearestResource = resourceManager.GetNearestResource(unit.transform.position);

        if (nearestResource != null)
        {
            unit.SetResourceTarget(nearestResource);
            unit.SetDropOffPoint(dropOffPoint);
            unit.MoveTo(nearestResource.position);
            Debug.Log($"Юниту назначена задача: двигаться к ресурсу {nearestResource.name}");
        }
       
    }
}