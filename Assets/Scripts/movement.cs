using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player2DControl : MonoBehaviour
{
    public float speed;
    public int currentHealth;
    public int maxHealth = 100;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveVelocity;
    //Добавил CoinManager для подбора монет
    public CoinManager cm;
    public HealthBar healthBar;
    public GameObject deathPanel;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0 )
        {
            Die();
        }
    }
    void Die()
    {
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
        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20);
        }

    }
    
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {   
            Destroy(other.gameObject);
            cm.coinCount++;
            
        }
    }
}