using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WinCondition : MonoBehaviour
{
    public string playerTag = "Player";
    public float escapeTime = 5;
    public TMP_Text countDownDisplay;
    public static bool escape = false;
    public static bool insideEscapeZone = false;
    Coroutine countdown = null;

    private void Start()
    {
        countDownDisplay.gameObject.SetActive(false);
        escape = false;
        insideEscapeZone = false;
    }

    IEnumerator EscapeCountDown() 
    {
        insideEscapeZone = true;
        while (escapeTime >= 0) {
            countDownDisplay.text = escapeTime.ToString();

            yield return new WaitForSeconds(1f);

            escapeTime--;
        }

        //win 
        escape = true;
        countDownDisplay.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag && !escape)
        {
            countDownDisplay.gameObject.SetActive(true);
            countdown = StartCoroutine(EscapeCountDown());

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerTag && !escape)
        {
            if (countdown != null) {
                StopCoroutine(countdown);
                insideEscapeZone = false;
                escapeTime = 5.0f;
                countDownDisplay.gameObject.SetActive(false);
            }       
            
        };
        
    }
}
