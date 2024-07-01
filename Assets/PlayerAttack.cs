using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject sword;
    [SerializeField] private float meleeSpeed;
    [SerializeField] private int damage;
    float timeUntilMelee;

    void Start()
    {
        anim = GetComponent<Animator>();
        sword.SetActive(false);
    }
    private void Update()
    {
        if (timeUntilMelee <= 0f) 
        {
            if(Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack");
                timeUntilMelee = meleeSpeed;
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
    }
}
