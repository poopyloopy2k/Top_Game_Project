using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinCount;
    public Sprite openChestSprite;
    public float spreadRadius = 1f; // ������ �������� �����
    private SpriteRenderer spriteRenderer;
    private bool isOpened = false;
    private AudioManager audioManager;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OpenChest()
    {
        if (!isOpened)
        {
            isOpened = true;
            spriteRenderer.sprite = openChestSprite; // ������ ������ �� ������ ��������� �������

            // ������������� ���� �������� �������
            //audioManager.PlaySFX(audioManager.chestOpenSound);

            // ����������� ������
            for (int i = 0; i < coinCount; i++)
            {
                // ��������� ��������� �������� � ������� spreadRadius
                Vector2 randomOffset = Random.insideUnitCircle.normalized * spreadRadius;
                Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

                GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
                Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    float randomForce = Random.Range(5f, 10f); // ���������� ����
                    rb.AddForce(randomOffset.normalized * randomForce, ForceMode2D.Impulse);
                }

            }
        }
    }
}