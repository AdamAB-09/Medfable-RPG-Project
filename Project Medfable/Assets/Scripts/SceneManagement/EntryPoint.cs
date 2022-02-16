using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Medfable.SceneManagement
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private int sceneReference = 1;
        [SerializeField]
        private EntryPointID location;
        [SerializeField]
        private EntryPointID destination;

        //Used to identify each of the entry points uniquely via location and destination
        enum EntryPointID
        {
            NorthTown, EastTown, EastCrossroads, SouthCrossroads
        }

        //Teleports the user to another scene whenever they pass through an entry point
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(SwitchScene());
            }
        }

        //Player loads into a new scene at the corresponding entry point that is connected to this
        private IEnumerator SwitchScene()
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneReference);
            EntryPoint entryDest = GetOtherEntryPoint();
            UpdatePlayer(entryDest);
            Destroy(gameObject);
        }

        //The position and rotation of the player is adjusted to the spawn location of the entry point
        private void UpdatePlayer(EntryPoint entryDest)
        {
            if (entryDest != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                Transform spawnPoint = entryDest.transform.GetChild(0);
                player.GetComponent<NavMeshAgent>().Warp(spawnPoint.position);
                player.transform.position = spawnPoint.position;
                player.transform.rotation = spawnPoint.rotation;
            }
        }

        //Gets the corresponding entry point destination that is referenced from this entry point
        private EntryPoint GetOtherEntryPoint()
        {
            foreach (EntryPoint entryPoint in FindObjectsOfType<EntryPoint>())
            {
                if (entryPoint.location == destination)
                {
                    return entryPoint;
                }
            }
            return null;
        }
    }
}
