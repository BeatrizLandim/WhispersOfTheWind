using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusicaGameplay2 : MonoBehaviour
{
    public GameObject objetoParaAtivar;

    
    [Header("UI")]
    public Image imagemUI;
    public Sprite imagem1;
    public Sprite imagem2;
    public Sprite imagem3;

    [Header("Tempo")]
    public float tempoImagem1 = 10f;
    public float tempoImagem2 = 10f;
    public float tempoImagem3 = 10f;

    private bool iniciouGameplay = false;

    void Start()
    {
        AudioManager.Instance.PlayMusic("Gameplay");
        StartCoroutine(IntroSequencia());
    }

    IEnumerator IntroSequencia()
    {
        Time.timeScale = 0f;

        // 🔹 Primeira imagem
        imagemUI.gameObject.SetActive(true);
        imagemUI.sprite = imagem1;

        yield return new WaitForSecondsRealtime(tempoImagem1);

        // 🔹 Segunda imagem
        imagemUI.sprite = imagem2;

        yield return new WaitForSecondsRealtime(tempoImagem2);

        imagemUI.sprite = imagem3;

        yield return new WaitForSecondsRealtime(tempoImagem3);

        // 🔹 Esconde UI
        imagemUI.gameObject.SetActive(false);

        if (objetoParaAtivar != null)
        {
            objetoParaAtivar.SetActive(true);
        }


        Time.timeScale = 1f;


        if (!iniciouGameplay)
        {
            iniciouGameplay = true;

        }
    }
}