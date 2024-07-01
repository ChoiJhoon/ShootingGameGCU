using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBombing : MonoBehaviour
{
    public float speed = -5.0f;

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
            Debug.Log("피격 당함");
            Destroy(this.gameObject);
        }
    }
}
