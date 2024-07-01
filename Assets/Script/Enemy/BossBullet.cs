using System.Collections;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public Transform player;        // 플레이어의 Transform 참조
    public float bulletSpeed = 10f; // 총알의 이동 속도

    private GameObject[] BossBulletPool;
    private int poolSize = 9;

    private int currentIndex = 0;

    void Start()
    {
        InitializeBulletPool();

        StartCoroutine(ShootBullets());
    }

    private void InitializeBulletPool()
    {
        //총알 풀 초기화
        BossBulletPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            BossBulletPool[i] = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BossBulletPool[i].SetActive(false);
        }
    }

    private IEnumerator ShootBullets()
    {
        while (true)
        {
            float delay = Random.Range(1f, 5f); // 1에서 5초 사이의 랜덤한 발사 간격
            yield return new WaitForSeconds(delay);

            Vector3 targetPosition = player.position; // 플레이어의 위치를 목표 위치로 설정

            for (int i = 0; i < 3; i++)
            {
                GameObject bullet = GetNextBulletFromPool();
                if (bullet != null)
                {
                    bullet.transform.position = transform.position;

                    Vector3 direction = (targetPosition - transform.position).normalized;
                    Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                    bulletRigidbody.velocity = direction * bulletSpeed;

                    bullet.SetActive(true);

                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }

    private GameObject GetNextBulletFromPool()
    {
        GameObject nextBullet = BossBulletPool[currentIndex];
        currentIndex = (currentIndex + 1) % poolSize; // 순환 구조로 인덱스 업데이트
        return nextBullet;
    }
}
