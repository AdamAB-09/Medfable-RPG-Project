using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerBody;
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject focalPoint;
    [SerializeField]
    private GameObject powerupIndicator;
    private bool hasPowerup;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        powerupIndicator.transform.position = transform.position;
    }

    private void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerBody.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCountdown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        { 
            float powerupStrength = 10;
            Rigidbody enemyBody = collision.gameObject.GetComponent<Rigidbody>();
            //This gets the opposite vector that points away from the player
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;
            enemyBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            
        }
    }

    IEnumerator PowerupCountdown()
    {
        float countdownTimer = 6;
        yield return new WaitForSeconds(countdownTimer);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

}
