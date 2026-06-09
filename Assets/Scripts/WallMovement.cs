using UnityEngine;

public class WallMovement : MonoBehaviour
{
    [Header("Checagem de Paredes")]
    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Configurações do Wall Slide")]
    public float slideDelay = 0.8f;
    public float slideSpeed = 2f;
    private float wallTimer;

    [Header("Configurações do Wall Jump")]
    public float wallJumpForceX = 8f;
    public float wallJumpForceY = 12f;
    public float wallJumpControlLock = 0.15f;

    private bool isOnLeftWall;
    private bool isOnRightWall;

    private float horizontalInput;
    private float controlLockTimer;
    private int lastWallJumpDirection = 0;


    [Header("Referências")]
    private Rigidbody2D rb;
    private Animator anim;

    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        isOnLeftWall = Physics2D.OverlapCircle(
            wallCheckLeft.position,
            wallCheckRadius,
            groundLayer
        );

        isOnRightWall = Physics2D.OverlapCircle(
        wallCheckRight.position,
        wallCheckRadius,
        groundLayer
        );

        if (isOnLeftWall)
            spriteRenderer.flipX = true;

        if (isOnRightWall)
            spriteRenderer.flipX = false;

        if (!isOnLeftWall && !isOnRightWall)
        {
            lastWallJumpDirection = 0;
        }

        HandleWallSlide();
        HandleWallJump();

        anim.SetBool("encostarParede", IsTouchingWall());
    }

    bool IsTouchingWall()
    {
        return !playerMovement.isGrounded &&
           (isOnLeftWall || isOnRightWall);
    }

    void HandleWallSlide()
    {
        bool touchingWall = isOnLeftWall || isOnRightWall;

        if (!playerMovement.isGrounded && touchingWall && IsPressingTowardsWall())
        {
            wallTimer += Time.deltaTime;

            if (wallTimer >= slideDelay)
            {
                rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
            }
        }
        else
        {
            wallTimer = 0;
        }
    }

    bool IsPressingTowardsWall()
    {
        return (isOnLeftWall && horizontalInput < 0) ||
               (isOnRightWall && horizontalInput > 0);
    }

    void HandleWallJump()
    {
        if (controlLockTimer > 0)
        {
            controlLockTimer -= Time.deltaTime;

        if (controlLockTimer <= 0)
        {
            playerMovement.lockFlip = false;
        }

        return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!playerMovement.isGrounded &&
                isOnLeftWall &&
                lastWallJumpDirection != 1)
            {
                WallJump(1);
            }
                else if (!playerMovement.isGrounded &&
                    isOnRightWall &&
                    lastWallJumpDirection != -1)
            {
                WallJump(-1);
            }
        }
    }

    void WallJump(int direction)
    {
        wallTimer = 0;

        isOnLeftWall = false;
        isOnRightWall = false;

        rb.velocity = Vector2.zero;

        rb.AddForce(
            new Vector2(
                wallJumpForceX * direction,
                wallJumpForceY
            ),
            ForceMode2D.Impulse
        );

        spriteRenderer.flipX = direction < 0;

        anim.SetBool("encostarParede", false);

        lastWallJumpDirection = direction;
        controlLockTimer = wallJumpControlLock;

        playerMovement.lockFlip = true;
    }

    void OnDrawGizmosSelected()
    {
        if (wallCheckLeft != null)
            Gizmos.DrawWireSphere(wallCheckLeft.position, wallCheckRadius);

        if (wallCheckRight != null)
            Gizmos.DrawWireSphere(wallCheckRight.position, wallCheckRadius);

    }
}