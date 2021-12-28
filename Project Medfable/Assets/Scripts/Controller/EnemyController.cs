using Medfable.Combat;
using Medfable.Movement;
using UnityEngine;

namespace Medfable.Controller
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float chaseRadius = 5f;
        private EntityCombat enemy;
        private GameObject player;
        private Vector3 startingLocation;
        private EntityMovement movement;
        private HealthSystem health;

        // Start is called before the first frame update
        private void Start()
        {
            health = GetComponent<HealthSystem>();
            startingLocation = transform.position;
            enemy = GetComponent<EntityCombat>();
            player = GameObject.FindWithTag("Player");
            movement = GetComponent<EntityMovement>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!health.IsAlive) { return; }
            
            /* Checks if enemy is within distance of the player and chases them otherwise they return
             * to original position
            */
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= chaseRadius)
            {
                enemy.Attack(player);
            }
            else
            {
                if (transform.position == startingLocation) { return; }
                movement.MoveTowards(startingLocation);
            }
        }

        // Unity calls this method to draw on screen any gizmos
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseRadius);
        }
    }
}
