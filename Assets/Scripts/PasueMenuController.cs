using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuController : MonoBehaviour
{
    public static bool gameIsPause = false; // maybe will use to stop user input
    [SerializeField] GameObject pauseMenu, soundPanel;

    public void Start()
    {
        gameIsPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!soundPanel.active) {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!gameIsPause)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }
            }
        }
        
    }

    void PauseGame() 
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        gameIsPause = true;
        Time.timeScale = 0.0f;  
        pauseMenu.SetActive(true);
    }

    public void ResumeGame() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        gameIsPause = false;
        Time.timeScale = 1.0f;  
        pauseMenu.SetActive(false);
    }

    public void MainMenuBtnClicked() 
    {
        //playBtnSound();
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.PlaySFX("ButtonBeep");
    }
}
