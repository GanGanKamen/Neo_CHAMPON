using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空中歯車ギミック
/// </summary>

namespace Igarashi
{
    public class AerialGear : MonoBehaviour
    {
        [SerializeField] private GanGanKamen.GameController gameController;
        [SerializeField] [Header("回転移動時の速さの上限値")] private float maxSpeed;
       public enum GearType
        {
            Nomal,
            Rotate,
            GearPiston,
        }
        [Header("空中歯車のタイプ")] public GearType gearType;
        [SerializeField] [Header("歯車の回転速度")] private float gearRotationSpeed;
        [SerializeField] [Header("歯車の回転方向")] private bool gearRotationDirection;

        private GameObject _Gururin;
        private Rigidbody _GururinRb;
        private GanGanKamen.GururinBase _gururinBase;
        private Vector3 _GururinPos;
        private int _inputAngleDirection; // コントローラーの回転入力方向
        private int _rotDirection; // ぐるりんの回転方向
        private float _moveAngle;
        private float _rotSpeed;
        private bool _speedDown; // 減速判定
        private bool _keepSpeed;
        private bool _braking; // ブレーキ判定
        private bool _leave;

        private void Awake()
        {
            _inputAngleDirection = 0;
            _rotDirection = 0;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                _GururinPos = other.transform.position;
                // ぐるりんのposition.zと空中歯車のPosition.zが同じときに噛み合い
                if (_GururinPos.z == transform.position.z)
                {
                    CollisionSet(other.gameObject);
                }
            }
        }

        void Update()
        {
            if (gearType == GearType.Rotate)
            {
                switch (gearRotationDirection)
                {
                    case true:
                        transform.Rotate(0.0f, 0.0f, gearRotationSpeed);
                    break;

                    case false:
                        transform.Rotate(0.0f, 0.0f, -gearRotationSpeed);
                    break;
                }
            }
            else if (gearType == GearType.GearPiston)
            {
                // ぐるりんと歯車のZ軸が異なった時(ピストン作動時)に強制分離
                if (_Gururin != null && _GururinPos.z != transform.position.z)
                {
                    _leave = true;
                }
            }

            if (_Gururin != null && _gururinBase.IsAttachGimmick)
            {
                if (_braking == false)
                {
                    switch (gameController.InputIsPress)
                    {
                        // 操作入力時
                        case true:
                            // 左回転
                            if (gameController.InputAngle > 0)
                            {
                                if (_inputAngleDirection == -1)
                                {
                                    AerialGearSpeedDown();
                                }
                                else
                                {
                                    _keepSpeed = false;
                                    _inputAngleDirection = 1;
                                    CircularMotion(Vector3.forward);
                                }
                            }
                            // 右回転
                            else if (gameController.InputAngle < 0)
                            {
                                if (_inputAngleDirection == 1)
                                {
                                    AerialGearSpeedDown();
                                }
                                else
                                {
                                    _keepSpeed = false;
                                    _inputAngleDirection = -1;
                                    CircularMotion(Vector3.back);
                                }
                            }
                        break;

                        // 操作入力なし
                        case false:
                            _keepSpeed = true;
                        break;
                    }
                }

                // ブレーキ操作
                switch (gameController.InputLongPress)
                {
                    case true:
                        AerialGearBrake();
                    break;

                    case false:
                        _braking = false;
                    break;
                }

                // 入力がない時に最終操作時の入力速度で回転
                if (_keepSpeed)
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
        }

        private void LateUpdate()
        {
            if (_Gururin != null && _gururinBase.IsAttachGimmick)
            {
                var GururinPos = _Gururin.transform.position;
                var gearPos = transform.position;

                // ジャンプ(歯車から離れる)時の処理
                if (gameController.InputFlick || _leave)
                {
                    if (_leave == false)
                    {
                        AerialGearJump(GururinPos, gearPos);
                    }
                    _Gururin.transform.parent = null;
                    _GururinRb.useGravity = true;
                    _gururinBase.SeparateGimmick();
                    _gururinBase = null;
                    _Gururin = null;
                    _leave = false;
                }
            }
        }

        // 接触時にぐるりんのコンポーネント取得等あれこれ
        void CollisionSet(GameObject colObj)
        {
            _Gururin = colObj.gameObject;
            _Gururin.transform.parent = transform;

            _GururinRb = _Gururin.GetComponent<Rigidbody>();
            _GururinRb.velocity = Vector3.zero;
            _GururinRb.angularVelocity = Vector3.zero;
            _GururinRb.useGravity = false;

            _gururinBase = _Gururin.GetComponent<GanGanKamen.GururinBase>();
            _gururinBase.AttackToGimmick();

            _moveAngle = 0.0f;
            _inputAngleDirection = 0;
            _rotDirection = 0;
            _speedDown = false;
        }

        // 円運動
        void CircularMotion(Vector3 direction)
        {
            if (direction == Vector3.back)
            {
                _rotDirection = 1;
            }
            else
            {
                _rotDirection = -1;
            }

            _moveAngle += -gameController.InputAngle / 10.0f;
            if (_braking == false)
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
        void AerialGearSpeedDown()
        {
            _speedDown = true;
            if (_speedDown)
            {
                _keepSpeed = true;
                _rotSpeed -= Time.deltaTime;
                // 回転方向を反転
                if (_rotSpeed <= 0.1f)
                {
                    _inputAngleDirection *= -1;
                    _speedDown = false;
                }
            }
        }

        // 踏ん張り(ブレーキ)
        void AerialGearBrake()
        {
            _inputAngleDirection = 0;
            _rotSpeed -= Time.deltaTime * _gururinBase.brakePower;
            _moveAngle = _rotSpeed * 10.0f;
            if (_rotSpeed >= 0.1f)
            {
                _braking = true;
                return;
            }
            _braking = false;
        }

        // ジャンプの方向
        void AerialGearJump(Vector3 GururinPos, Vector3 gearPos)
        {
            var jumpPower = _gururinBase.jumpPower / 2.0f;
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
