using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace Medfable.Saving
{
    public class SaveSystem : MonoBehaviour
    {

        //Allows the player to save their current progress onto a file
        public void Save(string saveFile)
        {
            string path = Path.Combine(Application.persistentDataPath, saveFile);
            print("Currently saving to " + path);
            using (FileStream fileStream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, CatchGameState());
            }
        }

        //Allows the player to load up their most current file
        public void Load(string loadFile)
        {
            string path = Path.Combine(Application.persistentDataPath, loadFile);
            print("Loading data from " + path);
            using (FileStream fileStream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                GetGameState(formatter.Deserialize(fileStream));
            }
        }

        //Restores a prior game state by checking all the savable entities in a saved dictionary
        private void GetGameState(object gameState)
        {
            Dictionary<string, object> dictState = (Dictionary<string, object>) gameState;
            foreach (EntitySavable entity in FindObjectsOfType<EntitySavable>())
            {
                entity.RestoreGameObj(dictState[entity.GetGUID()]);
            }
        }

        /*Capture the current game state by getting all the savable entites and adding it to
        * a dictionary
        */
        private object CatchGameState()
        {
            Dictionary<string, object> dictState = new Dictionary<string, object>();
            foreach (EntitySavable entity in FindObjectsOfType<EntitySavable>())
            {
                dictState[entity.GetGUID()] = entity.CatchObjState();
            }
            return dictState;
        }
    }
}
