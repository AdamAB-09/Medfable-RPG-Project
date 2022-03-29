using Medfable.Core;
using Medfable.Saving;
using UnityEngine;
using UnityEngine.Events;

namespace Medfable.Combat
{
    public class HealthSystem : MonoBehaviour, ISavable
    {
        private bool isAlive = true;
        [SerializeField]
        float health = 100f;
        [SerializeField]
        private UnityEvent damageEvent;
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

        /* Whenever an entity takes damage reduce health and trigger event, until health is 0 or 
         * below then the entity is removed from the scene
        */
        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
            else if (damageEvent != null)
            {
                damageEvent.Invoke();
            }
        }

        // Uses the death animation whenever an entity loses all their health
        private void Die()
        {
            if (!isAlive) { return; }
            isAlive = false;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<InteractionScheduler>().CancelCurrentAction();
        }

        //Stores the health of the entity when saving
        public object CatchObjAttributes()
        {
            return health;
        }

        //Loads the the most recent health of the entity
        public void RestoreObjAttributes(object obj)
        {
            health = (float)obj;
            
            /*The entity is dead if its health is below 0 however, if an entity died and the user loads
            * a save with the entity alive then their isAlive variable needs to be reset to true and animations
            */
            if (health <= 0)
            {
                Die();
            }
            else if (isAlive == false)
            {
                isAlive = true;
                GetComponent<Animator>().Rebind();
                GetComponent<Animator>().Update(0f);
            }
        }
    }
}
