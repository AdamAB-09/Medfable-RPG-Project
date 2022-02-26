using System.IO;
using System.Text;
using UnityEngine;

namespace Medfable.Saving
{
    public class SaveSystem : MonoBehaviour
    {

        //Allows the player to save their current progress onto a file
        public void Save(string saveFile)
        {
            saveFile = Path.Combine(Application.persistentDataPath, saveFile);
            print("Currently saving to " + saveFile);
            FileStream fileStream = File.Open(saveFile, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes("Writing to file!");
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();
        }

        //Allows the player to load up their most current file
        public void Load(string loadFile)
        {
            loadFile = Path.Combine(Application.persistentDataPath, loadFile);
            print("Loading data from " + loadFile);
        }
    }
}
