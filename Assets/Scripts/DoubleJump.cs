using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [Header("Segundo Pulo")]
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f; 
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private int jumpCount;
    public int maxJumps = 2; //quantidade de pulos
        bool estavaParado = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        CheckGround();
    }



    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

            if (horizontal != 0 && estavaParado)
        {
            AudioManager.Instance.Play("andar");
            estavaParado = false;
        }

            if (horizontal == 0)
        {
            estavaParado = true;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount < maxJumps)
            {
                if (jumpCount == 0)
                {
                    AudioManager.Instance.Play("pular");
                }

                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
            }
        }
    }

    void CheckGround()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (isGrounded)
        {
            jumpCount = 0; // reseta quando toca no chão
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}
