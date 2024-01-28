using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public AudioSource backgroundMusic;

    public AudioSource soundFX;

    public AudioClip bgMusic,drinking, throwGlass, cork, playerDmg, eDmg, eDeath, capZone, drunkMet, eAtk;

    private void Start()
    {

    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.clip = bgMusic;
            backgroundMusic.Play();
        }
    }

    public static void PlaySound()
    {
        GameObject soundGameObj = new GameObject("Sound");
        AudioSource audioSource = soundGameObj.AddComponent<AudioSource>();
    }
}
