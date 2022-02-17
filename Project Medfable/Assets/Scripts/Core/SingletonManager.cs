using UnityEngine;

namespace Medfable.Core
{
    public class SingletonManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject singletonPrefab;
        //This bool will keep its value when switching between scenes
        public static bool hasSpawnedObj = false;

        //Checks whether the singleton object has been created when a scene is loaded
        private void Awake()
        {
            if (hasSpawnedObj || singletonPrefab == null) { return; }
            SpawnSingletonObject();
            hasSpawnedObj = true;
        }

        //The singleton object will be created once only if it hasn't been created yet
        private void SpawnSingletonObject()
        {
            GameObject singletonObj = Instantiate(singletonPrefab);
            DontDestroyOnLoad(singletonObj);
        }
    }
}
