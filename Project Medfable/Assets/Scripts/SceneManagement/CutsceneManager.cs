using UnityEngine.Playables;
using UnityEngine;
using Medfable.Core;
using Medfable.Controller;
using Medfable.Saving;
using UnityEngine.AI;

namespace Medfable.SceneManagement
{
    public class CutsceneManager : MonoBehaviour, ISavable
    {
        private bool hasPlayedCutscene = false;
        private GameObject player;
        private SaveManager saveManager;

        //Start is called before the first frame update
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisablePlayer;
            GetComponent<PlayableDirector>().stopped += EnablePlayer;
            player = GameObject.FindWithTag("Player");
            saveManager = FindObjectOfType<SaveManager>();
        }

        //Gives control back to the player after the cinematic scene ends and they can save/load
        private void EnablePlayer(PlayableDirector playerDir)
        {
            player.GetComponent<PlayerController>().enabled = true;
            saveManager.enabled = true;
        }

        //Disables control/saving of the player whenever a cinematic scene is playing
        private void DisablePlayer(PlayableDirector playerDir)
        {
            player.GetComponent<InteractionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<NavMeshAgent>().isStopped = true;
            saveManager.enabled = false;
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

        //Checks whether the cutscene has been played when saving 
        public object CatchObjAttributes()
        {
            return hasPlayedCutscene;
        }

        //Loading a file will restore whether the cutscene has already been played or not
        public void RestoreObjAttributes(object obj)
        {
            hasPlayedCutscene = (bool)obj;
        }
    }
}
