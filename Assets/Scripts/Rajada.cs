using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rajada : MonoBehaviour
{
    [SerializeField] float dano = 50;
    [SerializeField] float tempoPraAutoDestruir = 1f;

    void Start()
    {
        Destroy(gameObject, tempoPraAutoDestruir);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SistemaDeVida sistemaDeVida = collision.gameObject.GetComponent<SistemaDeVida>();
            sistemaDeVida.AplicarDano(dano);
        }
    }
}
