using Medfable.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Medfable.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        private bool isAlive = true;
        [SerializeField]
        float health = 100f;
        [SerializeField]
        float deathCooldown = 3f;
        private GameObject player;

        // Start is called before the first frame update
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        // Getter to check whether an entity is alive from another class
        public bool IsAlive
        {
            get { return isAlive; }
        }

        /* Whenever an entity takes damage reduce health, until health is 0 or below then the
         * entity is removed from the scene
        */
        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Die();
            }
        }

        // Uses the death animation whenever an entity loses all their health
        private void Die()
        {
            if (!isAlive) { return; }
            isAlive = false;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<InteractionScheduler>().CancelCurrentAction();
            
            // Remove dead body from scene only if it's an enemy
            if (gameObject != player)
            {
                StartCoroutine(RemoveDeadBody());
            }
        }

        // Cooldown in which the dead body after they perform the death animation is removed from the game
        private IEnumerator RemoveDeadBody()
        {
            yield return new WaitForSeconds(deathCooldown);
            Destroy(gameObject);
        }
    }
}
