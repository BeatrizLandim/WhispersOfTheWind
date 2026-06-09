using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AreaDanca : MonoBehaviour
{
    private Coroutine trocaCenaCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Animator anim = other.GetComponent<Animator>();

            if (anim != null)
            {
                anim.SetBool("Dance", true);

                string cenaAtual = SceneManager.GetActiveScene().name;

                if (cenaAtual == "Jogo1" || cenaAtual == "Jogo2")
                {
                    trocaCenaCoroutine = StartCoroutine(TrocarCena());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Animator anim = other.GetComponent<Animator>();

            if (anim != null)
            {
                anim.SetBool("Dance", false);
            }

            if (trocaCenaCoroutine != null)
            {
                StopCoroutine(trocaCenaCoroutine);
                trocaCenaCoroutine = null;
            }
        }
    }

    private IEnumerator TrocarCena()
    {
        yield return new WaitForSeconds(10f);

        string cenaAtual = SceneManager.GetActiveScene().name;

        switch (cenaAtual)
        {
            case "Jogo1":
                SceneManager.LoadScene("Jogo2");
                break;

            case "Jogo2":
                SceneManager.LoadScene("BossFinal");
                break;
        }
    }
}