using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float timerDuration = 60f;
    public static float currentTime;
    private float elapsedTime;
    public static float remainingTime = 60.0f;
    public static bool finalEscapeTime = false;
    public static bool OverEscapeTime = false;
    public TMP_Text timerText;
    public GameObject aricraft;
    public TMP_Text escapeHint;

    void Start()
    {
        // Set the initial time
        currentTime = Time.time;

        remainingTime = 60.0f;
        finalEscapeTime = false;
        OverEscapeTime = false;
    }

    void FixedUpdate()
    {

        if (!PlayerCollision.isPlayeDie) {
            // Calculate the elapsed time
            elapsedTime = Time.time - currentTime;

            // Calculate remaining time
            remainingTime = timerDuration - elapsedTime;
        }

        // Check if the timer has reached zero
        if (remainingTime <= 0f)
        {
            // Perform actions when the timer reaches zero
            Debug.Log("Timer reached zero!");

            if (!finalEscapeTime)
            {
                // reset the timer for repeated use as escape time
                currentTime = Time.time;
                timerDuration = 30f;

                //final timer, if pass player lose
                finalEscapeTime = true;

                aricraft.gameObject.SetActive(true);
                escapeHint.gameObject.SetActive(true);
            }
            else if (finalEscapeTime)
            {
                //lose
                OverEscapeTime = true;
            }
        }

        // Display the remaining time
        //Debug.Log("Remaining Time: " + remainingTime.ToString("F2") + " seconds");
        if (remainingTime > 0) {
            timerText.text = remainingTime.ToString("N0");
        }
    }

    public float getCurrentTime()
    {
        return currentTime;
    }
}
