using UnityEngine;

namespace Medfable.Combat
{
    public class ProjectileSystem : MonoBehaviour
    {
        [SerializeField]
        private float projectileSpeed = 2f;
        private float projectileDamage = 0f;
        private HealthSystem combatTarget = null;

        // Projectile faces and hits the entity's central body at a given speed
        public void Update()
        {
            if (combatTarget == null) { return; }

            Vector3 aimLocation = combatTarget.GetComponent<Collider>().bounds.center;
            transform.LookAt(aimLocation);
            transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<HealthSystem>() == combatTarget)
            {
                combatTarget.TakeDamage(projectileDamage);
                Destroy(gameObject);
            }
        }

        // Sets a combat target for the entity to attack
        public void SetCombatTarget(HealthSystem target, float damage)
        {
            projectileDamage = damage;
            combatTarget = target;
        }
    }
}
