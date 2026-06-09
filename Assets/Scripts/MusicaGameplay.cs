using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaGameplay : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMusic("Gameplay");
    }
}