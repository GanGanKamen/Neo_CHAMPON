using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空中歯車ギミック
/// </summary>

public class AerialGear : MonoBehaviour
{
    [SerializeField,Header("回転移動時の速さ")] private float speed;
    private float _recodeSpeed;
    private bool _speedDown;
    private bool[] _directions = new bool[2];

    public enum GearType
    {
        Nomal,
        Rotate
    }
    public GearType gearType;
    [SerializeField,Header("歯車の回転方向")] private bool rotationDirection;

    private GameObject _Gururin;
    private Rigidbody _GururinRb;
    private TestPlayer _testPlayer;

    void Start()
    {
        _recodeSpeed = speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<TestPlayer>())
        {
            CollisionSet(other.gameObject);
        }
    }

    void Update()
    {
        if (gearType == GearType.Rotate)
        {
            switch (rotationDirection)
            {
                case true:
                    transform.Rotate(0, 0, 5);
                    break;

                case false:
                    transform.Rotate(0, 0, -5);
                    break;
            }
        }

        if (_Gururin != null && _testPlayer.gimmickHit)
        {
            //移動
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                _speedDown = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                CircularMotion(Vector3.back);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                CircularMotion(Vector3.forward);
            }

            //速度の減少方向
            if (_speedDown)
            {
                if (_directions[0])
                {
                    CircularMotion(Vector3.back);
                }
                else if (_directions[1])
                {
                    CircularMotion(Vector3.forward);
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (_Gururin != null && _testPlayer.gimmickHit)
        {
            var _GururinPos = _Gururin.transform.position;
            var _gearPos = transform.position;

            //ジャンプ(歯車から離れる)時の処理
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump(_GururinPos, _gearPos);

                speed = _recodeSpeed;

                _Gururin.transform.parent = null;
                _GururinRb.useGravity = true;
                _testPlayer.gimmickHit = false;

                _Gururin = null;

                //Debug.Log(_GururinPos);
            }
        }
    }

    //接触時にぐるりんのコンポーネント取得等あれこれ
    void CollisionSet(GameObject colObj)
    {
        _Gururin = colObj.gameObject;
        _Gururin.transform.parent = transform;

        _GururinRb = _Gururin.GetComponent<Rigidbody>();
        _GururinRb.velocity = Vector3.zero;
        _GururinRb.angularVelocity = Vector3.zero;
        _GururinRb.useGravity = false;

        _testPlayer = _Gururin.GetComponent<TestPlayer>();
        _testPlayer.gimmickHit = true;
    }

    //円運動
    void CircularMotion(Vector3 direction)
    {
        //歯車(仮)の周りをぐるりん(仮)が回転
        _Gururin.transform.RotateAround(transform.position, direction, speed * Time.deltaTime + Mathf.Abs(_GururinRb.velocity.x));
        _speedDown = false;

        if (direction == Vector3.forward)
        {
            _directions[0] = false;
            _directions[1] = true;
        }
        else
        {
            _directions[0] = true;
            _directions[1] = false;
        }

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

    void Jump(Vector3 GururinPos, Vector3 gearPos)
    {
        //第一象限(右上)
        if (GururinPos.x > gearPos.x && GururinPos.y > gearPos.y)
        {
            _GururinRb.AddForce(new Vector2(100.0f + speed, 100.0f + speed));
        }
        //第二象限(左上)
        else if (gearPos.x > GururinPos.x && GururinPos.y > gearPos.y)
        {
            _GururinRb.AddForce(new Vector2(-(100.0f + speed), 100.0f + speed));
        }
        //第三象限(左下)
        else if (gearPos.x > GururinPos.x && gearPos.y > GururinPos.y)
        {
            _GururinRb.AddForce(new Vector2(-(100.0f + speed), -(100.0f + speed)));
        }
        //第四象限(右下)
        else if (GururinPos.x > gearPos.x && gearPos.y > GururinPos.y)
        {
            _GururinRb.AddForce(new Vector2(100.0f + speed, -(100.0f + speed)));
        }
    }
}
