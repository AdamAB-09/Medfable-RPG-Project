using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private Toggle fullscreenButton;

    // Start is called before the first frame update
    void Start()
    {
        CheckForFullscreen();
    }

    // Toogles the fullscreen button to the user's current settings
    private void CheckForFullscreen()
    {
        fullscreenButton.isOn = Screen.fullScreen;
    }

    //Apply fullscreen when toggle button is pressed and window size when off
    public void ChangeFullscreen()
    {
        Screen.fullScreen = fullscreenButton.isOn;
    }
}
