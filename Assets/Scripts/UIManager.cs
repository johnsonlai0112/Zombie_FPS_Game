using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI outOfAmmo;
    [SerializeField] private TextMeshProUGUI outOfMaxAmmo;

    // Duration of text display in seconds
    [SerializeField] private float displayTime = 2f;

    void Start()
    {
        UpdateAmmo(12, 36);
    }

    public void UpdateAmmo(int count, int max)
    {
        ammoText.text = "Ammo: " + count + "/ " + max;
    }

    public void OutOfAmmo()
    {
        StartCoroutine(DisplayOutOfAmmoText());
    }

    IEnumerator DisplayOutOfAmmoText()
    {
        // Display the text for a certain interval of time
        outOfAmmo.text = "Out of Ammo! Reload !";
        outOfAmmo.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        outOfAmmo.gameObject.SetActive(false);
    }

    public void OutOfMaxAmmo()
    {
        StartCoroutine(DisplayOutOfMaxAmmoText());
    }

    IEnumerator DisplayOutOfMaxAmmoText()
    {
        // Display the text for a certain interval of time
        outOfMaxAmmo.text = "Pick Up Ammo Pack !";
        outOfMaxAmmo.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        outOfMaxAmmo.gameObject.SetActive(false);
    }


}
