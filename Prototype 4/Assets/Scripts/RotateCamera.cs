using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;


    // Update is called once per frame
    void Update()
    {
        // Allows the camera to rotate left and right on the user's key input
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.down, horizontalInput * Time.deltaTime * rotationSpeed);
    }
}
