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
    [SerializeField] private WatchBossEvent bossEvent;
    [SerializeField] private int num;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            vCam[0].SetActive(false);
            vCam[1].SetActive(true);

            if(bossEvent != null)
            {
                bossEvent.nextCamera(num, num + 1);
                state = true;
            }

            if (state)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
