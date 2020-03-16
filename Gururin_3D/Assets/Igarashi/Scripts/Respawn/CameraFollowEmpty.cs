using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollowEmpty : MonoBehaviour
{
    [SerializeField] private CameraManager _cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
        {
            var mainCameraCVC = _cameraManager.mainVCamera.GetComponent<CinemachineVirtualCamera>();
            mainCameraCVC.m_Follow = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
