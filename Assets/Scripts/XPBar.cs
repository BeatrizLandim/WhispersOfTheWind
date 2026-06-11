using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [Header("Configurações de XP")]
    public int xpAtual = 0;
    public int xpMaximo = 100;

    [Header("Referência da Barra")]
    public Image barraAtual;  // A imagem que enche (Fill)

    [Header("Níveis da Medalha")]
    public bool bronze = true;   // Sempre começa no bronze
    public bool prata = false;
    public bool ouro = false;

    void Start()
    {
        AtualizarBarra();
        VerificarNivel();
    }

    public void GanharXP(int quantidade)
    {
        xpAtual += quantidade;

        // Impede a barra de ultrapassar 100%
        if (xpAtual > xpMaximo)
            xpAtual = xpMaximo;

        AtualizarBarra();
        VerificarNivel();
    }

    public void AtualizarBarra()
    {
        float porcentagem = (float)xpAtual / xpMaximo;
        barraAtual.fillAmount = porcentagem;
    }

    private void VerificarNivel()
    {
        float porcentagem = (float)xpAtual / xpMaximo;

        if (porcentagem >= 0.66f)
        {
            bronze = true;
            prata = true;
            ouro = true;
        }
        else if (porcentagem >= 0.33f)
        {
            bronze = true;
            prata = true;
            ouro = false;
        }
        else
        {
            bronze = true;
            prata = false;
            ouro = false;
        }
    }
}
