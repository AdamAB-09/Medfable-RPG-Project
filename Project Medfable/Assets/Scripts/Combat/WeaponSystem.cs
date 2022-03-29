using UnityEngine;

namespace Medfable.Combat
{
    [CreateAssetMenu(fileName = "WeaponSystem", menuName = "Weapons/Create new weapon", order = 0)]
    public class WeaponSystem : ScriptableObject
    {
        [SerializeField]
        private ProjectileSystem projectile = null;
        [SerializeField]
        private float weaponDamage = 20f;
        [SerializeField]
        private float attackRange = 1.9f;
        [SerializeField]
        private AnimatorOverrideController weaponAnimate = null;
        [SerializeField]
        private GameObject weapon = null;
        [SerializeField]
        private bool isLeftHanded = false;

        // Will spawn the weapon for the the entity to use and equip it in its correct hand
        public void SpawnWeapon(Animator animator, Transform leftWeaponPos, Transform rightWeaponPos)
        {
            if (weapon != null)
            {
                Transform weaponTransform = GetWeaponTransform(leftWeaponPos, rightWeaponPos);
                Instantiate(weapon, weaponTransform);
            }
            if (weaponAnimate != null)
            {
                animator.runtimeAnimatorController = weaponAnimate;
            }
        }

        // Gets the weapon position whether it's on the left or right hand
        private Transform GetWeaponTransform(Transform leftWeaponPos, Transform rightWeaponPos)
        {
            Transform weaponPos;
            if (isLeftHanded)
            {
                weaponPos = leftWeaponPos;
            }
            else
            {
                weaponPos = rightWeaponPos;
            }

            return weaponPos;
        }

        // Gets the weapon damage of the current weapon
        public float GetDamage()
        {
            return weaponDamage;
        }

        // Gets the attacking range of the cureent weapon
        public float GetAttackRange()
        {
            return attackRange;
        }

        // Checks whether there is a projectile being utilised
        public bool isUsingProjectile()
        {
            return projectile != null;
        }

        // Launches the projectile to the target with its correct rotation/position
        public void UseProjectile(HealthSystem combatTarget, Transform leftWepPos, Transform rightWepPos)
        {
            Transform weaponTransform = GetWeaponTransform(leftWepPos, rightWepPos);
            ProjectileSystem newProjectile = Instantiate(projectile, weaponTransform.position, Quaternion.identity);
            newProjectile.SetCombatTarget(combatTarget, weaponDamage);
        }
    }
}

