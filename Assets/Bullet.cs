using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{


    [SerializeField] private float moveSpeed = 22f;
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
}
