using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private GameObject player;
    private Rigidbody enemyBody;
    private int playerCount;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize all the following components 
        enemyBody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // If the player falls off the platform it will be removed from the scene
        if (transform.position.y < -15)
        {
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        // Checks whether there is a player currently in the scene
        playerCount = FindObjectsOfType<PlayerController>().Length;
        
        // Enemy will only track down the player if there is a player in the scene by adding a force towards the player direction
        if (playerCount != 0)
        {
            Vector3 aiDirection = (player.transform.position - transform.position).normalized;
            enemyBody.AddForce(aiDirection * speed);
        }
    }
}
