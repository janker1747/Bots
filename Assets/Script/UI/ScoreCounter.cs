using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _count;

    public event Action<int> OnScoreChanged;
    public int CurrentScore => _count;

    public void AddPoint()
    {
        _count++;
        OnScoreChanged?.Invoke(_count);
    }
}