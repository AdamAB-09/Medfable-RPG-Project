using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimation();

        //Checks whether the left button which initaties player movement
        if (Input.GetMouseButtonDown(0))
        {
            PlayerMovement();
        }
    }

    // Changes the player animation relative to the velocity the player moves at
    private void PlayerAnimation()
    {
        // Changing global velocity to local for the animator to recongnise 
        Vector3 relativeVelocity = transform.InverseTransformDirection(GetComponent<NavMeshAgent>().velocity);
        float forwardSpeed = Mathf.Abs(relativeVelocity.z);
        GetComponent<Animator>().SetFloat("forwardSpeed", forwardSpeed);
    }

    private void PlayerMovement()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(cameraRay, out hitInfo);

        //Moves the player to where the raycast has collided
        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hitInfo.point;
        }

    }

}
