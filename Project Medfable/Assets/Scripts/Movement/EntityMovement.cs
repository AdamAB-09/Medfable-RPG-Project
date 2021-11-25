using Medfable.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace Medfable.Movement
{
    public class EntityMovement : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;

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

        /* Allows the entity to move and stop within its weapon range towards another
           entity's destination
        */
        public void MoveToEntity(Vector3 entityDest, float weaponRange)
        {
            navMeshAgent.destination = entityDest;
            navMeshAgent.stoppingDistance = weaponRange;
        }

        // Allows the entity to move towards a target destination
        public void MoveTowards(Vector3 targetDest)
        {
            navMeshAgent.stoppingDistance = 0;
            GetComponent<EntityCombat>().CancelAttack();
            navMeshAgent.destination = targetDest;
        }

        // Changes the player animation relative to the velocity the player moves at
        private void EntityAnimation()
        {
            // Changing global velocity to local for the animator to recongnise 
            Vector3 relativeVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
            float forwardSpeed = Mathf.Abs(relativeVelocity.z);
            GetComponent<Animator>().SetFloat("forwardSpeed", forwardSpeed);
        }
    }
}

