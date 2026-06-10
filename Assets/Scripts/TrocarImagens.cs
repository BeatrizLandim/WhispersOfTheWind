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
    public float tempoMenu = 60f;

    void Start()
    {
        StartCoroutine(SequenciaImagens());
    }

    IEnumerator SequenciaImagens()
    {
        imagemUI.sprite = imagem1;
        imagemUI.rectTransform.sizeDelta = new Vector2(1536, 1024);
        yield return new WaitForSeconds(tempoEntreImagens);

        imagemUI.sprite = imagem2;
        imagemUI.rectTransform.sizeDelta = new Vector2(1412, 1114);
        yield return new WaitForSeconds(tempoMenu);

        SceneManager.LoadScene("Menu");
    }
}