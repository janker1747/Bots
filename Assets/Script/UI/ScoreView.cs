using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private Text _text;

    private void OnEnable()
    {
        _scoreCounter.OnScoreChanged += UpdateText;
        UpdateText(_scoreCounter.CurrentScore);
    }

    private void OnDisable()
    {
        _scoreCounter.OnScoreChanged -= UpdateText;
    }

    private void UpdateText(int score)
    {
        _text.text = score.ToString();
    }
}