using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [Header("Segundo Pulo")]
    public float jumpForce = 10f;

    [Header("Peso do Pulo")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Coyote Time")]
    public float coyoteTime = 0.15f;
    private float coyoteCounter;


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
        ApplyJumpWeight();
        Move();
        Jump();

        if (playerMovement.isGrounded)
        {
            coyoteCounter = coyoteTime;
            jumpCount = 0;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

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
            AudioManager.Instance.Play("Andar");
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
        // Primeiro pulo
            if ((playerMovement.isGrounded || coyoteCounter > 0) && jumpCount == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                jumpCount = 1;
                coyoteCounter = 0;

                AudioManager.Instance.Play("Pulo");
            }

        // Segundo pulo
            else if (jumpCount < maxJumps)
            {
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

    void ApplyJumpWeight()
    {
        // Cai mais rápido
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

            // Soltou o botão antes do topo
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

}
