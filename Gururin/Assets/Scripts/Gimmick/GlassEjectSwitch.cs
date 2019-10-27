using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassEjectSwitch : MonoBehaviour
{

    public GameObject glassBall, glassPos, vCam;
    private CriAtomSource _pushSE;
    private Gamecontroller _gameController;
    private Vector3 _pos;
    private bool _eject;

    private void Start()
    {
        _pushSE = GameObject.Find("SE_item(CriAtomSource)").GetComponent<CriAtomSource>();
        _gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        _pos = transform.position;

        _eject = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _eject == false)
        {
            transform.position = new Vector2(0.0f, 1.1f);
            _eject = true;
            _pushSE.Play();
            StartCoroutine(VCam());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _eject = false;
        }
    }

    IEnumerator VCam()
    {
        //コントローラーの操作を封じる
        _gameController.isCon = true;
        //ガラス玉の位置にカメラを移動
        vCam.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        //ガラス玉をインスタンス
        var glass = Instantiate(glassBall);
        var pos = glassPos.transform.position;
        glass.transform.position = new Vector3(pos.x, pos.y, pos.z);

        yield return new WaitForSeconds(1.5f);

        //コントローラーの操作を許可
        _gameController.isCon = false;
        //カメラを元に戻す
        vCam.SetActive(false);
        transform.position = _pos;

        yield break;
    }
}
