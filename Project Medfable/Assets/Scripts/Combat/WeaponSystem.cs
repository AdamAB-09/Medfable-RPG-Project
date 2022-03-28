using UnityEngine;

namespace Medfable.Combat
{
    [CreateAssetMenu(fileName = "WeaponSystem", menuName = "Weapons/Create new weapon", order = 0)]
    public class WeaponSystem : ScriptableObject
    {
        [SerializeField]
        private AnimatorOverrideController weaponAnimate = null;
        [SerializeField]
        private GameObject weapon = null;

        public void SpawnWeapon(Animator animator, Transform weaponPos)
        {
            Instantiate(weapon, weaponPos);
            animator.runtimeAnimatorController = weaponAnimate;
        }

    }
}

