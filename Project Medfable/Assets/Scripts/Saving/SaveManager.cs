using UnityEngine;

namespace Medfable.Saving
{
    public class SaveManager : MonoBehaviour
    {
        private const string saveFile = "test-file";

        //Checks whether the player is pressing the key to save or load a file
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<SaveSystem>().Save(saveFile);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<SaveSystem>().Load(saveFile);
            }
        }
    }
}
