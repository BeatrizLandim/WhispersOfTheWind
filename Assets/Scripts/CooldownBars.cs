using UnityEngine;
using UnityEngine.UI;

public class CooldownBars : MonoBehaviour
{
    [Header("Referências")]
    public Image barraAtaque2;
    public Image barraAtaque3;

    [Header("Cooldowns")]
    public float cooldownAtaque2 = 3f;
    public float cooldownAtaque3 = 5f;

    private float timer2;
    private float timer3;

    private bool cooldown2;
    private bool cooldown3;

    void Start()
    {
        barraAtaque2.fillAmount = 1;
        barraAtaque3.fillAmount = 1;

        barraAtaque2.color = Color.white;
        barraAtaque3.color = Color.white;
    }

    void Update()
    {
        AtualizarAtaque2();
        AtualizarAtaque3();
    }

    // ===== ATAQUE 1 =====
    public void UsarAtaque2()
    {
        if (cooldown2) return;

        cooldown2 = true;
        timer2 = 0;

        barraAtaque2.color = Color.gray;
        barraAtaque2.fillAmount = 0;
    }

    void AtualizarAtaque2()
    {
        if (!cooldown2) return;

        timer2 += Time.deltaTime;

        float progresso = timer2 / cooldownAtaque2;
        barraAtaque2.fillAmount = progresso;

        if (timer2 >= cooldownAtaque2)
        {
            cooldown2 = false;
            barraAtaque2.fillAmount = 1;
            barraAtaque2.color = Color.white;
        }
    }

    // ===== ATAQUE 2 =====
    public void UsarAtaque3()
    {
        if (cooldown3) return;

        cooldown3 = true;
        timer3 = 0;

        barraAtaque3.color = Color.gray;
        barraAtaque3.fillAmount = 0;
    }

    void AtualizarAtaque3()
    {
        if (!cooldown3) return;

        timer3 += Time.deltaTime;

        float progresso = timer3 / cooldownAtaque3;
        barraAtaque3.fillAmount = progresso;

        if (timer3 >= cooldownAtaque3)
        {
            cooldown3 = false;
            barraAtaque3.fillAmount = 1;
            barraAtaque3.color = Color.white;
        }
    }
}