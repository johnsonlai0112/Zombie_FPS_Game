using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class CutSceneController : MonoBehaviour
{
    private float changeTime = 26.6f;
    private string levelToLoad = "Main";


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic("CutSceneBgm");
    }

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        Debug.Log("changeTime = " + changeTime);
        if (changeTime <= 0)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        
    }

    public void SkipButtonClicked()
    {
        PauseMenuController.gameIsPause = false;
        AudioManager.Instance.PlaySFX("ButtonBeep");
        SceneManager.LoadScene(levelToLoad);
    }
}
