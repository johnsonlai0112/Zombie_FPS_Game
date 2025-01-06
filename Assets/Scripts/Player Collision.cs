using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    private GunAnimation gunAnimation;
    private UIManager uiManager;

    [SerializeField] private static int maxHealth = 5;
    [SerializeField] private int currentHealth = 5;
    [SerializeField] private Slider slider;

    private bool isJumped = false;

    private string AmmoPack = "AmmoPack", NormalChaseZombie = "NormalChaseZombie", SmallChaseZombie = "SmallChaseZombie";

    //added by johnson
    public GameObject bloodScreen;
    public static bool isPlayeDie = false;
    public Animator playeDieAnimator;

    void Start()
    {
        gunAnimation = GameObject.FindWithTag("Gun").GetComponent<GunAnimation>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        maxHealth = 5;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        isPlayeDie = false;
    }

    public void Update()
    {
        if (PlayerCollision.isPlayeDie)
        {
            playeDieAnimator.SetBool("isPlayerDie", true);
        }
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if(collisionInfo.gameObject.tag == AmmoPack)
        {
            gunAnimation.IncreaseMaxAmmo();
            Destroy(collisionInfo.gameObject);
            Debug.Log("Ammo Collected");
        }

        if(collisionInfo.gameObject.tag == NormalChaseZombie)
        {
            TakeDamage(1);
            Debug.Log("collide");
        }

        if (collisionInfo.gameObject.tag == SmallChaseZombie)
        {
            TakeDamage(1);
            Debug.Log("collide");
        }

    }

    void TakeDamage(int damage)
    {
            int dmg = Mathf.RoundToInt(damage);
            currentHealth -= dmg;
            SetHealth(currentHealth);
            AudioManager.Instance.PlaySFX("PlayerGetHit");

        // added by johnson
        if (currentHealth >= 0) {
            StartCoroutine(BloodEffect()); 
        }

        if (currentHealth == 0 || Timer.OverEscapeTime)
        {
            isPlayeDie = true;
            playeDieAnimator.SetBool("isPlayerDie", true);
            AudioManager.Instance.PlaySFX("PlayerDeath");
        }
    }

    // added by johnson for the blood fade out
    IEnumerator BloodEffect()
    {
        if (bloodScreen.activeInHierarchy == false)
        {
            bloodScreen.SetActive(true);
        }

        var image = bloodScreen.GetComponentInChildren<Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 4f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.

            if (bloodScreen.activeInHierarchy)
            {
                bloodScreen.SetActive(false);
            }
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.value = health;
        slider.maxValue = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
