using Medfable.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Medfable.Saving
{
    public class SaveManager : MonoBehaviour
    {
        private const string saveFile = "player-save.sav";

        // Load the most recent scene data at the start of the game while fading out/in
        private IEnumerator Start()
        {
            FadeEffect fade = FindObjectOfType<FadeEffect>();
            yield return fade.FadeOutInstant();
            yield return GetComponent<SaveSystem>().LoadLastScene(saveFile);
            fade.EnableFade();
        }

        // Allows the user to load the recent file
        public void LoadMode()
        {
            GetComponent<SaveSystem>().Load(saveFile);
        }

        // Allows the user to save to the file
        public void SaveMode()
        {
            GetComponent<SaveSystem>().Save(saveFile);
        }

        // Allows the user to delete their save
        public void DeleteSaveMode()
        {
            GetComponent<SaveSystem>().DeleteSave(saveFile);
        }
    }
}
