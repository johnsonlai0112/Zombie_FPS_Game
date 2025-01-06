using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] EnemyTypes = new GameObject[2];
    public GameObject Player;
    float randX;
    float randZ;
    Vector3 SpawnPosition;
    public float spawnRate = 2f;
    float nextSpawn = 0.0f;
    static int maxEnemy = 60;
    public static int enemyCount = 0;
    Transform[] child = new Transform[2];
    Transform parent;

    //use for ensure the function is call
    float delayTime = 0.5f;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxEnemy = 60;

        enemyCount = 0;

        parent = gameObject.transform;

        for (int i = 0; i < parent.childCount; i++)
        {
            child[i] = parent.GetChild(i);

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyCount < maxEnemy)
        {
            if (Time.time > nextSpawn)
            {
                //next spawn time
                nextSpawn = Time.time + spawnRate;

                //next spawn position
                randX = Random.Range(child[0].transform.position.x, child[1].transform.position.x);
                randZ = Random.Range(child[0].transform.position.z, child[1].transform.position.z);
                SpawnPosition = new Vector3(randX, -10.81f, randZ);

                //next spawn type
                GameObject randomSpawnType = EnemyTypes[Random.Range(0, EnemyTypes.Length)];

                Instantiate(randomSpawnType, SpawnPosition, Quaternion.identity);

                SetPlayerForEnemy(randomSpawnType);

                enemyCount++;
            }
        }
    }

    //use for make sure the player variable inside the ChasePlayer is passed in 
    void SetPlayerForEnemy(GameObject enemy)
    {
        if (enemy != null)
        {
            ChasePlayer chasePlayer = enemy.GetComponent<ChasePlayer>();
            ZombieAudio zombieAudio = enemy.GetComponent<ZombieAudio>();
            if (chasePlayer != null)
            {
                chasePlayer.SetPlayer(Player);
                zombieAudio.SetPlayer(Player);
            }
        }
    }
}
