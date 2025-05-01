using UnityEngine;
using System.Collections.Generic;

public class UnitPool : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private int _initialPoolSize = 3;

    private Queue<Unit> _pool = new();

    private void Awake()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            Unit unit = Instantiate(_unitPrefab);
            unit.gameObject.SetActive(false);
            _pool.Enqueue(unit);
        }
    }

    public Unit GetObject()
    {
        Unit unit;

        if (_pool.Count > 0)
        {
            unit = _pool.Dequeue();
        }
        else
        {
            unit = Instantiate(_unitPrefab);
        }

        unit.gameObject.SetActive(true);
        return unit;
    }

    public void ReturObject(Unit unit)
    {
        _pool.Enqueue(unit);
    }
 }