using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ぐるりんが侵入したときにGameControllerを非表示にする(操作を受け付けなくする)
/// もしかしたら風接触時限定かもしれないので音を入れるときにクラス名変更するかも
/// </summary>

public class ControllerHidden : MonoBehaviour
{

    private GameObject _balloonCol;
    public GameObject velocityMitigation;

    private FlagManager flagManager;

    // Start is called before the first frame update
    void Start()
    {
        _balloonCol = GameObject.Find("BalloonGimmick");

        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Balloon"))
        {
            flagManager.pressParm = false;
            velocityMitigation.SetActive(true);

            //BallonGImmickのGravitySvaleを-5にする(一時的に強める)
            _balloonCol.GetComponent<Rigidbody2D>().gravityScale = -5.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Balloon"))
        {
            flagManager.pressParm = true;
            velocityMitigation.SetActive(false);

            //BallonGImmickのGravitySvaleを-3にする(元に戻す)
            _balloonCol.GetComponent<Rigidbody2D>().gravityScale = -3.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
