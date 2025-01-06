using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // to make it able to access any scene

    public Sound[] musicSound, sfxSound, m_FootstepSounds;
    public AudioSource musicSource, sfxSource;
    // clip name
    public static string 
        //bgm
        menuBgm = "MenuBgm",
        cutSceneBgm = "CutSceneBgm",
        loseBgm = "LoseBgm",
        second_30 = "30SecondBgm",
        second_60 = "60SecondBgm",
        //sfx
        gunFire = "GunFire",
        gunReload = "GunReload",
        zomieAttack = "ZomieAttack",
        zombieDeath = "ZombieDeath",
        zombieNormal = "ZombieNormal",
        zombieScream = "ZombieScream",
        buttonBeep = "ButtonBeep",
        land = "Land"
        ;


    public void Awake()
    {
        // make sure the sound will continue play when change scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        PlayMusic(menuBgm);
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log(name + " Music Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log(name + " Effect Sound Not Found");
        }
        else
        {
            //sfxSource.clip = s.clip;
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayFootStepAudio()
    {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);       
        sfxSource.PlayOneShot(m_FootstepSounds[n].clip);
        // move picked sound to index 0 so it's not picked next time
        //m_FootstepSounds[n] = m_FootstepSounds[0];
        //m_FootstepSounds[0].clip = sfxSource.clip;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }

}
