using Unity.VisualScripting;
using UnityEngine;

public class Player2DControl : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveVelocity;
    //Добавил CoinManager для подбора монет
    public CoinManager cm;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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