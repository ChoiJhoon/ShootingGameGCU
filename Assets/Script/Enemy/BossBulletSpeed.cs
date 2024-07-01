using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletSpeed : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

        private void OnBecameInvisible()
    {
        DeactivateBullet();
    }

    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }
}
