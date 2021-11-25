using Medfable.Core;
using Medfable.Movement;
using UnityEngine;

namespace Medfable.Combat
{
    public class EntityCombat : MonoBehaviour
    {
        private Transform target;
        [SerializeField]
        private float weaponRange = 2.5f;

        // Update is called once per frame
        private void Update()
        {
            if (target != null)
            {
                GetComponent<EntityMovement>().MoveToEntity(target.position, weaponRange);
            }
        }

        // User will start attacking the target
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<InteractionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        // Player will stop locking onto enemy and stop attacking
        public void CancelAttack()
        {
            target = null;
        }
    }
}
