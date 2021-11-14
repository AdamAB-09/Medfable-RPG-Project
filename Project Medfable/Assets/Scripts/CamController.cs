using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField]
    private float zoomValue = 0.3f;
    [SerializeField]
    private float maxZoom = 25f;
    [SerializeField]
    private float minZoom = 5f;

    private CinemachineVirtualCamera virtualCam;

    private void Awake()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            CamZoom();
        }
    }

    //Allows the user to zoom in and out of the camera
    private void CamZoom()
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
