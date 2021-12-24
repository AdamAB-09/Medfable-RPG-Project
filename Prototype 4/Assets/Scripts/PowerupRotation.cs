using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to rotate the indicator of the powerup
public class PowerupRotation : MonoBehaviour
{
    Vector3 rotationSpeed = new Vector3(0, 1, 0);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed);   
    }
}
