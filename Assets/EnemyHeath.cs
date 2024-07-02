using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health;
    AudioManager manager;
    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        manager.PlayLoopingSFX(manager.spider);
    }
    public void TakeDamage(int damage)
    {
        manager.PlaySFX(manager.spiderHurt);
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
        manager.StopSFX();
        manager.PlaySFX(manager.spiderDeath);

    }
}
