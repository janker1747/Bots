using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ScanAbility : MonoBehaviour
{
    [SerializeField] private SkillMenu _skillMenu;
    [SerializeField] private ResourceManager _resourceManager;

    [Header("Scan Settings")]
    [SerializeField] private float scanRadius = 10f;
    [SerializeField] private LayerMask scanLayer;
    [SerializeField] private float scanDuration = 3f;

    [Header("Outline Settings")]
    [SerializeField] private Material outlineMaterial;

    public event Action<List<Transform>> ResourcesScanned;

    private readonly List<Transform> scannedTargets = new();

    private void OnEnable()
    {
        _skillMenu.ScanButtonClicked += TriggerScan;
    }

    private void OnDisable()
    {
        _skillMenu.ScanButtonClicked -= TriggerScan;
    }

    private void TriggerScan()
    {
        Debug.Log("Сканирование началось");
        StartCoroutine(ScanRoutine());
    }

    private IEnumerator ScanRoutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(scanDuration);

        Collider[] targets = Physics.OverlapSphere(transform.position, scanRadius, scanLayer);

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

        yield return waitForSeconds;

        ResourcesScanned?.Invoke(scanned);

        foreach (IScannable target in scannedScannables)
        {
            target.OnScanEnded();
        }
    }

    public List<Transform> GetScannedTargets()
    {
        return new List<Transform>(scannedTargets);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}