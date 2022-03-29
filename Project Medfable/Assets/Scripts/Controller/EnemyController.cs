using Medfable.Combat;
using Medfable.Core;
using Medfable.Movement;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Medfable.Controller
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Chase player mechanism")]
        [SerializeField]
        private float chaseRadius = 5f;
        [SerializeField]
        private float searchTime = 5f;
        [SerializeField]
        private float chaseSpeed = 4.8f;
        [SerializeField]
        private float aggroCooldown = 3f;
        private float playerLastSpotted = Mathf.Infinity;
        private float lastAttacked = Mathf.Infinity;

        [Header("Patrolling mechanism")]
        [SerializeField]
        private PatrolController patrolRoute;
        [SerializeField]
        private float checkpointRadius = 1f;
        [SerializeField]
        private float dwellTime = 5f;
        [SerializeField]
        private float patrolSpeed = 3.8f;
        private int currentCheckpointIndex = 0;
        private bool isWaitingAtCP = false;

        [Header("Variables for instantiation")]
        private EntityCombat enemy;
        private GameObject player;
        private Vector3 startingLocation;
        private EntityMovement movement;
        private HealthSystem health;
        private NavMeshAgent agent;


        // Instantiate all the variables from the first frame
        private void Start()
        {
            health = GetComponent<HealthSystem>();
            startingLocation = transform.position;
            enemy = GetComponent<EntityCombat>();
            player = GameObject.FindWithTag("Player");
            movement = GetComponent<EntityMovement>();
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!health.IsAlive) { return; }
            
            /* Checks if enemy is currently in danger by the player and chases them otherwise they 
             * will either search for the player or return to their original position
            */
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (IsInDanger(distance) && enemy.CanAttack(player))
            {
                AttackPlayer();
            }
            else if (playerLastSpotted < searchTime)
            {
                SearchForPlayer();
            }
            else
            {
                GuardingBehaviour();
            }
            playerLastSpotted += Time.deltaTime;
            lastAttacked += Time.deltaTime;
        }

        // Used as a damage event to reset the timer when the enemy is getting attacked
        public void GettingAttacked()
        {
            lastAttacked = 0f;
        }

        // The enemy is in danger if the player is within its chase radius or its being currently attacked
        private bool IsInDanger(float distance)
        {
            return distance <= chaseRadius || lastAttacked < aggroCooldown;
        }

        /* Enemy will perform guarding action in which they will either patrol or return to
        *  their original position if they have no route
        */
        private void GuardingBehaviour()
        {
            Vector3 nextPosition = startingLocation;
            agent.speed = patrolSpeed;

            /* When there's a patrolling route the enemy will cycle through all the checkpoints
            *  via their positions in the scene
            */
            if (patrolRoute != null)
            {
                if (AtCheckpoint())
                {
                    currentCheckpointIndex = patrolRoute.GetNextCPIndex(currentCheckpointIndex);
                    StartCoroutine(StartDwellTime());
                }
                nextPosition = patrolRoute.GetCheckpoint(currentCheckpointIndex);
            }
            
            if (!isWaitingAtCP)
            {
                movement.MoveTowards(nextPosition);
            }
        }

        // Checks whether the enemy is near any patrolling checkpoints
        private bool AtCheckpoint()
        {
            float distanceToCheckpoint = Vector3.Distance(transform.position, patrolRoute.GetCheckpoint(currentCheckpointIndex));
            return distanceToCheckpoint <= checkpointRadius;
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
            agent.speed = chaseSpeed;
            playerLastSpotted = 0f;
        }

        // Draw on screen the patrol radius around the enemy
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseRadius);
        }

        // When enemy arrives at checkpoint they will have a waiting time
        private IEnumerator StartDwellTime()
        {
            isWaitingAtCP = true;
            yield return new WaitForSeconds(dwellTime);
            isWaitingAtCP = false;
        }
    }
}
