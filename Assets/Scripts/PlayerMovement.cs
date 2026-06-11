using System.Globalization;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    [Header("Fireball")]
    public GameObject fireballPrefab;
    public Transform shootPosition;
    public float cooldownFireball = 5f;
    public float delayFireball = 0.5f; // tempo até sair da mão

    private bool podeAtirar = true;
    private bool executandoFireball = false;

    // Variáveis públicas
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public bool lockFlip;

    public CooldownBars cooldownUI;

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

    private bool estaAndandoAudio = false;

    public float cooldownAtaque2 = 5f;

    private bool podeAtacar2 = true;

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
        Attack1();
        Attack2();
        Shoot();
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

    private void Attack1()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Ataque1");

            AudioManager.Instance.Play("Ataque");
        }
    }

        private void Attack2()
    {
       
            if (Input.GetMouseButtonDown(1) && isGrounded && podeAtacar2)
            {
                podeAtacar2 = false;

                cooldownUI.UsarAtaque2();

                animator.SetTrigger("Ataque2");

                AudioManager.Instance.Play("Ataque");

                Invoke(nameof(ResetarAtaque2), cooldownAtaque2);
            }
    }


    private void Movement()
    {
        if (executandoFireball)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
                return;
        }

    float moveInput = Input.GetAxis("Horizontal");

    bool estaAndando = isGrounded && Mathf.Abs(moveInput) > 0.01f;

    if (estaAndando)
    {
        if (!estaAndandoAudio)
        {
            AudioManager.Instance.Play("Andar");
            estaAndandoAudio = true;
        }
    }
    else
    {
        if (estaAndandoAudio)
        {
            AudioManager.Instance.Stop("Andar"); // se seu AudioManager tiver Stop
            estaAndandoAudio = false;
        }
    }

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
        animator.SetBool("Dance", true);
    }

    public void PararDanca()
    {
        estaDancando = false;
        animator.SetBool("Dance", false);
    }

    private void ResetarAtaque2()
    {
        podeAtacar2 = true;
    }

    private void Shoot()
    {
           
            if (Input.GetKeyDown(KeyCode.F) && isGrounded && podeAtirar && !executandoFireball)
            {
                StartCoroutine(FireballCoroutine());
                cooldownUI.UsarAtaque3();
            }
    }

    private IEnumerator FireballCoroutine()
    {
        podeAtirar = false;
        executandoFireball = true;

        lockFlip = true;

        animator.SetTrigger("Ataque3");

        yield return new WaitForSeconds(delayFireball);

        Vector3 position = shootPosition.position;

        Quaternion rotation;

        if (spriteRenderer.flipX)
            rotation = Quaternion.Euler(0, 180, 0);
        else
            rotation = Quaternion.identity;

        Instantiate(fireballPrefab, position, rotation);

        AudioManager.Instance.Play("Boladefogo");

        executandoFireball = false;
        lockFlip = false;

        yield return new WaitForSeconds(cooldownFireball);

        podeAtirar = true;
    }

    public static PlayerMovement Instance;

    void Awake()
    {
    Instance = this;
    }

}
