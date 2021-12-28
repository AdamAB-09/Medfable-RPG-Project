using Medfable.Combat;
using UnityEngine;

namespace Medfable.Controller
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float chaseRadius = 5f;
        private EntityCombat enemy;
        private GameObject player;

        // Start is called before the first frame update
        private void Start()
        {
            enemy = GetComponent<EntityCombat>();
            player = GameObject.FindWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            // Locates the player in the scene and calculates the distacne between enemy and player
            float distance = Vector3.Distance(player.transform.position, transform.position);
            
            if (distance <= chaseRadius)
            {
                enemy.Attack(player);
            }
            else
            {
                enemy.CancelAction();
            }
        }
    }
}
