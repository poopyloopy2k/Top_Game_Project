using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Coin random splash")]
    private float delay = 0;
    private float pastTime = 0;
    private float when = 1.0f;
    private Vector3 off;

    private void Awake()
    {
        off = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
    }

    void Start()
    {

    }

    void Update()
    {
        if (when >= delay)
        {
            pastTime = Time.deltaTime;
            transform.position += off * Time.deltaTime;
            delay += pastTime;
        }
    }   
}