using UnityEngine;

namespace Medfable.Core
{
    public class InteractionScheduler : MonoBehaviour
    {
        private IInteraction currentAction;

        // Schedule a new action and cancel the previous action
        public void StartNewAction(IInteraction action)
        {
            if (currentAction == action) { 
                return; 
            }
            else if (currentAction != null)
            {
                currentAction.CancelAction();
            }
            currentAction = action;
        }
    }
}
