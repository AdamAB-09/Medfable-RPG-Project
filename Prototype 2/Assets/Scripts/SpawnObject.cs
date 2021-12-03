using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    public GameObject[] animalPrefabs;
    private float spawnRangeX = 15;
    private float spawnPosZ = 20;

    // Start is called before the first frame update
    void Start()
    {
        /* Invokes the methoding for spawning animals in 2 seconds, then after first invokve it 
         * will be at 1.5 seconds
        */
        InvokeRepeating("SpawnRandomAnimal", 2, 1.5f);
    }

    // Method for placing the animals in the scene at random distances
    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }
}
