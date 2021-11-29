using UnityEngine;

namespace Medfable.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField]
        float health = 100f;

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
