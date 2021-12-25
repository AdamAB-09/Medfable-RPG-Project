using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(27, 0, 0);
    private float startDelay = 3;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;

    private void Awake()
    {
        // Initialise the Player Controller by locating the player in the scene
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // The obstacles are spawned in every 1.8 seconds and for the first call it will be 2 seconds
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Obstacles are spawned in as long as the player hasn't lost the game    
    private void SpawnObstacle()
    {
        if (!playerControllerScript.gameOver)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
