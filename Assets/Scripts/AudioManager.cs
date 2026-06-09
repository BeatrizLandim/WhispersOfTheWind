using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Audio[] audios;

    private Audio musicaAtual;

    public static AudioManager Instance;

    void Awake()    
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.loop;
        }
    }

    public void Play(string name)
    {
        Audio audio = Array.Find(audios, audio => audio.name == name);

        if (audio == null) return;

        audio.source.Play();
    }




    public void PlayMusic(string name)
    {
        Audio novaMusica = Array.Find(audios, a => a.name == name);

        if (novaMusica == null)
            return;

        if (musicaAtual == novaMusica)
            return; // já está tocando

        if (musicaAtual != null)
            musicaAtual.source.Stop();

            musicaAtual = novaMusica;
            musicaAtual.source.Play();
    }

    void Start()
    {
        PlayMusic("Menu");
    }



        public void PlaySFX(string name)
    {
        Audio audio = Array.Find(audios, a => a.name == name);

        if (audio == null)
            return;

        audio.source.PlayOneShot(audio.clip);
    }

}
