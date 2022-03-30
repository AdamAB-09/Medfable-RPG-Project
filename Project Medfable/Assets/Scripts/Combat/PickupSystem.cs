using UnityEngine;

namespace Medfable.Combat
{
    public class PickupSystem : MonoBehaviour
    {
        [SerializeField]
        private float restoreHealth = 0f;
        [SerializeField]
        WeaponSystem weapon = null;

        // Allows the player to pickup a weapon or regenerate their health 
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && weapon != null)
            {
                other.GetComponent<EntityCombat>().EquipWeapon(weapon);
                Destroy(gameObject);
            }
            else if (other.gameObject.tag == "Player" && restoreHealth > 0f)
            {
                other.GetComponent<HealthSystem>().Heal(restoreHealth);
                Destroy(gameObject);
            }
        }
    }
}
