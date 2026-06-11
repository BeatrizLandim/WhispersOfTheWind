using UnityEngine;
using System.Collections;

public class SeguirJogador : MonoBehaviour
{
    [SerializeField] private Transform jogador;
    [SerializeField] private Vector3 offset;

    private Vector3 shakeOffset;

    void Awake()
    {
        // segurança: evita esquecer de atribuir no Inspector
        if (jogador == null)
        {
            Debug.LogError("Jogador não atribuído na câmera!");
        }
    }

    void LateUpdate()
    {
        if (jogador == null) return;

        transform.position = jogador.position + offset + shakeOffset;
    }

    public void Shake(float duracao, float intensidade)
    {
        StartCoroutine(ShakeCoroutine(duracao, intensidade));
    }

    IEnumerator ShakeCoroutine(float duracao, float intensidade)
    {
        float tempo = 0f;

        while (tempo < duracao)
        {
            float x = Random.Range(-1f, 1f) * intensidade;
            float y = Random.Range(-1f, 1f) * intensidade;

            shakeOffset = new Vector3(x, y, 0);

            tempo += Time.deltaTime;
            yield return null;
        }

        shakeOffset = Vector3.zero;
    }
}