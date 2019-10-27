using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMitigation : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private FlagManager _flagManager;
    private GameObject _balloonCol;

    // Start is called before the first frame update
    void Start()
    {
        _flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        _balloonCol = GameObject.Find("BalloonGimmick");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //風船が付いているときに減速
        if (other.CompareTag("Player") && _flagManager.activeBalloon)
        {
            //ぐるりんの速度を軽減
            _rb2d = other.gameObject.GetComponent<Rigidbody2D>();
            _rb2d.velocity = new Vector2(_rb2d.velocity.x / 2.0f, -1.0f);
        }

        if (other.CompareTag("Balloon"))
        {
            //BallonGImmickのGravitySvaleを-5にする(一時的に強める)
            _balloonCol.GetComponent<Rigidbody2D>().gravityScale = -5.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Balloon"))
        {
            //BallonGImmickのGravitySvaleを-3にする(元に戻す)
            _balloonCol.GetComponent<Rigidbody2D>().gravityScale = -3.0f;
        }
    }
}
