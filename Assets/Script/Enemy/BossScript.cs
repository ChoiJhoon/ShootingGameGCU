using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int BossLife = 100; // ������ �ʱ� ���� ��
    public int mobScore = 1000;

    private ScoreUI scoreUI;

    public static event Action OnBossDefeated;

    private void Start()
    {
        scoreUI = FindObjectOfType<ScoreUI>(); // ScoreUI ��ü ����
    }

    public void TakeDamage(int damage)
    {
        BossLife -= damage;
        OnBossDefeated?.Invoke();
        if (BossLife <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (scoreUI != null)
        {
            scoreUI.AddMobScore(mobScore);
        }
        Destroy(gameObject);
    }
}
