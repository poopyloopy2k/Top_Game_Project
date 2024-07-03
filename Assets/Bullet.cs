using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // Урон, который наносит пуля
    public float lifeTime = 2f; // Время жизни пули

    private void Start()
    {
        // Уничтожить пулю после истечения времени жизни
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Нанести урон врагу
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            // Уничтожить пулю при столкновении с любым объектом, кроме игрока
            Destroy(gameObject);
        }
    }
}
