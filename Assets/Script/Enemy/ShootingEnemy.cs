using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField]
    public GameObject enemyBulletPrefab; // 발사할 총알 프리팹
    public Transform firePoint; // 총알이 발사될 위치
    public float fireRate = 2.0f; // 발사 간격
    public int poolSize = 10; // 오브젝트 풀의 크기

    private List<GameObject> bulletPool; // 총알 풀
    private int currentIndex = 0; // 현재 사용할 총알 인덱스

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

        // 모든 총알이 사용 중인 경우 새로운 총알을 추가
        GameObject newBullet = Instantiate(enemyBulletPrefab);
        newBullet.SetActive(false);
        bulletPool.Add(newBullet);
        return newBullet;
    }
}
