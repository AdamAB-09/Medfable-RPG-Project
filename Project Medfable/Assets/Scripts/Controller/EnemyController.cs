using Medfable.Combat;
using Medfable.Core;
using Medfable.Movement;
using UnityEngine;

namespace Medfable.Controller
{
    public class EnemyController : MonoBehaviour
    {
        [Header("AI mechanisms")]
        [SerializeField]
        private float chaseRadius = 5f;
        [SerializeField]
        private float searchTime = 5f;
        private float playerLastSpotted = Mathf.Infinity;

        [Header("Variables for intalising")]
        private EntityCombat enemy;
        private GameObject player;
        private Vector3 startingLocation;
        private EntityMovement movement;
        private HealthSystem health;


        // Initalise all the variables from the first frame
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
            
            /* Checks if enemy is within distance of the player and chases them otherwise they will
             * either search for the player or return to their original position
            */
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= chaseRadius)
            {
                AttackPlayer();
            }
            else if (playerLastSpotted < searchTime)
            {
                SearchForPlayer();
            }
            else
            {
                GuardingPosition();
            }
            playerLastSpotted += Time.deltaTime;
        }

        // Enemy returns to their original position where they were guarding
        private void GuardingPosition()
        {
            if (transform.position == startingLocation) { return; }
            movement.MoveTowards(startingLocation);
        }

        // When enemy loses track of the player they will halt and search
        private void SearchForPlayer()
        {
            GetComponent<InteractionScheduler>().CancelCurrentAction();
        }

        // Move towards the player and attack them
        private void AttackPlayer()
        {
            enemy.Attack(player);
            playerLastSpotted = 0f;
        }

        // Unity calls this method to draw on screen any gizmos
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseRadius);
        }
    }
}
