using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinCount;
    public Sprite openChestSprite;
    public float spreadRadius = 1f; // Радиус разброса монет
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
            spriteRenderer.sprite = openChestSprite; // Меняем спрайт на спрайт открытого сундука

            // Воспроизводим звук открытия сундука
            //audioManager.PlaySFX(audioManager.chestOpenSound);

            // Выбрасываем монеты
            for (int i = 0; i < coinCount; i++)
            {
                // Вычисляем случайное смещение в радиусе spreadRadius
                Vector2 randomOffset = Random.insideUnitCircle.normalized * spreadRadius;
                Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

                GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
                Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    float randomForce = Random.Range(5f, 10f); // Увеличение силы
                    rb.AddForce(randomOffset.normalized * randomForce, ForceMode2D.Impulse);
                }

            }
        }
    }
}