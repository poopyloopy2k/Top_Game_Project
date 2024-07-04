using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float meleeSpeed; // Время между атаками
    [SerializeField] private int damage; // Урон от атаки
    [SerializeField] private GameObject bulletPrefab; // Префаб пули
    [SerializeField] private Transform firePoint; // Точка стрельбы
    [SerializeField] private float bulletSpeed = 10f; // Скорость пули
    public AmmoBar AmmoBar;
    [SerializeField] private int currentAmmo;
    [SerializeField] private int maxAmmo;
    public TextMeshProUGUI AmmoText;

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
        currentAmmo = maxAmmo;
        AmmoBar.SetMaxAmmo(maxAmmo);
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
                else if (inventory.hotWeapon == 1)
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
        currentAmmo -= currentAmmo;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;
        // Возможно, добавить анимацию стрельбы и звук
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (inventory.hotWeapon == -1)
            {
                other.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
            else if (inventory.hotWeapon == 1)
            {
                other.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
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
    private void UpdateStats()
    {
        AmmoText.text = $"{currentAmmo}/{maxAmmo}";
    }
}
