using UnityEngine.Playables;
using UnityEngine;
using Medfable.Core;
using Medfable.Controller;

namespace Medfable.SceneManagement
{
    public class CutsceneManager : MonoBehaviour
    {
        private static bool hasPlayedCutscene = false;
        private GameObject player;

        //Start is called before the first frame update
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisablePlayer;
            GetComponent<PlayableDirector>().stopped += EnablePlayer;
            player = GameObject.FindWithTag("Player");
        }

        //Gives control back to the player after the cinematic scene ends
        private void EnablePlayer(PlayableDirector playerDir)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

        //Disables control of the player whenever a cinematic scene is playing
        private void DisablePlayer(PlayableDirector playerDir)
        {
            player.GetComponent<InteractionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        /*Whenever the player collides with the cutscene object for the first time it will display
        * a cinematic scene
        */
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
