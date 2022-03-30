using Medfable.Core;
using UnityEngine;

namespace Medfable.Controller
{
    public class CamController : MonoBehaviour
    {
        // Checks per frame whether the player is scrolling
        private void Update()
        {
            // When the scrollwheel is adjusted then CamZoom script is called
            if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                GetComponent<CamZoom>().Zoom();
            }
        }
    }
}
