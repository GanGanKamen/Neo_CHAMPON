using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空中歯車ギミック関係の動作処理
/// </summary>

namespace Igarashi
{
    public class AerialGearBase : MonoBehaviour
    {
        [SerializeField] private AerialRotatingGear _aerialRotatingGear;
        public enum GearType
        {
            Normal,
            Rotating,
            GearPiston
        }
        [Header("空中歯車のタイプ")] public GearType gearType;
        [SerializeField] [Header("回転移動時の速さの上限値")] private float maxSpeed;

        private GameObject _Gururin;
        private Rigidbody _GururinRb;
        private PlayerFace _playerFace;
        private GanGanKamen.GururinBase _gururinBase;
        private GanGanKamen.GameController _gameController;
        private Vector3 _GururinPos;
        private int _inputAngleDirection; // コントローラーの回転入力方向
        private int _rotDirection; // ぐるりんの回転方向
        private float _moveAngle;
        private float _rotSpeed;
        private bool _canKeepSpeed;
        private bool _hasBraked; // ブレーキ判定
        private bool _hasSeparated;

        private void Awake()
        {
            _inputAngleDirection = 0;
            _rotDirection = 0;
        }

        // Start is called before the first frame update
        void Start()
        {
            _gameController = GameObject.Find("GameController").GetComponent<GanGanKamen.GameController>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                _GururinPos = other.transform.position;
                // ぐるりんのposition.zと空中歯車のPosition.zが同じときに噛み合い
                if (_GururinPos.z == transform.position.z)
                {
                    CollisionSettings(other.gameObject);
                    _moveAngle = 0.0f;
                    _inputAngleDirection = 0;
                    _rotDirection = 0;
                }
            }
        }

        void Update()
        {
            switch (gearType)
            {
                case GearType.Rotating:
                    _aerialRotatingGear.RotatingGear(gameObject);
                    break;

                case GearType.GearPiston:
                    // ぐるりんと歯車のZ軸が異なった時(ピストン作動時)に強制分離
                    if (_Gururin != null && _GururinPos.z != transform.position.z)
                    {
                        _hasSeparated = true;
                    }
                    break;

                default:
                    break;
            }

            if (_Gururin == null) return;

            if (_hasBraked == false)
            {
                switch (_gameController.InputIsPress)
                {
                    // 操作入力あり
                    case true:
                        ControllerOperation();
                        break;

                    // 操作入力なし
                    case false:
                        _canKeepSpeed = true;
                        break;
                }
            }

            // ブレーキ操作
            switch (_gameController.InputLongPress)
            {
                case true:
                    _playerFace.Angry();
                    AerialGearBrake();
                    break;

                case false:
                    _playerFace.Nomal();
                    _hasBraked = false;
                    break;
            }

            // 入力がない時に最終操作時の入力速度で回転
            if (_canKeepSpeed)
            {
                if (_rotDirection == 1)
                {
                    CircularMotion(Vector3.back);
                }
                else if (_rotDirection == -1)
                {
                    CircularMotion(Vector3.forward);
                }
            }
        }

        private void LateUpdate()
        {
            if (_Gururin == null) return;

            var GururinPos = _Gururin.transform.position;
            var gearPos = transform.position;

            // ジャンプ(歯車から離れる)時の処理
            if (_gameController.InputFlick || _hasSeparated)
            {
                if (_hasSeparated == false)
                {
                    AerialGearJump(GururinPos, gearPos);
                }

                _GururinRb.useGravity = true;

                _gururinBase.SeparateGimmick();
                _gururinBase = null;

                _Gururin.transform.parent = null;
                _Gururin = null;

                _hasSeparated = false;
            }
        }

