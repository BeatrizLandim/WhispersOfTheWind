using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variáveis públicas
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public bool lockFlip;

    // Crouch
    public Vector2 crouchSize = new Vector2(1f, 0.7f);
    public Vector2 crouchOffset = new Vector2(0f, -0.15f);

    public float inputX;   
    
    // Componentes
    public Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D box;

    private PlayerCrouch crouch;

    public bool isGrounded;

    bool estaDancando = false;

   public Transform groundCheck;
   public float groundRadius = 0.1f;
   public LayerMask groundLayer;


    void Start()
    {
        crouch = GetComponent<PlayerCrouch>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (estaDancando)
            return;

        UpdateAnimator();
        Movement();
        Attack();
    }  

    void FixedUpdate()
    {
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

    // Evita travamento se entrar no chão com alta velocidade
    if (isGrounded && rb.velocity.y > 1f)
        isGrounded = false;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsJumping", !isGrounded);

        if (crouch != null)
            animator.SetBool("IsCrouching", crouch.isCrouching);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");

            AudioManager.Instance.Play("ataque");
        }
    }


    private void Movement()
    {
    float moveInput = Input.GetAxis("Horizontal");
    float speed = moveSpeed;

    if (crouch != null && crouch.isCrouching)
        speed *= 0.6f;

    rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

    if (!lockFlip)
    {
    MirrorSprite(moveInput);
    }
    

    }


    private void MirrorSprite(float moveInput)
    {
        if (moveInput > 0) spriteRenderer.flipX = false;
        else if (moveInput < 0) spriteRenderer.flipX = true;
    }

    // Detecta chão
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    public void IniciarDanca()
    {
        estaDancando = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("Dance", true);
    }

    public void PararDanca()
    {
        estaDancando = false;
        animator.SetBool("Dance", false);
    }
}
