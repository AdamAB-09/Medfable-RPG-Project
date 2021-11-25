using Medfable.Core;
using Medfable.Movement;
using UnityEngine;

namespace Medfable.Combat
{
    public class EntityCombat : MonoBehaviour, IInteraction
    {
        private Transform target;
        [SerializeField]
        private float attackRange = 2.7f;

        // Update is called once per frame
        private void Update()
        {
            if (target != null)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                GetComponent<EntityMovement>().MoveToEntity(target.position, attackRange);
                AttackAnimation(distance);
            }
        }

        private void AttackAnimation(float distance)
        {
            if (distance < attackRange)
            {
                GetComponent<Animator>().SetTrigger("attack");
            }
        }

        // User will start attacking the target
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<InteractionScheduler>().StartNewAction(this);
            target = combatTarget.transform;
        }

        // Player will stop locking onto enemy and stop attacking
        public void CancelAction()
        {
            target = null;
        }

        // Event for the animator to use
        public void Hit()
        { }
    }
}
