using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float force = 10.0f;
    private Rigidbody rb;

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
        gameObject.SetActive(false); // ��Ȱ��ȭ�Ͽ� Ǯ�� ��ȯ
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
