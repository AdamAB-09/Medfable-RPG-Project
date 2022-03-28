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

        public void SpawnWeapon(Animator animator, Transform weaponPos)
        {
            if (weapon != null)
            {
                Instantiate(weapon, weaponPos);
            }
            if (weaponAnimate != null)
            {
                animator.runtimeAnimatorController = weaponAnimate;
            }
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetAttackRange()
        {
            return attackRange;
        }

    }
}

