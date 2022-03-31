using Medfable.Combat;
using UnityEngine;

namespace Medfable
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private RectTransform remainingHealthDisplay;
        [SerializeField]
        private HealthSystem healthSystem;
        private GameObject player;

        // Locates the player at the start of the game
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        // Updates the display to the current entity's health and faces it towards the camera
        void Update()
        {
            float currentHealth = healthSystem.GetHealth();
            float maxHealth = healthSystem.GetMaxHealth();
            /* The health display will only show for enemies when they're not dead or at max health,
             * for the player it shows at all times
            */
            if ((healthSystem.gameObject != player) && (currentHealth <= 0 || currentHealth == maxHealth))
            {
                canvas.enabled = false;
                return;
            }

            canvas.enabled = true;
            remainingHealthDisplay.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
            transform.forward = Camera.main.transform.forward;
        }
    }
}
