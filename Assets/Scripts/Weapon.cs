using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Camera cam;
    public bool isPlayer;
    public GameObject item;
    public bool isPickedUp = false;

    private void Start()
    {
        transform.SetLocalPositionAndRotation(Vector2.zero, Quaternion.identity);
        cam = FindObjectOfType<Camera>();
    }

    private void FixedUpdate()
    {
        if (isPickedUp)
        {
            Vector2 currentPos = cam.ScreenToWorldPoint(Input.mousePosition);
            float a = Mathf.Atan2(-currentPos.x, currentPos.y) + Mathf.Rad2Deg;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            other.GetComponent<Inventory>().AddWeapon(gameObject);
            isPickedUp = true;
            gameObject.SetActive(false);
        }
    }
}
