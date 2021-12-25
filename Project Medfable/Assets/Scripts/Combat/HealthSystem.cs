using UnityEngine;

namespace Medfable.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField]
        float health = 100f;

        /* Whenever an entity takes damage reduce health, until health is 0 or below then the
         * entity is removed from the scene
        */
        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
