 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject player, winCamera;
    public Animator aircraftAnimator;
    public GameObject winPanel, losePanel;
    Coroutine ending = null;
    bool restart = false;


    // Start is called before the first frame update
    void Start()
    {
        RestartGame();

    }

    // Update is called once per frame
    void Update()
    {
        if (restart) {
            player.SetActive(true);
        }

        if (WinCondition.escape) {
            ending = StartCoroutine(WinSence());
        }

        if (PlayerCollision.isPlayeDie) {
            ending = StartCoroutine(LoseSence());
        }
    }

    IEnumerator WinSence() {
        player.SetActive(false);
        winCamera.SetActive(true);
        yield return new WaitForSecondsRealtime(1.0f);

        aircraftAnimator.SetBool("EngineStart", true);
        yield return new WaitForSecondsRealtime(5.0f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        winPanel.SetActive(true);

        if (AudioManager.Instance.musicSource != null)
        {
            AudioManager.Instance.StopMusic();
        }
    }

    IEnumerator LoseSence()
    {

        yield return new WaitForSecondsRealtime(5.0f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        losePanel.SetActive(true);

        if (AudioManager.Instance.musicSource != null)
        {
            AudioManager.Instance.StopMusic();
        }

        restart = true;
    }

    public void RestartGame() {
        player.gameObject.SetActive(true);
        winCamera.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        PlayerCollision.isPlayeDie = false;
        PauseMenuController.gameIsPause = false;
        Timer.currentTime = Time.time;
        Timer.remainingTime = 60.0f;
        Timer.finalEscapeTime = false;
        WinCondition.escape = false;
        WinCondition.insideEscapeZone = false;
        ZombieSpawner.enemyCount = 0;

    }
}
