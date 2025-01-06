using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private string levelToLoad;
    public GameObject optionPanel, tutorialPanel;

    public void StartBtnClicked()
    {
        PauseMenuController.gameIsPause = false;
        Time.timeScale = 1.0f;

        levelToLoad = "CutScene";
        AudioManager.Instance.PlaySFX("ButtonBeep");
        //audioManager.PlaySFX("ButtonBeep");   
        SceneManager.LoadScene(levelToLoad);
    }

    public void QuitBtnClicked()
    {
        levelToLoad = "";
        AudioManager.Instance.PlaySFX("ButtonBeep");
        Application.Quit();
    }

    public void OptionBtnClicked()
    {
        optionPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("ButtonBeep");
    }

    public void OptionQuitBtnClicked()
    {
        optionPanel.SetActive(false);
        AudioManager.Instance.PlaySFX("ButtonBeep");    
    }

    public void RestartBtn() {
        levelToLoad = "Main";
        SceneManager.LoadScene(levelToLoad);
        AudioManager.Instance.PlaySFX("ButtonBeep");
    }

    public void TutorialBtnClicked() {
        tutorialPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("ButtonBeep");
    }
    public void TutorialBtnExit()
    {
        tutorialPanel.SetActive(false);
        AudioManager.Instance.PlaySFX("ButtonBeep");
    }
}
