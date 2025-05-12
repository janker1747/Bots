using System;
using UnityEngine;

public class Resource : MonoBehaviour, IScannable
{
    [SerializeField] private Color highlightColor = Color.cyan;

    private Renderer _renderer;
    private Color[] _originalColors;


    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        CacheOriginalColors();
    }

    private void CacheOriginalColors()
    {
        _originalColors = new Color[_renderer.materials.Length];

        for (int i = 0; i < _renderer.materials.Length; i++)
        {
            _originalColors[i] = _renderer.materials[i].color;
        }
    }

    public void Scanned()
    {
        HighlightMaterials();
    }

    public void ScanEnded()
    {
        RestoreOriginalColors();
    }

    private void HighlightMaterials()
    {
        foreach (var mat in _renderer.materials)
        {
            mat.color = highlightColor;
        }
    }

    private void RestoreOriginalColors()
    {
        for (int i = 0; i < _renderer.materials.Length; i++)
        {
            _renderer.materials[i].color = _originalColors[i];
        }
    }


}