        // 接触時にぐるりんのコンポーネント取得等あれこれ
        private void CollisionSettings(GameObject colObj)
        {
            _Gururin = colObj.gameObject;
            _Gururin.transform.parent = transform;
            _playerFace = _Gururin.GetComponentInChildren<PlayerFace>();

            if (_GururinRb == null)
            {
                _GururinRb = _Gururin.GetComponent<Rigidbody>();
            }
            _GururinRb.velocity = Vector3.zero;
            _GururinRb.angularVelocity = Vector3.zero;
            _GururinRb.useGravity = false;

            _gururinBase = _Gururin.GetComponent<GanGanKamen.GururinBase>();
            _gururinBase.AttackToGimmick();
        }

        // コントローラーの回転操作
        private void ControllerOperation()
        {
            _canKeepSpeed = false;
            // 左回転
            if (_gameController.InputAngle > 0.0f)
            {
                switch (_inputAngleDirection)
                {
                    case -1:
                        AerialGearSpeedDown();
                        break;

                    default:
                        _inputAngleDirection = 1;
                        CircularMotion(Vector3.forward);
                        break;
                }
            }
            // 右回転
            else if (_gameController.InputAngle < 0.0f)
            {
                switch (_inputAngleDirection)
                {
                    case 1:
                        AerialGearSpeedDown();
                        break;

                    default:
                        _inputAngleDirection = -1;
                        CircularMotion(Vector3.back);
                        break;
                }
            }
            else if(_gameController.InputAngle == 0.0f)
            {
                _moveAngle = 0.0f;
            }
        }

        // 円運動
        private void CircularMotion(Vector3 direction)
        {
            if (direction == Vector3.back)
            {
                _rotDirection = 1;
                _Gururin.transform.Rotate(0.0f, 0.0f, -_rotSpeed);
            }
            else
            {
                _rotDirection = -1;
                _Gururin.transform.Rotate(0.0f, 0.0f, _rotSpeed);
            }

            _moveAngle += -_gameController.InputAngle / 10.0f;
            if (_hasBraked == false)
            {
                _rotSpeed = Mathf.Abs(_moveAngle / _gururinBase.accel * Time.deltaTime);
                if (_rotSpeed >= maxSpeed)
                {
                    _rotSpeed = maxSpeed;
                }
            }
            // 歯車の周りをぐるりんが回転
            _Gururin.transform.RotateAround(transform.position, direction, _rotSpeed);
        }

        // 減速
        private void AerialGearSpeedDown()
        {
            _canKeepSpeed = true;
            _rotSpeed -= Time.deltaTime;
            // 回転方向を反転
            if (_rotSpeed <= 0.1f)
            {
                _inputAngleDirection *= -1;
            }
        }

        // 踏ん張り(ブレーキ)
        private void AerialGearBrake()
        {
            _inputAngleDirection = 0;
            _rotSpeed -= Time.deltaTime * _gururinBase.brakePower;
            _moveAngle = _rotSpeed * 10.0f;
            if (_rotSpeed >= 0.1f)
            {
                _hasBraked = true;
                return;
            }
            _hasBraked = false;
        }

        // ジャンプの方向
        private void AerialGearJump(Vector3 GururinPos, Vector3 gearPos)
        {
            var jumpPower = _gururinBase.jumpPower;
            // 第一象限(右上)
            if (GururinPos.x > gearPos.x && GururinPos.y > gearPos.y)
            {
                _GururinRb.AddForce(new Vector2(jumpPower, jumpPower), ForceMode.VelocityChange);
            }
            // 第二象限(左上)
            else if (gearPos.x > GururinPos.x && GururinPos.y > gearPos.y)
            {
                _GururinRb.AddForce(new Vector2(-jumpPower, jumpPower), ForceMode.VelocityChange);
            }
            // 第三象限(左下)
            else if (gearPos.x > GururinPos.x && gearPos.y > GururinPos.y)
            {
                _GururinRb.AddForce(new Vector2(-jumpPower, -jumpPower), ForceMode.VelocityChange);
            }
            // 第四象限(右下)
            else if (GururinPos.x > gearPos.x && gearPos.y > GururinPos.y)
            {
                _GururinRb.AddForce(new Vector2(jumpPower, -jumpPower), ForceMode.VelocityChange);
            }
        }
    }
}
