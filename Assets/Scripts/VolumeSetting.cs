using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    public Image image_SFX, image_Music; 
    public Sprite[] sprite_SFX, sprite_Music;


    public void ToggleMusic() {
        AudioManager.Instance.ToggleMusic();
        AudioManager.Instance.PlaySFX("ButtonBeep");
        if (AudioManager.Instance.musicSource.mute)
        {
            image_Music.sprite = sprite_Music[1];
        }
        else {
            image_Music.sprite = sprite_Music[0];
        }
    }
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
        AudioManager.Instance.PlaySFX("ButtonBeep");
        if (AudioManager.Instance.sfxSource.mute)
        {
            image_SFX.sprite = sprite_SFX[1];
        }
        else
        {
            image_SFX.sprite = sprite_SFX[0];
        }
    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }



}
