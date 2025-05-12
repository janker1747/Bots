using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ScanAbility : MonoBehaviour
{
    [Header("Scan Settings")]
    [SerializeField] private float _scanRadius = 10f;
    [SerializeField] private LayerMask _scanLayer;
    [SerializeField] private float _duration = 3f;
    [SerializeField] private float _delay = 5f;

    [Header("Outline Settings")]
    [SerializeField] private Material outlineMaterial;

    private WaitForSeconds _scanDelay;
    private WaitForSeconds _scanDuration;

    public event Action<List<Resource>> ResourcesScanned;

    private int _bufferSize = 20;
    private Collider[] _colliderBuffer;

    private void Awake()
    {
        _colliderBuffer = new Collider[_bufferSize];
        _scanDelay = new WaitForSeconds(_delay);
        _scanDuration = new WaitForSeconds(_duration);
        StartCoroutine(ScanLoop());
    }

    private IEnumerator ScanLoop()
    {
        while (enabled)
        {
            yield return _scanDelay;
            yield return StartCoroutine(ScanRoutine());
        }
    }

    private IEnumerator ScanRoutine()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(
            transform.position,
            _scanRadius,
            _colliderBuffer,
            _scanLayer
        );

        List<Resource> scanned = new();
        List<IScannable> scannedScannables = new();

        for (int i = 0; i < numColliders; i++)
        {
            Collider collision = _colliderBuffer[i];
            IScannable scannable = collision.GetComponent<IScannable>();
            Resource resource = collision.GetComponent<Resource>();

            if (scannable != null)
            {
                scannable.HighlightScan();

                if (resource != null)
                {
                    scanned.Add(resource);
                }

                scannedScannables.Add(scannable);
            }
        }

        yield return _scanDuration;

        ResourcesScanned?.Invoke(scanned);

        foreach (IScannable target in scannedScannables)
        {
            target.RemoveScanHighlight();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }
}