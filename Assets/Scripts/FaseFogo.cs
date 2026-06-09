using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FaseFogo : MonoBehaviour
{
    [Header("XP inicial")]
    public int xpinicio = 50;


    void Start()
    {
        SistemaXP xp = FindObjectOfType<SistemaXP>();
        if (xp != null)
            xp.AdicionarXP(xpinicio);



        if (SceneManager.GetActiveScene().name == "Jogo2")
        {
            Debug.Log("A Fase2 iniciou!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
