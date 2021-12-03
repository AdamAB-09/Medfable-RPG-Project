using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField]
    private float speed = 40;

    // Update is called once per frame
    void Update()
    {
        // Moves the animals and projectile in the scene forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
