using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Audio
    {
        public string nome;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1f;

        [Range(0.1f, 3f)]
        public float pitch = 1f;

        public bool loop = false;

        [HideInInspector]
        public AudioSource source;
    }

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

    // 🔊 SFX padrão
    public void Play(string nome)
    {
        Audio audio = Array.Find(audios, a => a.nome == nome);

        if (audio == null) return;

        audio.source.Play();
    }

    // ⛔ parar qualquer som específico
    public void Stop(string nome)
    {
        Audio audio = Array.Find(audios, a => a.nome == nome);

        if (audio == null) return;

        audio.source.Stop();
    }

    // 🎵 música (troca automática)
    public void PlayMusic(string nome)
    {
        Audio novaMusica = Array.Find(audios, a => a.nome == nome);

        if (novaMusica == null) return;

        if (musicaAtual == novaMusica) return;

        if (musicaAtual != null)
            musicaAtual.source.Stop();

        musicaAtual = novaMusica;
        musicaAtual.source.Play();
    }

    // 🔇 parar música atual
    public void StopMusic()
    {
        if (musicaAtual != null)
        {
            musicaAtual.source.Stop();
            musicaAtual = null;
        }
    }

    // 🔇 parar tudo
    public void StopAllSounds()
    {
        foreach (Audio audio in audios)
        {
            if (audio.source != null)
                audio.source.Stop();
        }

        musicaAtual = null;
    }

    // 🔊 SFX sem interromper outros
    public void PlaySFX(string nome)
    {
        Audio audio = Array.Find(audios, a => a.nome == nome);

        if (audio == null) return;

        audio.source.PlayOneShot(audio.clip);
    }

    void Start()
    {
        PlayMusic("Menu");
    }

    public void StopCurrentMusic()
    {
        StopMusic();
    }
}