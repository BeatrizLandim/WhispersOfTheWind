using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 20f; // Velocidade da bala
    public float lifetime = 2f; // Tempo de vida da bala antes de ser destru�da

    private void Start()
    {
        // Destr�i a bala ap�s um certo tempo
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move a bala na dire��o em que ela est� apontando
        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se a colis�o foi com um inimigo
        if (other.CompareTag("Inimigo"))
        {
            // Destr�i o inimigo
            Destroy(other.gameObject);
        }

        // Destr�i a bala
        Destroy(gameObject);
    }
}
