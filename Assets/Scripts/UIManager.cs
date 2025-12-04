using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text stepsText;
    public TMP_Text promptText;
    public GameObject gameOverPanel;
    public TMP_Text gameOverScoreText;
    public TMP_Text gameOverStepsText;
    public GameObject gamePanel;

    public void UpdateScore(int score) => scoreText.text = $"Score: {score}";
    public void UpdateSteps(int steps) => stepsText.text = $"Steps: {steps}";
    public void SetPrompt(string s) => promptText.text = s;

    public void ShowSequenceNumbers(IReadOnlyList<Spot> sequence)
    {
        ClearSequenceNumbers();
        for (int i = 0; i < sequence.Count; i++)
        {
            sequence[i].ShowSequenceNumber(i + 1);
        }
    }

    public void ClearSequenceNumbers()
    {
        var spots = FindObjectsOfType<Spot>();
        foreach (var s in spots) s.HideSequenceNumber();
    }

    public void FlashWrong(Spot spot)
    {
        spot.ShowWrongFeedback();
    }

    public void ShowGameOver(int score, int steps)
    {
        HideGamePanel();
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = $"Final Score: {score}";
        gameOverStepsText.text = $"Steps: {steps}";
    }

    public void HideGamePanel()
    {
        gamePanel.SetActive(false); 
    }
}
