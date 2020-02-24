﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空中歯車ギミック
/// </summary>

namespace Igarashi
{
    public class AerialGear : MonoBehaviour
    {
        [SerializeField] [Header("回転移動時の速さの上限値")] private float maxSpeed;
        [SerializeField] [Header("ジャンプの強さ")] private float jumpPower;
        public enum GearType
        {
            Nomal,
            Rotate
        }
        public GearType gearType;
        [SerializeField] [Header("歯車の回転方向")] private bool rotationDirection;
        [SerializeField] private GanGanKamen.GururinBase gururinBase; // PlayerCtrlから継承
        [SerializeField] private GanGanKamen.GameController gameController;

        private GameObject _Gururin;
        private Rigidbody _GururinRb;
        private float _moveAngle;
        private float _rotSpeed;
        private bool _speedDown; // 減速判定
        private bool _keepSpeed;
        private bool _braking; // ブレーキ判定
        private int _inputAngleDirection; // コントローラーの回転入力方向
        private int _rotDirection; // ぐるりんの回転方向

        void Start()
        {
            _inputAngleDirection = 0;
            _rotDirection = 0;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
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

            if (_Gururin != null && gururinBase.IsAttachGimmick)
            {
                Debug.Log(gameController.InputAngle);
                if (_braking == false)
                {
                    if (gameController.InputIsPress == false)
                    {
                        _keepSpeed = true;
                    }
                    // 回転操作
                    else
                    {
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
                    }
                }

                // ブレーキ操作
                if (gameController.InputLongPress)
                {
                    AerialGearBrake();
                }
                else
                {
                    _braking = false;
                }

                // 入力がない時に最終操作入力時の速度で回転
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
            if (_Gururin != null && gururinBase.IsAttachGimmick)
            {
                var GururinPos = _Gururin.transform.position;
                var gearPos = transform.position;

                // ジャンプ(歯車から離れる)時の処理
                if (gameController.InputFlick)
                {
                    AerialGearJump(GururinPos, gearPos);

                    _Gururin.transform.parent = null;
                    _GururinRb.useGravity = true;
                    gururinBase.SeparateGimmick();

                    _Gururin = null;
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

            _moveAngle = 0.0f;
            _inputAngleDirection = 0;
            _rotDirection = 0;
            _speedDown = false;

            gururinBase.AttackToGimmick();
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
                _rotSpeed = Mathf.Abs(_moveAngle / 2.0f * Time.deltaTime);
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
            _rotSpeed -= Time.deltaTime * 5.0f;
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
