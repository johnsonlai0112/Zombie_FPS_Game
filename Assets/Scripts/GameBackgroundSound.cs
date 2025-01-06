using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameBackgroundSound : MonoBehaviour
{
    private bool audio1Played = false;
    private bool audio2Played = false;

    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.Instance.musicSource.isPlaying) {
            AudioManager.Instance.StopMusic();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //play the audio if time is less than 30
        if (Timer.remainingTime <= 30 && audio1Played == false)
        {
            StartCoroutine(PlaySoundsSequentially());

            audio1Played = true;
        }

        //play the audio if time is less than 0 means 60 second is over
        else if (Timer.remainingTime <= 0 && audio2Played == false)
        {
            AudioManager.Instance.PlayMusic("60SecondBgm");
            audio2Played = true;
        }
    }



    IEnumerator PlaySoundsSequentially()
    {
        // Play the first sound
        AudioManager.Instance.PlayMusic("ZombieScream");

        // Wait for the first sound to finish
        yield return new WaitForSeconds(AudioManager.Instance.musicSource.clip.length);

        // Play the second sound
        AudioManager.Instance.PlayMusic("30SecondBgm");
    }
}
