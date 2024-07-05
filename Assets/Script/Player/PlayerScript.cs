using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rigid;
    private float moveForce = 7.0f;
    private float x_Axis;
    private float z_Axis;
    private float previousX_Axis;
    [SerializeField]
    public float power;
    public Animator animator;

    [SerializeField]
    private GameObject bullet;
    private GameObject[] bulletPool;
    public Transform FirePos;

    private GameOverManager gameOverManager;

    [SerializeField]
    private GameObject shieldPrefab;
    [SerializeField]
    private GameObject currentShield;
    private bool isShieldActive = false;
    [SerializeField] private float shieldDuration = 3f;
    [SerializeField] private float shieldCooldown = 5f;
    private bool isShieldOnCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        InitializeBulletPool();
        previousX_Axis = 0f;
        gameOverManager = FindObjectOfType<GameOverManager>();
    }
    private void InitializeBulletPool()
    {
        bulletPool = new GameObject[10]; // 예시로 10개의 총알을 풀링합니다.
        for (int i = 0; i < bulletPool.Length; i++)
        {
            bulletPool[i] = Instantiate(bullet);
            bulletPool[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Shooting();
        Shield();
        CamerainPlayer();
    }

    private void PlayerMovement()
    {
        x_Axis = Input.GetAxisRaw("Horizontal");
        z_Axis = Input.GetAxisRaw("Vertical");

        Vector3 velocity = new Vector3(x_Axis, 0, z_Axis);
        velocity *= moveForce;
        rigid.velocity = velocity;

        HandleAnimations();
    }
    private void HandleAnimations()
    {
        animator.SetFloat("New Float", x_Axis);       
    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = GetNextBulletFromPool();
            if (bullet != null)
            {
                bullet.transform.position = FirePos.position;
                bullet.transform.rotation = FirePos.rotation;
                bullet.SetActive(true);
            }
            BulletManager bulletScript = bullet.GetComponent<BulletManager>();
            bulletScript.SetDamage((int)power);
        }
    }

    private GameObject GetNextBulletFromPool()
    {
        for (int i = 0; i < bulletPool.Length; i++)
        {
            if (bulletPool[i] != null && !bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }
        return null;
    }

    void Shield()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isShieldActive && !isShieldOnCooldown)
        {
            ActivateShield();
        }
    }

    void ActivateShield()
    {
        Vector3 offset = transform.forward * 1;
        // 쉴드를 플레이어의 위치에 offset을 더해 앞으로 이동시켜 생성합니다.
        currentShield = Instantiate(shieldPrefab, transform.position + offset, transform.rotation);
        // 쉴드 활성화 상태로 변경합니다.
        isShieldActive = true;

        StartCoroutine(DisableShieldAfterDelay(shieldDuration));
    }

    IEnumerator DisableShieldAfterDelay(float delay)
    {
        // delay 초 후에 쉴드를 비활성화합니다.
        yield return new WaitForSeconds(delay);

        // 쉴드를 비활성화하고 메모리에서 제거합니다.
        Destroy(currentShield);
        isShieldActive = false;

        // 쿨타임을 시작합니다.
        StartCoroutine(StartShieldCooldown());
    }

    IEnumerator StartShieldCooldown()
    {
        // 쿨타임 동안 대기합니다.
        isShieldOnCooldown = true;
        yield return new WaitForSeconds(shieldCooldown);
        isShieldOnCooldown = false;
    }

    void CamerainPlayer()
    {
        Vector3 worldpos = Camera.main.WorldToViewportPoint(this.transform.position);
        worldpos.x = Mathf.Clamp01(worldpos.x);
        worldpos.y = Mathf.Clamp01(worldpos.y);
        this.transform.position = Camera.main.ViewportToWorldPoint(worldpos);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체의 태그를 확인하여 게임 오버 처리
        if (other.CompareTag("Enemy") || other.CompareTag("Boss") ||
            other.CompareTag("SuicideEnemy") || other.CompareTag("ShootingEnemys"))
        {
            Debug.Log("사망");
            HandleGameOver();
        }
    }

    private void HandleGameOver()
    {
        gameOverManager.ShowGameOver();
    }
}
