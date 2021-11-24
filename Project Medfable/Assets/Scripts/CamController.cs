using UnityEngine;

public class CamController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // When the scrollwheel is adjusted then CamZoom script is called
        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<CamZoom>().Zoom();
        }
    }
}
