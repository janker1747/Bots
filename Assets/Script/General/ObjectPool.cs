using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _initialSize = 5;

    private readonly Queue<T> _pool = new();

    protected virtual void Awake()
    {
        for (int i = 0; i < _initialSize; i++)
        {
            T item = CreateItem();
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }
    }

    public T GetObject()
    {
        T item;

        if (_pool.Count > 0)
        {
            item = _pool.Dequeue();
        }
        else
        {
            item = Instantiate(_prefab);
        }

        item.gameObject.SetActive(true);

        return item;
    }

    public void ReturnObject(T item)
    {
        item.gameObject.SetActive(false);
        _pool.Enqueue(item);
    }

    protected virtual T CreateItem()
    {
        return Instantiate(_prefab, transform);
    }
}
