using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public GameObject mainVCamera;
    public GameObject startCamera;
    public GameObject goalCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchAreaCamera(ArealCameraArea cameraArea,bool ONOFF)
    {
        switch (ONOFF)
        {
            case true:
                if (cameraArea.targetCamera == null) return;
                cameraArea.targetCamera.SetActive(true);
                break;
            case false:
                if (cameraArea.targetCamera == null) return;
                cameraArea.targetCamera.SetActive(false);
                break;
        }
    }
}
