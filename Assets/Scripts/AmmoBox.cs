using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{

    [SerializeField] private float spawnRotationY = 5.0f;
    [SerializeField] private Transform objectSpawned;
    [SerializeField] private GameObject objectToSpawn;    // Prefab of the object to spawn
    [SerializeField] private Transform fpsController;     // Reference to the FPSController's Transform
    [SerializeField] private float spawnInterval = 5f;    // Time between spawns in seconds
    [SerializeField] private int maxSpawns = 5;       // Maximum number of ammo boxes to spawn

    private float remainingTime;
    private int spawnedCount = 0;   // Counts currently spawned ammo boxes
    private bool isSpawning = false;

    private void Start()
    {
        remainingTime = spawnInterval;
        InvokeRepeating("SpawnObject", spawnInterval, spawnInterval);
        StartSpawning();
    }

    // Update is called once per frame
    private void Update()
    {
        spawnedCount = 0;
        // Rotate each spawned object continuously
        foreach (Transform spawnedObject in FindObjectsOfType<Transform>())
        {
            if (spawnedObject.gameObject.tag == "AmmoPack")
            {
                spawnedObject.Rotate(0f, spawnRotationY * Time.deltaTime, 0f); // Apply continuous rotation
                spawnedCount++;
            }
        }
        if (spawnedCount < maxSpawns && !isSpawning)
        {
            StartSpawning();
        }
    }

    private void StartSpawning()
    {
        isSpawning = true; 
        Invoke("SpawnObject", spawnInterval);  // Spawn one with a delay
    }

    private void SpawnObject()
    {
        if (spawnedCount < maxSpawns)
        {
            // Get a random position further away from the FPSController
            float radius = Random.Range(3f, 10f);
            Vector3 spawnPosition = fpsController.position + Random.onUnitSphere * radius;  // Adjust radius as needed
            spawnPosition.y = objectSpawned.position.y;

            // Instantiate the object at the random position
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            spawnedObject.tag = "AmmoPack"; // Assign a tag for identification

            spawnedCount++;  // Increment spawned count

            // Cancel the repeating Invoke if reached max spawns
            if (spawnedCount < maxSpawns)
            {
                Invoke("SpawnObject", spawnInterval);  // Schedule the next spawn with a delay
            }
            else
            {
                isSpawning = false;  // Stop spawning if reached max
            }
        }
        else
        {
            isSpawning = false;  // Stop spawning if exceeded max
        }
    }

    private void SpawnAmmo()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            // Reset the remaining time
            remainingTime = spawnInterval;

            // Spawn the ammo box
            SpawnObject();
        }
    }

}
