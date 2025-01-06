using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ZombieAudio : MonoBehaviour
{
    public GameObject Player;
    private AudioSource audio;
    public AudioClip zombieNormalClip;
    public AudioClip zombieAttack;
    public AudioClip zombieDie;
    public float playInterval = 7f;
    public float attackInterval = 3f; // Add a new variable for attack interval
    public float detectionRange = 5.0f;
    public float detectionAttackRange = 3.0f;

    private string bulletTag = "Bullet";
    private string playerTag = "Player";
    private string smallZombieTag = "SmallChaseZombie";
    private string normalZombieTag = "NormalChaseZombie";

    private bool deathAudioPlayed = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(PlayAudioAtIntervals());
        StartCoroutine(PlayZombieAttackAudioAtIntervals());
    }

    // Play the audio every few seconds (something like loop)
    IEnumerator PlayAudioAtIntervals()
    {
        while (gameObject != null)
        {
            // Wait for a few seconds
            yield return new WaitForSeconds(playInterval);

            // If the player is in range, play the normal audio
            if (Player != null && Vector3.Distance(transform.position, Player.transform.position) <= detectionRange)
            {
                PlayZombieNormalAudio();
            }
        }
    }

    // Play attack audio every few seconds while the player stays inside the collision
    IEnumerator PlayZombieAttackAudioAtIntervals()
    {
        while (gameObject != null)
        {
            // Wait for a few seconds
            yield return new WaitForSeconds(attackInterval);

            // If the player is in range, play the attack audio
            if (Player != null && Vector3.Distance(transform.position, Player.transform.position) <= detectionAttackRange)
            {
                PlayZombieAttackAudio();
            }
        }
    }

    void Update()
    {
        // Dead play sound
        if (gameObject.CompareTag(normalZombieTag))
        {
            if (gameObject.GetComponent<ZombieAnimation>().getGetHitCount() == 3 && deathAudioPlayed == false)
            {
                AudioManager.Instance.PlaySFX(AudioManager.zombieDeath);
                deathAudioPlayed = true;
            }
        }
        else if (gameObject.CompareTag(smallZombieTag))
        {
            if (gameObject.GetComponent<ZombieAnimation>().getGetHitCount() == 2 && deathAudioPlayed == false)
            {
                AudioManager.Instance.PlaySFX(AudioManager.zombieDeath);
                deathAudioPlayed = true;
            }
        }
    }

    // Play normal audio
    void PlayZombieNormalAudio()
    {
        AudioManager.Instance.PlaySFX(AudioManager.zombieNormal);
    }

    // Play attack audio
    void PlayZombieAttackAudio()
    {
        AudioManager.Instance.PlaySFX(AudioManager.zomieAttack);
    }

    public void SetPlayer(GameObject Player)
    {
        this.Player = Player;
    }
}
