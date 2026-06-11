using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SistemaDeVida : MonoBehaviour, IDamageable
{
    public BarraDeVida barraDeVida;
    //public SeguirJogador cameraFollow;

    [Range(0, 100)] public float vidaMaxima = 100f;
    [Range(0, 100)] public float vidaAtual;

    protected Animator animator;
    protected bool estaMorto = false;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();

        vidaAtual = vidaMaxima;
        AtualizarVida();

        if (animator != null)
            animator.SetBool("Vivo", true);
    }

    public virtual void AplicarDano(float dano)
    {
        if (estaMorto) return;

        vidaAtual -= dano;

        // 🔥 evita bug de bloco solto
        if (animator != null)
        {
            AudioManager.Instance.Play("Dano");
            StartCoroutine(AnimacaoMachucado());
        }

        AtualizarVida();

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    public void TakeDamage(int dano)
    {
        AplicarDano(dano);
    }

    private IEnumerator AnimacaoMachucado()
    {
        if (animator != null)
        {
            animator.SetBool("Machucado", true);
            yield return new WaitForSeconds(0.2f);
            animator.SetBool("Machucado", false);
        }
    }

    protected void AtualizarVida()
    {
        if (barraDeVida != null)
            barraDeVida.AtualizarUI(vidaAtual / vidaMaxima);
    }

    protected virtual void Morrer()
    {
        if (estaMorto) return;
        estaMorto = true;

        // 🚨 garante que animação de morte não seja interrompida
        if (animator != null)
        {
            animator.SetBool("Vivo", false);
            animator.SetBool("Machucado", false);
            animator.speed = 1f;
        }

        StartCoroutine(ReiniciarJogoCompleto());
    }

    public IEnumerator ReiniciarJogoCompleto()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<DoubleJump>().enabled = false;
        GetComponent<PlayerCrouch>().enabled = false;

        AudioManager.Instance.StopCurrentMusic();
        AudioManager.Instance.Play("Morte");

        yield return new WaitForSeconds(14.5f);

        // ⚠️ debug antigo removido (estava errado)
        Destroy(AudioManager.Instance.gameObject);

        SceneManager.LoadScene("Creditos", LoadSceneMode.Single);
    }
}
