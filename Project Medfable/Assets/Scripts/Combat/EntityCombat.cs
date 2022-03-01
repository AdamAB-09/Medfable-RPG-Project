using Medfable.Core;
using Medfable.Movement;
using System.Collections;
using UnityEngine;

namespace Medfable.Combat
{
    public class EntityCombat : MonoBehaviour, IInteraction
    {
        private HealthSystem target;
        [SerializeField]
        private float attackRange = 1.9f;
        [SerializeField]
        private float attackCooldown = 2f;
        private bool isCooldown = false;
        private float weaponDamage = 20f;

        // Update is called once per frame
        private void Update()
        {
            if (target != null && target.IsAlive)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                GetComponent<EntityMovement>().MoveToEntity(target.transform.position, attackRange);
                AttackHandler(distance);
            }
        }

        // Controls the attack behaviour of an entity
        private void AttackHandler(float distance)
        {
            if (distance <= attackRange && !isCooldown)
            {
                transform.LookAt(target.transform);
                GetComponent<Animator>().SetTrigger("attack");
                StartCoroutine(Cooldown());
            }
        }

        // Event for the animator to use - this is called when the entity's animation shows a complete hit
        public void Hit()
        {
            if (target == null) { return; }
            target.GetComponent<HealthSystem>().TakeDamage(weaponDamage);
        }

        // User will start attacking the target
        public void Attack(GameObject combatTarget)
        {
            GetComponent<InteractionScheduler>().StartNewAction(this);
            target = combatTarget.GetComponent<HealthSystem>();
        }

        // Player will stop locking onto enemy and stop attacking
        public void CancelAction()
        {
            target = null;
            GetComponent<EntityMovement>().CancelAction();
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        //Checks whether there is a valid target that is alive in order to be attacked
        public bool CanAttack(CombatTarget target)
        {
            return target && target.GetComponent<HealthSystem>().IsAlive;
        }

        // Generates an attack cooldown after the entity performs the combat animation
        private IEnumerator Cooldown()
        {
            isCooldown = true;
            yield return new WaitForSeconds(attackCooldown);
            isCooldown = false;
        }
    }
}