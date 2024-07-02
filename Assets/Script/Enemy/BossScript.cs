using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int BossLife = 100; // ������ �ʱ� ���� ��

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
