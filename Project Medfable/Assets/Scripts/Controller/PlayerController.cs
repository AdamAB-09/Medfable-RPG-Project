using Medfable.Combat;
using Medfable.Movement;
using UnityEngine;

namespace Medfable.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private HealthSystem health;

        //Awake is called when script instances are being loaded
        private void Awake()
        {
            health = GetComponent<HealthSystem>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!health.IsAlive) { return; }
            if (PlayerCombat()) { return; }
            PlayerMovement();
        }

        // Allows the player to engage in combat with targets in the environment 
        private bool PlayerCombat()
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
                        GetComponent<EntityCombat>().Attack(target.gameObject);
                        return true;
                    }
                }
            }
            return false;
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

        // Returns the point of the ray where the player clicks on the screen
        private Ray GetCameraRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
