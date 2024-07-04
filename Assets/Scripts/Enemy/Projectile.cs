    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.TextCore.Text;

    public class Projectile : MonoBehaviour
    {
        public float speed = 5f;
        public float lifetime = 5f; // Время жизни пули
        [SerializeField] private int damage;

        private Vector2 direction;

        private void Start()
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player != null)
            {
                direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
            }
            else
            {
                direction = Vector2.zero; // Пуля не будет двигаться, если игрок не найден
            }

            // Уничтожаем пулю через 'lifetime' секунд
            Destroy(gameObject, lifetime);
        }

        private void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            { 

            other.GetComponent<Player2DControl>().TakeDamage(damage); 
            DestroyProjectile(); 
            Debug.Log("damage to player");
                
            }
        }

        private void DestroyProjectile()
        {
            Destroy(gameObject);
        }
    }
