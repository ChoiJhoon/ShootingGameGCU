using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int BossLife = 100; // 보스의 초기 생명 값

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
        Destroy(gameObject);
    }
}
