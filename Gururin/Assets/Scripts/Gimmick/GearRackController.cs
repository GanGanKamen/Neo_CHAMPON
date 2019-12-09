using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRackController : MonoBehaviour
{

    [SerializeField] private GameObject traFixedObj, pointEffectObj, directionObj;
    private PointEffector2D _pointEffector2D;
    private PlayerMove _playerMove;
    private Rigidbody2D _gururinRb2d;
    public bool traFixedActive;

    // Start is called before the first frame update
    void Start()
    {
        if(pointEffectObj != null)
        {
            _pointEffector2D = pointEffectObj.GetComponent<PointEffector2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerMove = other.GetComponent<PlayerMove>();
            _gururinRb2d = other.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (traFixedObj != null && traFixedActive)
        {
            case true:
                pointEffectObj.SetActive(true);
                directionObj.SetActive(true);
                traFixedObj.SetActive(false);
                break;

            case false:
                directionObj.SetActive(false);
                traFixedObj.SetActive(true);
                break;
        }

        if (_playerMove != null && _pointEffector2D != null)
        {
            _playerMove.setSpeed = false;
            _playerMove.speed[0] = 5.0f;
            //歯車型ラックの重力 = ぐるりんの移動速度の絶対値 * 重力加速度
            _pointEffector2D.forceMagnitude = Mathf.Abs(_gururinRb2d.velocity.x) * -9.81f;
            //重力の下限値
            if(_pointEffector2D.forceMagnitude >= -20.0f)
            {
                _pointEffector2D.forceMagnitude = -20.0f;
            }
            //重量が-40.0以下の時(ぐるりんの速度が大きい時)重力を強くする
            else if (-40.0f >= _pointEffector2D.forceMagnitude)
            {
                _pointEffector2D.forceMagnitude *= 2.0f;
                Debug.Log(Mathf.Abs(_gururinRb2d.velocity.x));
            }
            //Debug.Log(Mathf.Abs(_gururinRb2d.velocity.x));
            //ラックが回転している方向に逆らう時にぐるりんの速度を補正or重力を弱める
        }
    }
}
