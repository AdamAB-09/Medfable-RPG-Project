using UnityEngine;

namespace Medfable.Core
{

    // Codebase takes inspiration from https://chandler-lane.medium.com/setting-up-combat-architecture-in-unity-996096620ef6
    public class InteractionScheduler : MonoBehaviour
    {
        private IInteraction currentAction;

        /* Schedule a new action and cancel the current action. If the user repeats the same action
           then just return
        */
        public void StartNewAction(IInteraction action)
        {
            if (currentAction == action) { return; }
            else if (currentAction != null)
            {
                currentAction.CancelAction();
            }
            currentAction = action;
        }

        // Cancel the current action of an entity
        public void CancelCurrentAction()
        {
            StartNewAction(null);
        }
    }
}
