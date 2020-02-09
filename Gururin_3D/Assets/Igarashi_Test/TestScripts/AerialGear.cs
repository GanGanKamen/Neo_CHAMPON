using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialGear : MonoBehaviour
{

    [SerializeField] private float speed;
    private float _recodeSpeed;
    private bool _speedDown;
    private bool[] _directions = new bool[2];

    private GameObject _gururin;
    private Rigidbody _gururinRb2d;
    private TestPlayer _testPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _recodeSpeed = speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject)
        {
            _gururin = other.gameObject;
            _gururin.transform.parent = transform;

            _gururinRb2d = _gururin.GetComponent<Rigidbody>();
            _gururinRb2d.velocity = Vector3.zero;
            _gururinRb2d.angularVelocity = Vector3.zero;
            _gururinRb2d.useGravity = false;

            _testPlayer = _gururin.GetComponent<TestPlayer>();
            _testPlayer.gimmickHit = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject)
        {
            _gururin.transform.parent = null;
            _gururinRb2d.useGravity = true;
            _testPlayer.gimmickHit = false;
            _gururin = null;

            speed = _recodeSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 10);

        /*
        var distance = Vector3.Distance(gururin.transform.position, transform.position);
        Debug.Log(distance);
        _gururinRb2d.MovePosition(new Vector3(Mathf.Sin(Time.time * speed), gururin.transform.position.y, Mathf.Cos(Time.time * speed)));
        */

        if (_gururin != null && _testPlayer.gimmickHit)
        {
            //localPositionだと親オブジェクトの角度が変わった時に変動してしまうので要修正
            var gururinPos =  _gururin.transform.position;
            var gearPos = transform.position;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //第一象限(右上)
                if(gururinPos.x >  gearPos.x && gururinPos.y > gearPos.y)
                {
                    _gururinRb2d.AddForce(new Vector2(100.0f + speed, 100.0f + speed));
                }
                //第二象限(左上)
                else if(gearPos.x > gururinPos.x && gururinPos.y > gearPos.y)
                {
                    _gururinRb2d.AddForce(new Vector2(-(100.0f + speed), 100.0f + speed));
                }
                //第三象限(左下)
                else if(gearPos.x > gururinPos.x && gearPos.y > gururinPos.y)
                {
                    _gururinRb2d.AddForce(new Vector2(-(100.0f + speed), -(100.0f + speed)));
                }
                //第四象限(右下)
                else if (gururinPos.x > gearPos.x && gearPos.y > gururinPos.y)
                {
                    _gururinRb2d.AddForce(new Vector2(100.0f + speed, -(100.0f + speed)));
                }

                Debug.Log(gururinPos);
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                _speedDown = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Rotate(Vector3.back);
                _directions[0] = true;
                _directions[1] = false;
                _speedDown = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Rotate(Vector3.forward);
                _directions[0] = false;
                _directions[1] = true;
                _speedDown = false;
            }

            if (_speedDown)
            {
                if (_directions[0])
                {
                    Rotate(Vector3.back);
                }
                else if (_directions[1])
                {
                    Rotate(Vector3.forward);
                }
            }
        }
    }

    void Rotate(Vector3 direction)
    {
        //歯車(仮)の周りをぐるりん(仮)が回転
        _gururin.transform.RotateAround(transform.position, direction, speed * Time.deltaTime + Mathf.Abs(_gururinRb2d.velocity.x));

        //移動していないとき速度を減少
        if (_speedDown)
        {
            speed -= Time.deltaTime * 100.0f;
            if (speed <= 0.0f)
            {
                speed = _recodeSpeed;
                _speedDown = false;
            }
        }
        //移動入力で速度増加
        else
        {
            speed += Time.deltaTime * 100.0f;
            if (speed >= 500.0f)
            {
                speed = 500.0f;
            }
        }
    }
}
