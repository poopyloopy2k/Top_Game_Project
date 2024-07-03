using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float meleeSpeed; // ����� ����� �������
    [SerializeField] private int damage; // ���� �� �����
    [SerializeField] private GameObject bulletPrefab; // ������ ����
    [SerializeField] private Transform firePoint; // ����� ��������
    [SerializeField] private float bulletSpeed = 10f; // �������� ����

    private float timeUntilMelee;
    private AudioManager audioManager;
    private Inventory inventory;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        if (timeUntilMelee <= 0f)
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                if (inventory.hotWeapon == -1)
                {
                    MeleeAttack();
                }
                else
                {
                    RangedAttack();
                }
            }
        }
        else
        {
            timeUntilMelee -= Time.deltaTime;
        }
    }

    private void MeleeAttack()
    {
        anim.SetTrigger("Attack");
        timeUntilMelee = meleeSpeed;
        audioManager.PlaySFX(audioManager.swordHit);
    }

    private void RangedAttack()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
        // ��������, �������� �������� �������� � ����
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            Debug.Log("Enemy hit");
        }
        if (other.CompareTag("Chest"))
        {
            Chest chest = other.GetComponent<Chest>();
            if (chest != null)
            {
                chest.OpenChest();
            }
        }
    }
}