using UnityEngine;
using UnityEngine.UI;

public class BossMagoFSM : MonoBehaviour, IDamageable
{
    [Header("Referencias")]
    [SerializeField] private Transform jogador;
    [SerializeField] private Animator animator;
    [SerializeField] private Image barraDeVidaImage;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D bossCollider;

    [Header("Biotransporte")]
    [SerializeField] private float bioIntervaloGlobal = 40f;
    [SerializeField] private float bioTempoParaSumir = 0.45f;
    [SerializeField] private float bioTempoSumido = 1f;
    [SerializeField] private float bioDistanciaAoLado = 1.8f;
    [SerializeField] private float bioRaioExplosao = 2.2f;
    [SerializeField] private int bioDano = 25;
    [SerializeField] private LayerMask playerLayer;

    [Header("Recuperacao")]
    [SerializeField] private float bioRecuperacao = 2.5f;

    [Header("Vida")]
    [SerializeField] private int vidaMaxima = 500;
    [SerializeField] private int vidaParaFuria = 150;
    [SerializeField] private float multiplicadorFuria = 0.8f;
    [SerializeField] private bool soTomaDanoQuandoVulneravel = true;

    private BossEstado estadoAtual;
    private int vidaAtual;
    private float bioTimerGlobal;
    private bool vulneravel;
    private bool furiaAtivada;

    public Transform Jogador => jogador;
    public float BioTempoParaSumir => bioTempoParaSumir;
    public float BioTempoSumido => bioTempoSumido;
    public float BioDistanciaAoLado => bioDistanciaAoLado;
    public float BioRaioExplosao => bioRaioExplosao;
    public int BioDano => bioDano;
    public float BioRecuperacao => bioRecuperacao;

    private void Start()
    {
        vidaAtual = vidaMaxima;
        bioTimerGlobal = bioIntervaloGlobal;
        AtualizarBarraDeVida();
        TrocarEstado(new BossEstadoIdle(this));
    }

    private void Update()
    {
        AtualizarTimerBiotransporte();
        estadoAtual?.Atualizar();
    }

    public void TrocarEstado(BossEstado novoEstado)
    {
        estadoAtual?.Sair();
        estadoAtual = novoEstado;
        estadoAtual.Entrar();
    }

    public void TocarAnimacao(string nome)
    {
        if (animator == null)
            return;

        animator.Play(nome, 0, 0f);
    }

    public void FicarInvisivel(bool invisivel)
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = !invisivel;

        if (bossCollider != null)
            bossCollider.enabled = !invisivel;
    }

    public void CausarDanoEmArea(Vector2 centro, float raio, int dano)
    {
        Collider2D hit = Physics2D.OverlapCircle(centro, raio, playerLayer);

        if (hit != null && hit.TryGetComponent(out IDamageable alvo))
            alvo.TakeDamage(dano);
    }

    public void DefinirVulneravel(bool valor)
    {
        vulneravel = valor;
    }

    public void TakeDamage(int dano)
    {
        if (soTomaDanoQuandoVulneravel && !vulneravel)
            return;

        vidaAtual -= dano;
        vidaAtual = Mathf.Max(vidaAtual, 0);
        AtualizarBarraDeVida();

        Debug.Log("Boss tomou dano. Vida atual: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            TrocarEstado(new BossEstadoMorto(this));
            return;
        }

        if (!furiaAtivada && vidaAtual <= vidaParaFuria)
            AtivarFuria();
    }

    private void AtualizarTimerBiotransporte()
    {
        if (estadoAtual is BossEstadoBiotransporte || estadoAtual is BossEstadoMorto)
            return;

        bioTimerGlobal -= Time.deltaTime;

        if (bioTimerGlobal > 0f)
            return;

        bioTimerGlobal = bioIntervaloGlobal;
        TrocarEstado(new BossEstadoBiotransporte(this));
    }

    private void AtualizarBarraDeVida()
    {
        if (barraDeVidaImage != null)
            barraDeVidaImage.fillAmount = (float)vidaAtual / vidaMaxima;
    }

    private void AtivarFuria()
    {
        furiaAtivada = true;

        bioIntervaloGlobal *= multiplicadorFuria;
        bioTempoParaSumir *= multiplicadorFuria;
        bioTempoSumido *= multiplicadorFuria;
        bioRecuperacao *= multiplicadorFuria;

        TocarAnimacao("Furia");
        Debug.Log("Boss entrou em furia!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, bioRaioExplosao);
    }
}
