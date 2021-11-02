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
        enemyBody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -15)
        {
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        playerCount = FindObjectsOfType<PlayerController>().Length;
        if (playerCount != 0)
        {
            Vector3 aiDirection = (player.transform.position - transform.position).normalized;
            enemyBody.AddForce(aiDirection * speed);
        }
    }
}
