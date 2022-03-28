using UnityEngine;

namespace Medfable.Combat
{
    [CreateAssetMenu(fileName = "WeaponSystem", menuName = "Weapons/Create new weapon", order = 0)]
    public class WeaponSystem : ScriptableObject
    {
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
                Transform weaponPos;
                if (isLeftHanded)
                {
                    weaponPos = leftWeaponPos;
                }
                else
                {
                    weaponPos = rightWeaponPos;
                }
                Instantiate(weapon, weaponPos);
            }
            if (weaponAnimate != null)
            {
                animator.runtimeAnimatorController = weaponAnimate;
            }
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

    }
}

