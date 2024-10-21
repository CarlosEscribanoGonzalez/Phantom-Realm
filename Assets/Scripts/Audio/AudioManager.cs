using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] musicSounds, sfxSounds; 
    public AudioSource musicSource, sfxSource;
    public Scrollbar musicScrollbar, sfxScrollbar;
    public bool detected;

    private void Start()
    {
        //PlayMusic("MainTheme");
    }

    private void Update()
    {}

    private void Awake() //Función para mantener la música sonando entre escena y escena
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayMusic(string songName)
    {
        Sound m = Array.Find(musicSounds, x => x.nombre == songName);

        if(m == null)
        {
            Debug.Log("Song not found");
        }
        else
        {
            musicSource.clip = m.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string effectName)
    {
        Sound s = Array.Find(sfxSounds, x => x.nombre == effectName);

        if(s == null)
        {
            Debug.Log("Sound effect not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
            StartCoroutine(PlayerDetected());
        }
    }

    public void MuteMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void MuteSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolumeChange()
    {
        musicSource.volume = musicScrollbar.value;
    }

    public void SFXVolumeChange()
    {
        sfxSource.volume = sfxScrollbar.value;
    }

    public void PauseMusic()
    {
        musicSource.Pause();
        sfxSource.Stop();
    }

    IEnumerator PlayerDetected()
    {
        detected = true;
        yield return new WaitForSeconds(3.0f);
        detected = false;
    }
}
