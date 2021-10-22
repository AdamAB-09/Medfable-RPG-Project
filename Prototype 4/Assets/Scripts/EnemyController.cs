using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private GameObject player;
    private Rigidbody enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void FixedUpdate()
    {
        Vector3 aiDirection = (player.transform.position - transform.position).normalized;
        enemyBody.AddForce(aiDirection * speed);
    }
}
