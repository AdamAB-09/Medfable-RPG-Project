using UnityEngine;

namespace Medfable.Core
{
    public class InteractionScheduler : MonoBehaviour
    {
        private MonoBehaviour currentAction;

        public void StartAction(MonoBehaviour action)
        {
            if (currentAction == action) 
            { 
                return; 
            }
            if (currentAction != null)
            {
                print(action);
            }
            currentAction = action;
        }
    }
}
