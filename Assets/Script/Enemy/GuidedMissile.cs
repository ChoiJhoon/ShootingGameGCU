using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : MonoBehaviour
{
    public float speed = 2.0f;
    public float rotateSpeed = 200.0f; 
    private Transform target; 

    private void Start()
    {
        // "Player" 태그를 가진 객체를 찾습니다.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform; 
        }
    }

    private void Update()
    {
        if (target != null)
        {
            // 타겟 방향으로 회전
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            // 회전 쿼터니언 계산
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);

            // 타겟 방향으로 이동
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            Debug.Log("미사일 방어 성공");
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("미사일 피격 당함");
            gameObject.SetActive(false);
        }
    }    
}
