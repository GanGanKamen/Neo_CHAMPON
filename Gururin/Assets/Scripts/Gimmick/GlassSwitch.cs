using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSwitch : MonoBehaviour
{

    public GameObject door, vCam;
    public bool blocking;
    private CriAtomSource _blockSE;

    private void Start()
    {
        _blockSE = GetComponent<CriAtomSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject)
        {
            blocking = true;
            //StartCoroutine(VCam());
            door.SetActive(false);
            _blockSE.Play();
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }

    /*
    IEnumerator VCam()
    {
        //ブロックの位置にカメラを移動
        vCam.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        door.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        //カメラを元に戻す
        vCam.SetActive(false);

        yield break;
    }
    */
}
