using UnityEngine;

public class SkillMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Awake()
    {
        HideMenu();
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