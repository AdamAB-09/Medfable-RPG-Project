using System;
using UnityEngine;

namespace Medfable.Combat
{
    [CreateAssetMenu(fileName = "WeaponSystem", menuName = "Weapons/Create new weapon", order = 0)]
    public class WeaponSystem : ScriptableObject
    {
        [Header("Weapon stats")]
        [SerializeField]
        private float weaponDamage = 20f;
        [SerializeField]
        private float attackRange = 1.9f;
        private const string weaponName = "Equipped Weapon";

        [Header("Variables for instantiation")]
        [SerializeField]
        private ProjectileSystem projectile = null;
        [SerializeField]
        private AnimatorOverrideController weaponAnimate = null;
        [SerializeField]
        private GameObject weapon = null;
        [SerializeField]
        private bool isLeftHanded = false;

        // Will spawn the weapon for the the entity to use and equip it in its correct hand if it has any set to it
        public void SpawnWeapon(Animator animator, Transform leftWeaponPos, Transform rightWeaponPos)
        {
            DestroyEquippedWeapon(leftWeaponPos, rightWeaponPos);
            if (weapon != null)
            {
                Transform weaponTransform = GetWeaponTransform(leftWeaponPos, rightWeaponPos);
                GameObject newWeapon = Instantiate(weapon, weaponTransform);
                newWeapon.name = weaponName;
            }

            SetAnimatorController(animator);
        }

        // Readjusts the runtime animator for the weapon the entity is using to perform correct attack animation
        private void SetAnimatorController(Animator animator)
        {
            if (weaponAnimate != null)
            {
                animator.runtimeAnimatorController = weaponAnimate;
            }
        }

        // Checks whether there currently exists a weapon in the entity's hand and destroys it if true
        private void DestroyEquippedWeapon(Transform leftWeaponPos, Transform rightWeaponPos)
        {
            Transform equippedWeapon = leftWeaponPos.Find(weaponName);
            if (equippedWeapon == null)
            {
                equippedWeapon = rightWeaponPos.Find(weaponName);
            }
            if (equippedWeapon == null) { return; }

            Destroy(equippedWeapon.gameObject);
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

