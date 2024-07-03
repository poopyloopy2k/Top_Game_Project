using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player2DControl : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI AmmoText;
    public float speed;
    public int currentHealth;
    public int maxHealth = 100;
    public int currentAmmo;
    public int maxAmmo;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveVelocity;
    //Добавил CoinManager для подбора монет
    public CoinManager cm;
    public HealthBar healthBar;
    public AmmoBar AmmoBar;
    public GameObject deathPanel;
    private SpriteRenderer spriteRenderer;
    //Для звука
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentAmmo = maxAmmo;
        healthBar.SetMaxHealth(maxHealth);
        AmmoBar.SetMaxAmmo(maxAmmo);
        UpdateStats();

    }
    public void TakeDamage(int damage)
    {
        audioManager.PlaySFX(audioManager.hurt);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        audioManager.PlaySFX(audioManager.death);
        Debug.Log("Player is dead");
        deathPanel.SetActive(true);
        gameObject.SetActive(false);

    }
    public void Respawn()
    {
        Debug.Log("Player is respawn");
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        cm.ResetCoins();
        transform.position = Vector2.zero;
        deathPanel.SetActive(false);
        gameObject.SetActive(true);

    }
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        // Изменение анимационного состояния на основе ввода пользователя
        anim.SetBool("isRunning", moveInput != Vector2.zero);

        // Получение позиции курсора мыши
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Проверка направления персонажа относительно курсора мыши по оси X
        if ((mousePosition.x < transform.position.x && transform.localScale.x > 0) ||
            (mousePosition.x > transform.position.x && transform.localScale.x < 0))
        {
            // Разворот персонажа
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20);
        }
        UpdateStats();

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
            audioManager.PlaySFX(audioManager.coin);
        }
        else if (other.gameObject.CompareTag("Collision"))
        {
            audioManager.PlaySFX(audioManager.wallCollision);
        }
    }
    public void GetHealth(int health)
    {
        this.currentHealth += health;
        this.currentHealth = Mathf.Clamp(this.currentHealth, 0, maxHealth);
        healthBar.slider.value = currentHealth;
        
        UpdateStats();

    }
    public void GetAmmo(int ammo)
    {
        this.currentAmmo += ammo;
        this.currentAmmo = Mathf.Clamp(this.currentAmmo, 0, maxAmmo);
        
        UpdateStats();

    }
    private void UpdateStats()
    {
        HealthText.text = $"{currentHealth}/{maxHealth}";
        AmmoText.text = $"{currentAmmo}/{maxAmmo}";
    }
}