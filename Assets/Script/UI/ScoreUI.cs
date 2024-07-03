using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public static int score = 0;
    public float scoreIncreaseInterval = 1.0f; // 1초마다 점수 증가
    public int scoreIncreaseAmount = 10; // 점수 증가량

    private void Start()
    {
        score = 0;
        UpdateUI();
        InvokeRepeating("ScoreIncrease", scoreIncreaseInterval, scoreIncreaseInterval);
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
        scoreText2.text = scoreText.text;
    }

    void ScoreIncrease()
    {
        score += scoreIncreaseAmount;
        UpdateUI();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }

    public void AdjustScoreIncreaseInterval(float newInterval)
    {
        scoreIncreaseInterval = newInterval;
        // 새로운 간격으로 InvokeRepeating 메서드를 다시 호출
        CancelInvoke("ScoreIncrease");
        InvokeRepeating("ScoreIncrease", scoreIncreaseInterval, scoreIncreaseInterval);
    }
}

