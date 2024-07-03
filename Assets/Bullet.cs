using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // ����, ������� ������� ����
    public float lifeTime = 2f; // ����� ����� ����

    private void Start()
    {
        // ���������� ���� ����� ��������� ������� �����
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // ������� ���� �����
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            // ���������� ���� ��� ������������ � ����� ��������, ����� ������
            Destroy(gameObject);
        }
    }
}
