using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// VCamの切り替え制御
/// </summary>

public class CameraChange : MonoBehaviour
{

    public GameObject[] vCam;
    //オブジェクトの表示状態
    public bool state;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            vCam[0].SetActive(false);
            vCam[1].SetActive(true);

            if (state)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
