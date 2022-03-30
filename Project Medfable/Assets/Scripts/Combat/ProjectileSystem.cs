using UnityEngine;

namespace Medfable.Combat
{
    public class ProjectileSystem : MonoBehaviour
    {
        [SerializeField]
        private float timeUntilDestroy = 5f;
        [SerializeField]
        private GameObject impactEffect = null;
        [SerializeField]
        private float projectileSpeed = 2f;
        private float projectileDamage = 0f;
        private HealthSystem combatTarget = null;

        // Projectile faces towards the entity's central body but doesn't follow them
        private void Start()
        {
            if (combatTarget == null) { return; }
            transform.LookAt(GetAimLocation());
        }

        // Gets the central point of an entity's body to target
        private Vector3 GetAimLocation()
        {
            return combatTarget.GetComponent<Collider>().bounds.center;
        }

        // Projectile moves forward at a given speed per frame
        public void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
            DestroyImpactEffect();
        }

        // Destroys any impact effect after collision if there is any
        private void DestroyImpactEffect()
        {
            if (GetComponent<ParticleSystem>() == null) { return; }
            if (GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(impactEffect);
            }
        }

        // Whenever the arrows collides with an entity, it will output damage and get removed
        private void OnTriggerEnter(Collider other)
        {
            if (!combatTarget.IsAlive) { return; }
            if (other.GetComponent<HealthSystem>() == combatTarget)
            {
                if (impactEffect != null)
                {
                    Instantiate(impactEffect, GetAimLocation(), transform.rotation);
                }
                combatTarget.TakeDamage(projectileDamage);
                Destroy(gameObject);
            }
        }

        // Sets a combat target for the entity to attack and destroys projectile after set duration
        public void SetCombatTarget(HealthSystem target, float damage)
        {
            projectileDamage = damage;
            combatTarget = target;
            Destroy(gameObject, timeUntilDestroy);
        }
    }
}
