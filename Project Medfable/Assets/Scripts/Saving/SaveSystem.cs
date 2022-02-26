using UnityEngine;

namespace Medfable.Saving
{
    public class SaveSystem : MonoBehaviour
    {

        //Allows the player to save their current progress onto a file
        public void Save(string saveFile)
        {
            print("Currently saving to " + saveFile);
        }

        //Allows the player to load up their most current file
        public void Load(string loadFile)
        {
            print("Loading data from " + loadFile);
        }
    }
}
