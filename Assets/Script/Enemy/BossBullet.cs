using System.Collections;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public GameObject bulletPrefab; // �߻��� �Ѿ� ������
    public Transform player;        // �÷��̾��� Transform ����
    public float bulletSpeed = 10f; // �Ѿ��� �̵� �ӵ�

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
        //�Ѿ� Ǯ �ʱ�ȭ
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
            float delay = Random.Range(1f, 5f); // 1���� 5�� ������ ������ �߻� ����
            yield return new WaitForSeconds(delay);

            Vector3 targetPosition = player.position; // �÷��̾��� ��ġ�� ��ǥ ��ġ�� ����

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
        currentIndex = (currentIndex + 1) % poolSize; // ��ȯ ������ �ε��� ������Ʈ
        return nextBullet;
    }
}
