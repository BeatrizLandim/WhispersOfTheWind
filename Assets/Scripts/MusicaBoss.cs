using UnityEngine;

public class MusicaBoss : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMusic("Boss");
    }
}