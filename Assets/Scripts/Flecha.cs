using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float velocidade = 5f;
    public int dano = 5;

    private float direcao;

    public void DefinirDirecao(float dir)
    {
        direcao = dir;

        transform.localScale = new Vector3(
            dir > 0 ? 1 : -1,
            1,
            1
        );
    }

    void Update()
    {
        transform.Translate(
            Vector2.right * direcao * velocidade * Time.deltaTime
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SistemaDeVida vida =
                other.GetComponent<SistemaDeVida>();

            if (vida != null)
                vida.AplicarDano(dano);

            Destroy(gameObject);
        }
    }
}