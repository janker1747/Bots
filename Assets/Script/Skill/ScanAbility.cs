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

    public event Action<List<Transform>> ResourcesScanned;

    private void Awake()
    {
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

        Collider[] targets = Physics.OverlapSphere(transform.position, _scanRadius, _scanLayer);

        List<Transform> scanned = new();
        List<IScannable> scannedScannables = new();

        foreach (Collider collision in targets)
        {
            IScannable scannable = collision.GetComponent<IScannable>();

            if (scannable != null)
            {
                scannable.OnScanned();
                scanned.Add(collision.transform);
                scannedScannables.Add(scannable);
            }
        }

        yield return scanDuration;

        ResourcesScanned?.Invoke(scanned);

        foreach (IScannable target in scannedScannables)
        {
            target.OnScanEnded();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }
}
