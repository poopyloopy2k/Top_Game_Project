using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float meleeSpeed;
    [SerializeField] private int damage;
    float timeUntilMelee;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Update()
    {
        if (timeUntilMelee <= 0f) 
        {
            if(Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack");
                timeUntilMelee = meleeSpeed;
                audioManager.PlaySFX(audioManager.swordHit);
               
            }
            
        }
        else
        {
            timeUntilMelee -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
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
