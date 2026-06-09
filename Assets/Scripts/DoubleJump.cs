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

    private PlayerMovement playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Move();
        Jump();

        if (playerMovement.isGrounded)
        {
            jumpCount = 0;
        }
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
                    AudioManager.Instance.Play("Pulo");
                }

                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
            }
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
