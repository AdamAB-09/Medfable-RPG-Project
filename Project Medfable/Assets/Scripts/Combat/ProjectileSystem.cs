using UnityEngine;

namespace Medfable.Combat
{
    public class ProjectileSystem : MonoBehaviour
    {
        [SerializeField]
        private float projectileSpeed = 2f;
        [SerializeField]
        private Transform combatTarget = null;

        // Projectile faces and hits the entity's central body at a given speed
        public void Update()
        {
            if (combatTarget == null) { return; }

            Vector3 aimLocation = combatTarget.GetComponent<Collider>().bounds.center;
            transform.LookAt(aimLocation);
            transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        }
    }
}
