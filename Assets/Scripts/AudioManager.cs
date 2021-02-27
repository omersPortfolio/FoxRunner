using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] soundEffects;

    public AudioSource backgroundMusic, levelEndMusic;

    public bool isMuted;


    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();

        soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);

        soundEffects[soundToPlay].Play();
    }

    public void PlayLevelVictory()
    {
        backgroundMusic.Stop();
        levelEndMusic.Play();
    }

    public void Play10(int soundToPlay)
    {
        for (int i = 0; i < 10; i++)
        {
            soundEffects[soundToPlay].Stop();

            soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);

            soundEffects[soundToPlay].Play();
        }
    }

    public void Mute()
    {
        backgroundMusic.mute = !backgroundMusic.mute;
        for (int i = 0; i < soundEffects.Length; i++)
        {
           soundEffects[i].mute = !soundEffects[i].mute;
        }
        levelEndMusic.mute = !levelEndMusic.mute;
    }

   
}
