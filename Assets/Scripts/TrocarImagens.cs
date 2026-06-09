using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TrocarImagens : MonoBehaviour
{
    public Image imagemUI;

    public Sprite imagem1;
    public Sprite imagem2;
    public Sprite imagem3;

    public float tempoEntreImagens = 10f;
    public float tempoMenu = 60f;

    void Start()
    {
        StartCoroutine(SequenciaImagens());
    }

    IEnumerator SequenciaImagens()
    {
        imagemUI.sprite = imagem1;
        yield return new WaitForSeconds(tempoEntreImagens);

        imagemUI.sprite = imagem2;
        yield return new WaitForSeconds(tempoEntreImagens);

        imagemUI.sprite = imagem3;
        yield return new WaitForSeconds(tempoMenu);

        SceneManager.LoadScene("Menu");
    }
}