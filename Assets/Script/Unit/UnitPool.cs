using UnityEngine;
using System.Collections.Generic;

public class UnitPool : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;   
    [SerializeField] private int _initialPoolSize = 3;  

    private Queue<Unit> _pool = new();

    private void Awake()
    {
        InitializePool(_initialPoolSize);
    }

    public void AddToPool(Unit unit)
    {
        unit.gameObject.SetActive(false); 
        _pool.Enqueue(unit);
    }

    public Unit GetFromPool(Transform spawnPosition)
    {
        if (_pool.Count > 0)
        {
            Unit unit = _pool.Dequeue();
            unit.transform.position = spawnPosition.position; 
            unit.gameObject.SetActive(true); 
            return unit;
        }
        else
        {
            Unit unit = Instantiate(_unitPrefab);
            return unit;
        }
    }

    private void InitializePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            Unit unit = Instantiate(_unitPrefab);
            AddToPool(unit);
        }
    }
}