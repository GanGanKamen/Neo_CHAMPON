using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRackController : MonoBehaviour
{

    [SerializeField] private GameObject traFixedObj, pointEffectObj, directionObj, rotGear;
    private PointEffector2D _pointEffector2D;
    private PlayerMove _playerMove;
    private Rigidbody2D _gururinRb2d;
    private Gamecontroller _gameController;
    public bool traFixedActive;

    public float rotSpeed;

    //自動回転歯車の回転方向
    public enum RotationDirection
    {
        None,
        Clockwise,
        AntiClockwise
    }
    public RotationDirection rotationDirection;

    // Start is called before the first frame update
    void Start()
    {
        if (rotSpeed == 0.0f)
        {
            rotSpeed = 1.0f;
        }
        rotSpeed = Mathf.Abs(rotSpeed);

        if (pointEffectObj != null)
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
            _gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rotGear != null)
        {
            switch (rotationDirection)
            {
                case RotationDirection.Clockwise:
                    rotGear.transform.Rotate(new Vector3(0.0f, 0.0f, rotSpeed));
                    break;

                case RotationDirection.AntiClockwise:
                    rotGear.transform.Rotate(new Vector3(0.0f, 0.0f, -rotSpeed));
                    break;
            }
        }

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

        if (_gameController != null)
        {
            _playerMove.setSpeed = false;
            _playerMove.speed[0] = 3.0f;
            switch (rotationDirection)
            {
                case RotationDirection.Clockwise:
                    //時計回り
                    if (_gameController.AxB.z < 0)
                    {
                        GearRackGravity(-40.0f, -20.0f);
                    }
                    //反時計回り
                    if (_gameController.AxB.z > 0)
                    {
                        GearRackGravity(-40.0f, -20.0f);
                    }
                    break;

                case RotationDirection.AntiClockwise:
                    //時計回り
                    if (_gameController.AxB.z < 0)
                    {
                        GearRackGravity(-40.0f, -20.0f);
                    }
                    //反時計回り
                    if (_gameController.AxB.z > 0)
                    {
                        GearRackGravity(-40.0f, -20.0f);
                    }
                    break;
            }
        }
        //Debug.Log(Mathf.Abs(_gururinRb2d.velocity.x));
        //ラックが回転している方向に逆らう時にぐるりんの速度を補正or重力を弱める
    }

    void GearRackGravity(float maxGravity, float minGravity)
    {
        //歯車型ラックの重力 = ぐるりんの移動速度の絶対値 * 重力加速度
        _pointEffector2D.forceMagnitude = Mathf.Abs(_gururinRb2d.velocity.x) * -9.81f;
        //重力の下限値
        if (Mathf.Abs(_gururinRb2d.velocity.x) <= 3.5f)
        {
            _pointEffector2D.forceMagnitude = minGravity;
        }
        else if (Mathf.Abs(_gururinRb2d.velocity.x) >= 4.5f)
        {
            _pointEffector2D.forceMagnitude += maxGravity * 2.0f;
        }
        //重量がmaxGravity以下の時(ぐるりんの速度が大きい時)重力を強くする
        else if (Mathf.Abs(_gururinRb2d.velocity.x) >= 3.5f) //(maxGravity >= _pointEffector2D.forceMagnitude)
        {
            _pointEffector2D.forceMagnitude += maxGravity;
            Debug.Log(Mathf.Abs(_gururinRb2d.velocity.x));
        }
    }
}
