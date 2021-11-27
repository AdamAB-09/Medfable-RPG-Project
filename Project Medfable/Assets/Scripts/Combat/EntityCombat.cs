using Medfable.Core;
using Medfable.Movement;
using System.Collections;
using UnityEngine;

namespace Medfable.Combat
{
    public class EntityCombat : MonoBehaviour, IInteraction
    {
        private Transform target;
        [SerializeField]
        private float attackRange = 2.7f;
        [SerializeField]
        private float attackCooldown = 2f;
        private bool isCooldown = false;

        // Update is called once per frame
        private void Update()
        {
            if (target != null)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                GetComponent<EntityMovement>().MoveToEntity(target.position, attackRange);
                AttackHandler(distance);
            }
        }

        // Controls the attack behaviour of an entity
        private void AttackHandler(float distance)
        {
            if (distance < attackRange && !isCooldown)
            {
                transform.LookAt(target);
                GetComponent<Animator>().SetTrigger("attack");
                StartCoroutine(Cooldown());
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

        // Generates an attack cooldown after the entity performs the combat animation
        private IEnumerator Cooldown()
        {
            isCooldown = true;
            yield return new WaitForSeconds(attackCooldown);
            isCooldown = false;
        }
    }
}
