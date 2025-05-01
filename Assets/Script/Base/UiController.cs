using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private SkillMenu _skillMenu;

    private void OnMouseDown()
    {
        if (_skillMenu != null)
        {
            _skillMenu.ToggleMenu();
        }
    }
}
