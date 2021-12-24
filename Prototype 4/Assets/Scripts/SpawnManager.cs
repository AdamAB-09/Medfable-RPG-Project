using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject powerupPrefab;
    private float spawnRange = 8;
    private int playerCount;
    private int enemyCount;
    private int waveCount = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        // At the start of the game it will create wave 1 and create a random spawn for the enemies and powerup
        SpawnEnemy(waveCount);
        Instantiate(powerupPrefab, GenerateSpawnPos(), powerupPrefab.transform.rotation);
    }

    // This method is used to create a random spawn position on the platform for enemies and powerup
    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    // Spawn the corresponding amount of enemies to the wave number
    void SpawnEnemy(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }
    
    // Gives a slight chance of spawning a powerup for the player
    void SpawnPowerup()
    {
        int powerupChance = 1;
        
        //Gives a 1/3 chance of dropping a powerup per wave
        if (Random.Range(0, 3) == powerupChance) 
        {
            Instantiate(powerupPrefab, GenerateSpawnPos(), powerupPrefab.transform.rotation);
        }
        else
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        playerCount = FindObjectsOfType<PlayerController>().Length;

        // Whenever the player pushes all the enemies and remains on the platform, start the next wave of enemies
        if (enemyCount == 0 && playerCount != 0)
        {
            waveCount++;
            SpawnEnemy(waveCount);
            SpawnPowerup();
        }
        // If the player falls of however, it will trigger game over
        else if (playerCount == 0)
        {
            Debug.Log("Game over");
        }
    }

}
