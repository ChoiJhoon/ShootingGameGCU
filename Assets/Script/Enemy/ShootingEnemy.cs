using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField]
    public GameObject enemyBulletPrefab; // �߻��� �Ѿ� ������
    public Transform firePoint; // �Ѿ��� �߻�� ��ġ
    public float fireRate = 2.0f; // �߻� ����
    public int poolSize = 10; // ������Ʈ Ǯ�� ũ��

    private List<GameObject> bulletPool; // �Ѿ� Ǯ
    private int currentIndex = 0; // ���� ����� �Ѿ� �ε���

    private void Start()
    {
        InitializeBulletPool();
        InvokeRepeating("Shoot", 0f, fireRate);
    }

    private void InitializeBulletPool()
    {
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    private void Shoot()
    {
        GameObject bullet = GetNextBulletFromPool();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);
        }
    }

    private GameObject GetNextBulletFromPool()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[currentIndex].activeInHierarchy)
            {
                GameObject bullet = bulletPool[currentIndex];
                currentIndex = (currentIndex + 1) % poolSize;
                return bullet;
            }
        }

        // ��� �Ѿ��� ��� ���� ��� ���ο� �Ѿ��� �߰�
        GameObject newBullet = Instantiate(enemyBulletPrefab);
        newBullet.SetActive(false);
        bulletPool.Add(newBullet);
        return newBullet;
    }
}
