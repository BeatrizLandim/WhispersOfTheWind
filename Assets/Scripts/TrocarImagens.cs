using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TrocarImagens : MonoBehaviour
{
    public Image imagemUI;

    public Sprite imagem1;
    public Sprite imagem2;

    public float tempoEntreImagens = 10f;
    public float tempoMenu = 30f;

    private bool executando = false;

    void Start()
    {
        StartCoroutine(SequenciaImagens());
    }

    IEnumerator SequenciaImagens()
    {
        if (executando) yield break;
        executando = true;

        // Primeira imagem
        imagemUI.sprite = imagem1;
        imagemUI.rectTransform.sizeDelta = new Vector2(1536, 1024);

        yield return new WaitForSeconds(tempoEntreImagens);

        // Segunda imagem
        imagemUI.sprite = imagem2;
        imagemUI.rectTransform.sizeDelta = new Vector2(1412, 1114);

        yield return new WaitForSeconds(tempoMenu);

        // Garante que o jogo não está pausado
        Time.timeScale = 1f;

        // 🔁 Reinicia a cena atual (reset completo do jogo)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}