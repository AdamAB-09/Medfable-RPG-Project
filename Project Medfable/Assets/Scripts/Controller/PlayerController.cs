using Medfable.Combat;
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
            PlayerCombat();
            PlayerMovement();
        }

        // Allows the player to engage in combat with targets in the environment 
        private void PlayerCombat()
        {
            //Checks whether left mouse button is pressed and gets all the raycast hits
            if (Input.GetMouseButtonDown(0))
            {
                Ray cameraRay = GetCameraRay();
                RaycastHit[] hits = Physics.RaycastAll(cameraRay);

                /* If one of the raycast hits has a CombatTarget script attached then allow
                   player to attack
                */
                foreach (RaycastHit hit in hits)
                {
                    CombatTarget target = hit.collider.GetComponent<CombatTarget>();
                    if (target != null)
                    {
                        GetComponent<EntityCombat>().Attack(target);
                        break;
                    }
                }
            }
        }

        // Uses raycasting to move the player where the user clicks on the environment 
        private void PlayerMovement()
        {
            //Checks whether the left mouse button is pressed which initiates player movement
            if (Input.GetMouseButtonDown(0))
            {
                Ray cameraRay = GetCameraRay();
                RaycastHit target;
                bool hasHit = Physics.Raycast(cameraRay, out target);

                //Moves the player to where the raycast has collided by calling EntityMovement script
                if (hasHit)
                {
                    GetComponent<EntityMovement>().MoveTowards(target.point);
                }
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

        // Returns the point of the ray where the player clicks on the screen
        private Ray GetCameraRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
