using UnityEngine;

namespace Medfable.Saving
{
    public class SaveManager : MonoBehaviour
    {
        private const string saveFile = "player-save.sav";

        //Load the most recent file into the game whenever user launches the game
        private void Start()
        {
            LoadMode();
        }

        //Checks whether the player is pressing the key to save or load a file
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveMode();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadMode();
            }
        }

        //Allows the user to load the recent file
        public void LoadMode()
        {
            GetComponent<SaveSystem>().Load(saveFile);
        }

        //Allows the user to save to the file
        public void SaveMode()
        {
            GetComponent<SaveSystem>().Save(saveFile);
        }
    }
}
