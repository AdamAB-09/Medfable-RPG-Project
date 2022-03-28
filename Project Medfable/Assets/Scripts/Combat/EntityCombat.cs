using Medfable.Core;
using Medfable.Movement;
using System.Collections;
using UnityEngine;

namespace Medfable.Combat
{
    public class EntityCombat : MonoBehaviour, IInteraction
    {
        [Header("Attacking attributes")]
        [SerializeField]
        private float attackCooldown = 2f;
        private bool isCooldown = false;

        [Header("Variables for instantiating")]
        private HealthSystem target;
        [SerializeField]
        private Transform rightWeaponPos = null;
        [SerializeField]
        private Transform leftWeaponPos = null;
        [SerializeField]
        private WeaponSystem weapon = null;
        private WeaponSystem currentWeapon = null;


        //Equip weapon for an entity if they have any set to them
        private void Start()
        {
            EquipWeapon(weapon);
        }

        // Update is called once per frame
        private void Update()
        {
            if (target == null) { return; }
            if (CanAttack(target.gameObject))
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                GetComponent<EntityMovement>().MoveToEntity(target.transform.position, currentWeapon.GetAttackRange());
                AttackHandler(distance);
            }
        }

        // Allows the entity to equip the weapon and change animation to it
        public void EquipWeapon(WeaponSystem weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.SpawnWeapon(animator, leftWeaponPos, rightWeaponPos);
        }

        // Controls the attack behaviour of an entity
        private void AttackHandler(float distance)
        {
            if (distance <= currentWeapon.GetAttackRange() && !isCooldown)
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
            target.GetComponent<HealthSystem>().TakeDamage(currentWeapon.GetDamage());
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
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        //Checks whether there is a valid target that is alive in order to be attacked
        public bool CanAttack(GameObject target)
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