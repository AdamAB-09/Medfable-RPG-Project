using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Modifiers")]
    public bool gameOver;
    private Animator playerAnim;
    private Rigidbody playerBody;
    public bool isOnGround = true;
    [SerializeField]
    private float jumpForce = 600;
    [SerializeField]
    private float gravityModifier;
    
    [Header("Particle effects")]
    [SerializeField]
    private ParticleSystem explosionParticle;
    [SerializeField]
    private ParticleSystem dirtParticle;
    
    [Header("Audio effects")]
    [SerializeField]
    private AudioClip jumpClip;
    [SerializeField]
    private AudioClip crashClip;
    private AudioSource playerAudio;
    [Range(0, 1)]
    public float volume;
   

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpClip, volume);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game over!");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashClip, volume);
        }
    }
}
