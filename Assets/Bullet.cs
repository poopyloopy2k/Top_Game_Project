using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{


    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private int damage;
    AmmoBar ammoBar;

    public Vector3 moveDirection;
    private void Start()
    {
        moveDirection = transform.right;
        SetMoveDirection(moveDirection);
    }
    private void Update()
    {
        MoveProjectile();
    }
    private void MoveProjectile()
    {
        Player2DControl player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2DControl>();
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
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
}
