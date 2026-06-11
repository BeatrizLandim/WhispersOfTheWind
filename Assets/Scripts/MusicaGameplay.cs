using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicaGameplay : MonoBehaviour
{

    public GameObject objetoParaAtivar;

    [Header("UI")]
    public Image imagemUI;
    public Sprite imagem1;
    public Sprite imagem2;


    [Header("Tempo")]
    public float tempoImagem1 = 10f;
    public float tempoImagem2 = 10f;

    private bool iniciouGameplay = false;


    // Start is called before the first frame update
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