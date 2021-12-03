using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField]
    private float speed = 30;
    private PlayerController playerControllerScript;
    private float leftBound = -15;

    private void Awake()
    {
        // Initialise the Player Controller by locating the player in the scene
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obstacles and background only moves left when player is alive
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        // To reduce overhead, destroy obstacles that move past the player and are present off-screen
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
