using Medfable.Movement;
using UnityEngine;

namespace Medfable.Combat
{
    public class EntityCombat : MonoBehaviour
    {
        Transform target;
        [SerializeField]
        float weaponRange = 2.5f;

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
            target = combatTarget.transform;
        }

        // Player will stop locking onto enemy and stop attacking
        public void CancelAttack()
        {
            target = null;
        }
    }
}
