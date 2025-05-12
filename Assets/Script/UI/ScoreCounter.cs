using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _count;

    public event Action<int> ScoreChanged;
    public int CurrentScore => _count;

    public void AddPoint()
    {
        _count++;
        ScoreChanged?.Invoke(_count);
    }
}