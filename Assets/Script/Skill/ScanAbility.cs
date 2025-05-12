using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ScanAbility : MonoBehaviour
{
    [Header("Scan Settings")]
    [SerializeField] private float _scanRadius = 10f;
    [SerializeField] private LayerMask _scanLayer;
    [SerializeField] private float _scanDuration = 3f;
    [SerializeField] private float _delay = 5f;

    [Header("Outline Settings")]
    [SerializeField] private Material outlineMaterial;

    public event Action<List<Resource>> ResourcesScanned;

    private int _bufferSize = 20;
    private Collider[] _colliderBuffer;

    private void Awake()
    {
        _colliderBuffer = new Collider[_bufferSize];
        StartCoroutine(ScanLoop());
    }

    private IEnumerator ScanLoop()
    {
        WaitForSeconds scanDelay = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return scanDelay;
            yield return StartCoroutine(ScanRoutine());
        }
    }

    private IEnumerator ScanRoutine()
    {
        WaitForSeconds scanDuration = new WaitForSeconds(_scanDuration);

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
                scannable.Scanned();
                if (resource != null)
                {
                    scanned.Add(resource);
                }
                scannedScannables.Add(scannable);
            }
        }

        yield return scanDuration;

        ResourcesScanned?.Invoke(scanned);

        foreach (IScannable target in scannedScannables)
        {
            target.ScanEnded();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }
}