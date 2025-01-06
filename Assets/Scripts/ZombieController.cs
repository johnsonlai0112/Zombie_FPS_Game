using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private float destroyZombieDelay = 10.0f;
    private string smallZombieTag = "SmallChaseZombie";
    private string normalZombieTag = "NormalChaseZombie";
    private bool speedIncreased = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //increase the speed after 30 second
        if (Timer.remainingTime <= 30 && speedIncreased == false)
        {
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed *= 1.5f;

            speedIncreased = true;
        }

        //destroy the zombie game object
        if (gameObject.CompareTag(normalZombieTag))
        {
            if (gameObject.GetComponent<ZombieAnimation>().getGetHitCount() == 3)
            {
                gameObject.GetComponent<ChasePlayer>().enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Invoke("DestroyZombie", destroyZombieDelay);
                ZombieSpawner.enemyCount--;
            }
        }
        else if (gameObject.CompareTag(smallZombieTag))
        {
            if (gameObject.GetComponent<ZombieAnimation>().getGetHitCount() == 2)
            {
                gameObject.GetComponent<ChasePlayer>().enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Invoke("DestroyZombie", destroyZombieDelay);
                ZombieSpawner.enemyCount--;
            }
        }
    }

    void DestroyZombie()
    {
        Destroy(gameObject);
    }
}
