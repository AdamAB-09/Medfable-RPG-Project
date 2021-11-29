using Medfable.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Medfable.Movement
{
    public class EntityMovement : MonoBehaviour, IInteraction
    {
        private NavMeshAgent navMeshAgent;

        // Awake is called when script instances are being loaded 
        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        private void Update()
        {
            EntityAnimation();
        }

        /* Allows the entity to move and stop within a fixed range towards another
           entity's position
        */
        public void MoveToEntity(Vector3 entityPosition, float range)
        {
            MovementAction();
            navMeshAgent.stoppingDistance = range;
            navMeshAgent.destination = entityPosition;

            /* Movement action is temporarily turned on at the start of the function when moving to an entity 
               but when in range it's turned back off
            */
            float distance = Vector3.Distance(transform.position, entityPosition);
            if (distance < range)
            {
                CancelAction();
            }
        }

        // Allows the entity to move towards a target destination
        public void MoveTowards(Vector3 targetDest)
        {
            MovementAction();
            GetComponent<InteractionScheduler>().StartNewAction(this);
            navMeshAgent.stoppingDistance = 0;
            navMeshAgent.destination = targetDest;
        }

        // Changes the entity animation relative to the velocity the player moves at
        private void EntityAnimation()
        {
            // Changing global velocity to local for the animator to recongnise 
            Vector3 relativeVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
            float forwardSpeed = Mathf.Abs(relativeVelocity.z);
            GetComponent<Animator>().SetFloat("forwardSpeed", forwardSpeed);
        }

        // Allows the entity to move off
        private void MovementAction()
        {
            navMeshAgent.isStopped = false;
        }

        // Stops the entity from moving
        public void CancelAction()
        {
            navMeshAgent.isStopped = true;
        }
    }
}