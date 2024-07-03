using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public static int score = 0;
    public float scoreIncreaseInterval = 1.0f; // 1�ʸ��� ���� ����
    public int scoreIncreaseAmount = 10; // ���� ������

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
        // ���ο� �������� InvokeRepeating �޼��带 �ٽ� ȣ��
        CancelInvoke("ScoreIncrease");
        InvokeRepeating("ScoreIncrease", scoreIncreaseInterval, scoreIncreaseInterval);
    }
}

