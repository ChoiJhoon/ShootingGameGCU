using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int BossLife = 100; // 보스의 초기 생명 값
    public int mobScore = 1000;

    private ScoreUI scoreUI;

    private void Start()
    {
        scoreUI = FindObjectOfType<ScoreUI>(); // ScoreUI 객체 참조
    }

    public void TakeDamage(int damage)
    {
        BossLife -= damage;
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
