using UnityEngine;
using System.Collections.Generic;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private UnitPool _unitPool;
    [SerializeField] private Transform _spawnPoint;

    public List<Unit> SpawnUnits(int count)
    {
        List<Unit> units = new();

        for (int i = 0; i < count; i++)
        {
            Unit unit = _unitPool.GetObject();

            if (unit != null)
            {
                unit.transform.position = _spawnPoint.position; 
                units.Add(unit);
            }
        }

        return units;
    }
}