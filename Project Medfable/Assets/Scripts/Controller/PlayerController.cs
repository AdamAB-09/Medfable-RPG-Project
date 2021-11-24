using Medfable.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Medfable.Controller
{
    public class PlayerController : MonoBehaviour
    {

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

        // Uses raycasting to move the player where the user clicks on the environment 
        private void PlayerMovement()
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit target;
            bool hasHit = Physics.Raycast(cameraRay, out target);

            //Moves the player to where the raycast has collided by calling EntityMovement script
            if (hasHit)
            {
                GetComponent<EntityMovement>().MoveTowards(target.point);
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
    }
}
