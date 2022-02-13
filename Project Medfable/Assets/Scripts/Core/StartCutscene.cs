using UnityEngine.Playables;
using UnityEngine;

namespace Medfable.Core
{
    public class StartCutscene : MonoBehaviour
    {
        private bool hasPlayedCutscene = false;
        
        //Whenever the player collides with the cutscene object for the first time it will display a cinematic
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && !hasPlayedCutscene)
            {
                GetComponent<PlayableDirector>().Play();
                hasPlayedCutscene = true;
            }
        }

    }
}
