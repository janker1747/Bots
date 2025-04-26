using UnityEngine;
using System.Collections.Generic;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private UnitPool _unitPool;
    [SerializeField] private Transform _spawnPoint;

    public List<Unit> SpawnUnits(int count, Transform spawnPoint)
    {
        List<Unit> units = new();

        for (int i = 0; i < count; i++)
        {
            Unit unit = _unitPool.GetFromPool(_spawnPoint);
            if (unit != null)
            {
                unit.transform.position = spawnPoint.position; 
                units.Add(unit);
            }
        }

        return units; 
    }
}