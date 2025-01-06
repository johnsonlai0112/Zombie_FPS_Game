using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunAnimation : MonoBehaviour
{
    private Animator animator;
    private UIManager uiManager;

    [SerializeField] private AudioSource gunFireAudio;
    [SerializeField] private AudioSource gunReloadAudio;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private float bulletSpeed = 60f;

    // Animation
    [SerializeField] private string fireAnimation = "FirePistol";
    [SerializeField] private string reloadAnimation = "Reload";
    [SerializeField] private string runAnimation = "Run";
    [SerializeField] private string idleAnimation = "Idle";

    // Gun
    private int currentAmmo;
    private int startAmmo = 12;
    private int maxAmmo = 36;

    private bool isRunning = false;     // Track running state

    void Start()
    {
        animator = GetComponent<Animator>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        currentAmmo = startAmmo;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        PlayerCollision.isPlayeDie = false;
        PauseMenuController.gameIsPause = false;
    }

    void Update()
    {
        if (!PlayerCollision.isPlayeDie) {
            // Shoot
            if (!PauseMenuController.gameIsPause)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (currentAmmo != 0 || currentAmmo > 0)
                    {
                        //AimRay();
                        Shoot();
                        Animate(fireAnimation);
                        AudioManager.Instance.PlaySFX(AudioManager.gunFire);
                    }
                    else
                    {
                        uiManager.OutOfAmmo();
                    }
                }

                // Reload
                if (Input.GetKeyDown(KeyCode.R) && animator != null)
                {
                    if (maxAmmo != 0)
                    {
                        Animate(reloadAnimation);
                        AudioManager.Instance.PlaySFX(AudioManager.gunReload);
                        Reload();
                    }
                    else if (maxAmmo == 0)
                    {
                        uiManager.OutOfMaxAmmo();
                    }

                }

                // Check for Shift+W key combination and manage running animation

                if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                {
                    // Animation
                    if (!isRunning)
                    {
                        Animate(runAnimation);
                        isRunning = true;
                    }
                }
                else
                {
                    if (isRunning)
                    {
                        animator.StopPlayback(); // Stop the running animation
                        Animate(idleAnimation);
                        isRunning = false;
                    }
                }
            }
        }
       
       
    }


    public void OnApplicationPause(bool pause)
    {
        Cursor.visible = true;
    }

    public void OnApplicationQuit()
    {
        Cursor.visible = true;
    }

    public void Animate(string animationName)
    {
        animator.Play(animationName, 0, 0.0f);
    }

    public void IncreaseMaxAmmo()
    {
        maxAmmo += 12;
        uiManager.UpdateAmmo(currentAmmo, maxAmmo);
    }

    public void Shoot()
    {
        Debug.Log("Shoot");
        if (currentAmmo > 0 && currentAmmo != 0)
        {
            currentAmmo--;
        }

        uiManager.UpdateAmmo(currentAmmo, maxAmmo);

        Bullet();

    }

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && animator != null)
        {
            currentAmmo = 12;
            maxAmmo -= 12;
        }

        uiManager.UpdateAmmo(currentAmmo, maxAmmo);
    }

    public void Bullet()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector3 aimDir = (mouseWorldPosition - bulletPoint.transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, Quaternion.LookRotation(aimDir, Vector3.forward));
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * bulletSpeed;
    }

    public void AimRay()
    {
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(rayOrigin, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("NormalChaseZombie"))
            {
                Debug.Log("Collision detected with zombie: " + hit.collider.name);
                Destroy(hit.collider.gameObject); // Destroy the zombie when collided with a bullet
            }

            if (hit.collider.CompareTag("SmallChaseZombie"))
            {
                Debug.Log("Collision detected with zombie: " + hit.collider.name);
                Destroy(hit.collider.gameObject); // Destroy the zombie when collided with a bullet
            }

        }
    }


}
