using UnityEngine;

namespace Medfable.Combat
{
    public class ProjectileSystem : MonoBehaviour
    {
        [SerializeField]
        private float projectileSpeed = 2f;
        private float projectileDamage = 0f;
        private HealthSystem combatTarget = null;

        // Projectile faces towards the entity's central body but doesn't follow them
        private void Start()
        {
            if (combatTarget == null) { return; }
            Vector3 aimLocation = combatTarget.GetComponent<Collider>().bounds.center;
            transform.LookAt(aimLocation);
        }

        // Projectile moves forward at a given speed per frame
        public void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        }

        // Whenever the arrows collides with an entity, it will output damage and get removed
        private void OnTriggerEnter(Collider other)
        {
            if (!combatTarget.IsAlive) { return; }
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
