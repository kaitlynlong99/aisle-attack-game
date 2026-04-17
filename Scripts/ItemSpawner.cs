using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject goodItemPrefab;
    public GameObject badItemPrefab;

    public float spawnInterval = 3f; // Time interval between spawns
    public float minimumX = -2f;
    public float maximumX = 2f;
    public float goodItemChance = 0.8f; // Chance to spawn a good item (between 0 and 1)

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void SpawnItem()
    {
        float randomX = Random.Range(minimumX, maximumX); // Generate a random X position within the specified range
        GameObject itemToSpawn;
        if (Random.value < goodItemChance) // Randomly decide whether to spawn a good item or a bad item based on the specified chance
        {
            itemToSpawn = goodItemPrefab; // Spawn a good item
        }
        else
        {
            itemToSpawn = badItemPrefab; // Spawn a bad item
        }
        Vector3 spawnPosition = new Vector3(randomX, 8.5f, 0); // Set the spawn position with the random X and the spawner's Y position
        Instantiate(itemToSpawn, spawnPosition, Quaternion.identity); // Spawn the item at the spawner's position
    }

    public void StartSpawning()
    {
        InvokeRepeating("SpawnItem", spawnInterval, spawnInterval); // Start spawning items at regular intervals
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnItem"); // Stop spawning items by canceling the InvokeRepeating
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
