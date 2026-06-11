using System.Collections;
using UnityEngine;

public class InimigoArqueiro : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public GameObject flechaPrefab;
    public Transform pontoDisparo;

    [Header("Distâncias")]
    public float alcanceLanca = 1.5f;
    public float alcanceFlecha = 8f;

    [Header("Dano")]
    public int danoLanca = 10;

    [Header("Tempo")]
    public float tempoEntreLancas = 1f;
    public float tempoEntreFlechas = 10f;

    private bool atacandoLanca;
    private bool podeAtirar = true;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    void Update()
    {
        if (player == null)
            return;

        VirarParaJogador();

        float distancia = Vector2.Distance(transform.position, player.position);

        // Ataque de lança
        if (distancia <= alcanceLanca)
        {
            if (!atacandoLanca)
                StartCoroutine(AtaqueLanca());
        }

        // Ataque de flecha
        else if (distancia <= alcanceFlecha)
        {
            if (podeAtirar)
                StartCoroutine(AtirarFlecha());
        }
    }

    void VirarParaJogador()
    {
        spriteRenderer.flipX =
            player.position.x < transform.position.x;
    }

    IEnumerator AtaqueLanca()
    {
        atacandoLanca = true;

        anim.SetTrigger("AtaqueLanca");

        SistemaDeVida vida =
            player.GetComponent<SistemaDeVida>();

        if (vida != null)
            vida.AplicarDano(danoLanca);

        yield return new WaitForSeconds(tempoEntreLancas);

        atacandoLanca = false;
    }

    IEnumerator AtirarFlecha()
    {
        podeAtirar = false;

        anim.SetTrigger("Atirar");

        yield return new WaitForSeconds(0.3f);

        GameObject flecha = Instantiate(
            flechaPrefab,
            pontoDisparo.position,
            Quaternion.identity
        );

        Flecha scriptFlecha =
            flecha.GetComponent<Flecha>();

        if (scriptFlecha != null)
        {
            float direcao =
                player.position.x > transform.position.x
                ? 1f
                : -1f;

            scriptFlecha.DefinirDirecao(direcao);
        }

        yield return new WaitForSeconds(tempoEntreFlechas);

        podeAtirar = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            transform.position,
            alcanceLanca
        );

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(
            transform.position,
            alcanceFlecha
        );
    }
}