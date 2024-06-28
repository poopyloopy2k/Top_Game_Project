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

        // ��������� ������������� ��������� �� ������ ����� ������������
        anim.SetBool("isRunning", moveInput != Vector2.zero);

        // ��������� ������� ������� ����
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // �������� ����������� ��������� ������������ ������� ���� �� ��� X
        if ((mousePosition.x < transform.position.x && transform.localScale.x > 0) ||
            (mousePosition.x > transform.position.x && transform.localScale.x < 0))
        {
            // �������� ���������
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