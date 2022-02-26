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
                GameObject player = GetPlayer();
                formatter.Serialize(fileStream, new SerializePosition(player.transform.position));
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
                GameObject player = GetPlayer();
                SerializePosition serializedPos = (SerializePosition)formatter.Deserialize(fileStream);
                player.transform.position = serializedPos.GetVector3();
            }
        }

        private GameObject GetPlayer()
        {
            return GameObject.FindWithTag("Player");
        }
    }
}
