using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwitch : MonoBehaviour
{

    public GameObject hideBlock, appearBlock, vCam;
    public bool blocking;
    //カメラのブレンド速度
    public float blendSpeed;
    private CriAtomSource _pushSE, _blockSE;
    private Gamecontroller _gameController;
    private FlagManager _flagManager;

    // Start is called before the first frame update
    void Start()
    {
        blocking = false;
        _pushSE = GameObject.Find("SE_item(CriAtomSource)").GetComponent<CriAtomSource>();
        _blockSE = GetComponent<CriAtomSource>();
        _gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        _flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && blocking == false)
        {
            //ぐるりんの動きを止める
            _flagManager.moveStop = true;
            transform.position = new Vector2(0.0f, -0.5f);
            _pushSE.Play();
            blocking = true;
            StartCoroutine(VCam());
        }
    }

    IEnumerator VCam()
    {
        //コントローラーの操作を封じる
        _gameController.isCon = true;
        //ブロックの位置にカメラを移動
        vCam.SetActive(true);

        yield return new WaitForSeconds(blendSpeed);

        //ブロックを消す
        //hideBlock.transform.position = new Vector3(100, 0);
        hideBlock.SetActive(false);
        _blockSE.Play();
        /*
        if(fan != null)
        {
            fan.SetActive(false);
        }
        */
        if (appearBlock != null)
        {
            appearBlock.SetActive(true);
        }

        yield return new WaitForSeconds(blendSpeed);

        //ぐるりんの動きを許可
        _flagManager.moveStop = false;
        //コントローラーの操作を許可
        _gameController.isCon = false;
        //カメラを元に戻す
        vCam.SetActive(false);
        gameObject.SetActive(false);

        yield break;
    }
}
