using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _scan;
    [SerializeField] private ScanAbility _scanAbility;

    public ScanAbility ScanAbility => _scanAbility;

    public event Action ScanButtonClicked;

    private void Awake()
    {
        HideMenu();
        _scan.onClick.AddListener(() => ScanButtonClicked?.Invoke());
    }

    public void ToggleMenu()
    {
        if (_canvasGroup.alpha > 0)
        {
            HideMenu();
        }
        else
        {
            ShowMenu();
        }
    }

    private void ShowMenu()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    private void HideMenu()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}