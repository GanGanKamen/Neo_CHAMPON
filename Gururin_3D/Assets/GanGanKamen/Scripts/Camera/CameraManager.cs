using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public GameObject mainVCamera;
    public GameObject startCamera;
    public GameObject goalCamera;
    public GameObject deadCamera;
    [Header("死亡時のカメラの視野角")] public float deadCameraView;
    [Header("死亡時にカメラが近づく速度 1.0~30.0")] [Range(1.0f, 30.0f)] public float deadCameraZoomInSpeed;

    private GameObject _firstMaincamera;

    // Start is called before the first frame update
    void Start()
    {
        _firstMaincamera = mainVCamera;

        var Gururin = GameObject.FindWithTag("Player");
        var mainCameraCVC = mainVCamera.GetComponent<CinemachineVirtualCamera>();
        mainCameraCVC.m_Follow = Gururin.transform;
        mainCameraCVC.m_LookAt = Gururin.transform;

        CameraInit(goalCamera);
        CameraInit(deadCamera);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // メインカメラ以外のカメラのPriorityを0にする
    public void CameraInit(GameObject VCamera)
    {
        var cameraCVC = VCamera.GetComponent<CinemachineVirtualCamera>();
        cameraCVC.m_Priority = 0;
    }

    // メインカメラ以外のカメラの設定
    public CinemachineVirtualCamera CameraSetting(GameObject VCamera)
    {
        var Gururin = GameObject.FindWithTag("Player");
        var mainCameraCVC = mainVCamera.GetComponent<CinemachineVirtualCamera>();
        var cameraCVC = VCamera.GetComponent<CinemachineVirtualCamera>();
        cameraCVC.m_Follow = Gururin.transform;
        // VCameraのPriorityをmainCameraより1高くする
        cameraCVC.m_Priority = mainCameraCVC.m_Priority + 1;
        cameraCVC.m_Lens.FieldOfView = mainCameraCVC.m_Lens.FieldOfView / 2.0f;

        return cameraCVC;
    }

    public void SwitchAreaCamera(ArealCameraArea cameraArea, bool ONOFF)
    {
        if (cameraArea.targetCamera == null) return;

        switch (ONOFF)
        {
            case true:
                cameraArea.targetCamera.SetActive(true);
                mainVCamera = cameraArea.targetCamera;
                break;

            case false:
                cameraArea.targetCamera.SetActive(false);
                mainVCamera = _firstMaincamera;
                break;
        }
    }
}
