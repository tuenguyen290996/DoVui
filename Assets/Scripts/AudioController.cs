using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Ins;

    [Range(0, 1)]
    public float musicVolume;
    [Range(0, 1)]
    public float soundVolume;

    public AudioSource musicAus;
    public AudioSource soundAus;

    public AudioClip[] backgroundMusic;
    public AudioClip rightSound;
    public AudioClip loseSound;
    public AudioClip winSound;

    private void Awake()
    {
        MakeSingleton();
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayBackgoundMusic();
    }

    void Update()
    {
        if(musicAus && soundAus)
        {
            musicAus.volume = musicVolume;
            soundAus.volume = soundVolume;
        }
    }

    public void PlayBackgoundMusic()
    {
        if(musicAus && backgroundMusic !=null && backgroundMusic.Length > 0)
        {
            int randIdx = Random.Range(0, backgroundMusic.Length);

            if (backgroundMusic[randIdx])
            {
                musicAus.clip = backgroundMusic[randIdx];
                musicAus.volume = musicVolume;
                musicAus.Play();
            }
        }
    }

    public void PlaySound(AudioClip sound)
    {
        if(soundAus && sound)
        {
            soundAus.volume = soundVolume;
            soundAus.PlayOneShot(sound);
        }
    }

    public void PlayRightSound()
    {
        
        PlaySound(rightSound);
    }

    public void PlayLoseSound()
    {
        PlaySound(loseSound);
    }

    public void PlayWinSound()
    {
        PlaySound(winSound);
    }

    public void StopMusic()
    {
        if (musicAus)
        {
            musicAus.Stop();
        }
    }
    
    void MakeSingleton()
    {
        if(Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
