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
        //Checks whether the left button which initaties player movement
        if (Input.GetMouseButtonDown(0))
        {
            PlayerMovement();
        }

    }

    private void PlayerMovement()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(cameraRay, out hitInfo);

        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hitInfo.point;
        }

    }

}
