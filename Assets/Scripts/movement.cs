using UnityEngine;

public class Player2DControl : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        // »зменение анимационного состо€ни€ на основе ввода пользовател€
        anim.SetBool("isRunning", moveInput != Vector2.zero);

        // ѕолучение позиции курсора мыши
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ѕроверка направлени€ персонажа относительно курсора мыши по оси X
        if ((mousePosition.x < transform.position.x && transform.localScale.x > 0) ||
            (mousePosition.x > transform.position.x && transform.localScale.x < 0))
        {
            // –азворот персонажа
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}