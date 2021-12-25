using Cinemachine;
using UnityEngine;

namespace Medfable.Core
{
    public class CamZoom : MonoBehaviour
    {
        private CinemachineVirtualCamera virtualCam;
        [SerializeField]
        private float zoomValue = 0.3f;
        [SerializeField]
        private float maxZoom = 25f;
        [SerializeField]
        private float minZoom = 5f;

        // Awake is called when script instances are being loaded 
        private void Awake()
        {
            virtualCam = GetComponent<CinemachineVirtualCamera>();
        }

        //Allows the user to zoom in and out of the camera
        public void Zoom()
        {
            CinemachineComponentBase componentBase = virtualCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
            float camDistance = (componentBase as CinemachineFramingTransposer).m_CameraDistance;

            //If the player scrolls down they zoom out until maxZoom value
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && camDistance < maxZoom)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance += zoomValue;
            }
            //If the player scrolls up they zoom in until minZoom value
            else if (Input.GetAxis("Mouse ScrollWheel") < 0 && camDistance > minZoom)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance -= zoomValue;
            }
        }
    }
}
