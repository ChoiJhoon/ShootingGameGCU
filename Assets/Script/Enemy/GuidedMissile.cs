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
        // "Player" �±׸� ���� ��ü�� ã���ϴ�.
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
            // Ÿ�� �������� ȸ��
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            // ȸ�� ���ʹϾ� ���
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);

            // Ÿ�� �������� �̵�
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            Debug.Log("�̻��� ��� ����");
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�̻��� �ǰ� ����");
            gameObject.SetActive(false);
        }
    }    
}
