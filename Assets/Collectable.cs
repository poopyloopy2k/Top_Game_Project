using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableType type;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] public int price;
    [Header("PotionSettings")]
    [SerializeField] public int health;
    [SerializeField] public int ammo;
    [Header("WeaponSettings")]
    [SerializeField] private GameObject weapon;

    public enum CollectableType
    {
        Weapon,
        Potion
    }

    private bool isPlayerInRange = false;
    private Player2DControl player;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && player.cm.coinCount >= price)
        {
            player.cm.coinCount -= price;
            if (type == CollectableType.Potion)
            {
                player.GetHealth(health);
                Destroy(gameObject);
            }
            else if (type == CollectableType.Weapon)
            {
                player.GetComponent<Inventory>().AddWeapon(weapon);
                Destroy(gameObject); // �������� ������ ����� ���������� � ���������
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player2DControl>())
        {
            isPlayerInRange = true;
            player = other.GetComponent<Player2DControl>();
            if (price > 0)
            {
                priceText.text = price.ToString();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player2DControl>())
        {
            isPlayerInRange = false;
            player = null;
            priceText.text = string.Empty;
        }
    }
}