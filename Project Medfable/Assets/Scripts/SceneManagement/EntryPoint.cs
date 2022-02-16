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

        private IEnumerator SwitchScene()
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneReference);
            EntryPoint entryDest = GetOtherEntryPoint();
            UpdatePlayer(entryDest);
            Destroy(gameObject);
        }

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

        private EntryPoint GetOtherEntryPoint()
        {
            foreach (EntryPoint entryPoint in FindObjectsOfType<EntryPoint>())
            {
                if (entryPoint.location == destination)
                {
                    print(entryPoint);
                    return entryPoint;
                }
            }
            return null;
        }
    }
}
