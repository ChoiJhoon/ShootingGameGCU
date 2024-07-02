using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float force = 10.0f;
    private Rigidbody rb;

    public int damage;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);
    }

    private void OnBecameInvisible()
    {
        DeactivateBullet();
    }

    private void DeactivateBullet()
    {
        gameObject.SetActive(false); // 비활성화하여 풀에 반환
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            BossScript boss = other.GetComponent<BossScript>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            DeactivateBullet();
        }
        else if (other.CompareTag("SuicideEnemy"))
        {
            SuicideBombing suicideEnemy = other.GetComponent<SuicideBombing>();
            if (suicideEnemy != null)
            {
                suicideEnemy.TakeDamage(damage);
            }
            DeactivateBullet();
        }
        else if (other.CompareTag("ShootingEnemys"))
        {
            ShootingEnemy shootingEnemy = other.GetComponent<ShootingEnemy>();
            if (shootingEnemy != null)
            {
                shootingEnemy.TakeDamage(damage);
            }
            DeactivateBullet();
        }
    }
}
