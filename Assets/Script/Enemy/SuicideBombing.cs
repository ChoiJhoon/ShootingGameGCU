using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SuicideBombing : MonoBehaviour
{
    public float speed = 5.0f;
    public int MobsLife = 10;
    public int mobScore = 20;

    private Rigidbody rb;

    private ScoreUI scoreUI;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        scoreUI = FindObjectOfType<ScoreUI>(); // ScoreUI 객체 참조

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
        if (scoreUI != null)
        {
            scoreUI.AddMobScore(mobScore);
        }
        Destroy(gameObject);
    }
}
