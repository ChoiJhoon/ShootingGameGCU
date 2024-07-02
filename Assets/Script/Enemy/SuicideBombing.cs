using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBombing : MonoBehaviour
{
    public float speed = -5.0f;
    public int MobsLife = 10;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("플레이어 공격");
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        MobsLife -= damage;
        if (MobsLife <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
