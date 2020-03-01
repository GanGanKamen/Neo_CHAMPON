using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ラックギミック
/// </summary>

namespace Igarashi
{
    public class Rack : MonoBehaviour
    {
        [SerializeField] [Header("減速値")] [Range(_lowerSpeedLimit, 1.0f)] private float decelerationValue;
        [SerializeField] [Header("最大速度")] private float maxSpeed;
        public enum GravityType
        {
            Up,
            Right,
            Left
        }
        [Header("重力の方向")] public GravityType gravityType;

        private GameObject _Gururin;
        private Rigidbody _GururinRb;
        private GanGanKamen.GururinBase _gururinBase;
        private GanGanKamen.GameController _gameController;
        private float _moveAngle;
        private const float _lowerSpeedLimit = 0.1f;
        private bool _gravityChange; // 重力方向が変化しているかの判定
        private bool _stopping;

        // Start is called before the first frame update
        void Start()
        {
            _gameController = GameObject.Find("GameController").GetComponent<GanGanKamen.GameController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                CollisionSet(other.gameObject);
                _gravityChange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                if (_Gururin != null)
                {
                    _gravityChange = false;
                    _GururinRb.useGravity = true;
                    _gururinBase.SeparateGimmick();
                    _gururinBase = null;
                    _Gururin = null;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_Gururin != null && _gururinBase.IsAttachGimmick)
            {
                RackGururinMove();

                if (_gameController.InputAngle != 0)
                {
                    _moveAngle += -_gameController.InputAngle / 10.0f;

                    if (gravityType == GravityType.Left || gravityType == GravityType.Right)
                    {
                        _GururinRb.useGravity = false;
                    }
                }
                else
                {
                    _moveAngle = 0.0f;
                    // GravityTypeがLeft or Rightの時
                    if (gravityType == GravityType.Left || gravityType == GravityType.Right)
                    {
                        switch (_gameController.InputLongPress)
                        {
                            // 踏ん張りで停止
                            case true:
                                _GururinRb.velocity = Vector3.zero;
                                _GururinRb.angularVelocity = Vector3.zero;
                                _GururinRb.useGravity = false;
                                break;

                            // 踏ん張らなければ落下
                            case false:
                                _GururinRb.useGravity = true;
                                break;
                        }
                        return;
                    }
                    _GururinRb.velocity = Vector3.zero;
                }

                /*
                // 油を取ると最大速度上昇
                if ()
                {
                    maxSpeed += ;
                }
                */
            }
        }

        private void FixedUpdate()
        {
            if (_Gururin != null && _gravityChange)
            {
                GravityChange();
            }
        }

        private void LateUpdate()
        {
            if (_Gururin != null && _gururinBase.IsAttachGimmick)
            {
                // ジャンプ(歯車から離れる)時の処理
                if (_gameController.InputFlick)
                {
                    RackJump();
                }
            }
        }

        // 接触時にぐるりんのコンポーネント取得等あれこれ
        void CollisionSet(GameObject colObj)
        {
            _Gururin = colObj.gameObject;

            _GururinRb = _Gururin.GetComponent<Rigidbody>();
            _GururinRb.velocity = Vector3.zero;
            _GururinRb.angularVelocity = Vector3.zero;
            _GururinRb.useGravity = false;

            _gururinBase = _Gururin.GetComponent<GanGanKamen.GururinBase>();
            _gururinBase.AttackToGimmick();
        }

        // 重量変化
        void GravityChange()
        {
            switch (gravityType)
            {
                case GravityType.Up:
                    var upForce = new Vector3(0.0f, 9.81f);
                    _GururinRb.AddForce(upForce, ForceMode.Acceleration);
                    break;

                case GravityType.Left:
                    var leftForce = new Vector3(-9.81f, 0.0f);
                    _GururinRb.AddForce(leftForce, ForceMode.Acceleration);
                    break;

                case GravityType.Right:
                    var rightForce = new Vector3(9.81f, 0.0f);
                    _GururinRb.AddForce(rightForce, ForceMode.Acceleration);
                    break;
            }
        }

        // ラック上での移動処理
        void RackGururinMove()
        {
            // ぐるりんのZ軸を0で固定する
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);

            var rotSpeed = _moveAngle * _gururinBase.accel * Time.deltaTime;
            // 通常時より減速した状態で移動
            var realSpeed = rotSpeed * decelerationValue;
            if (realSpeed >= maxSpeed)
            {
                realSpeed = maxSpeed;
            }
            else if (realSpeed <= -maxSpeed)
            {
                realSpeed = -maxSpeed;
            }

            // GravityTypeに応じて移動方向を変化
            switch (gravityType)
            {
                case GravityType.Up:
                    var moveUpVecSpeed = new Vector3(-realSpeed, 0.0f) - _GururinRb.velocity;
                    _GururinRb.AddForce(moveUpVecSpeed, ForceMode.Acceleration);
                    break;

                case GravityType.Left:
                    var moveLeftVecSpeed = new Vector3(0.0f, -realSpeed) - _GururinRb.velocity;
                    _GururinRb.AddForce(moveLeftVecSpeed, ForceMode.Acceleration);
                    break;

                case GravityType.Right:
                    var moveRightVecSpeed = new Vector3(0.0f, realSpeed) - _GururinRb.velocity;
                    _GururinRb.AddForce(moveRightVecSpeed, ForceMode.Acceleration);
                    break;
            }
        }

        void RackJump()
        {
            switch (gravityType)
            {
                case GravityType.Up:
                    var downForce = new Vector3(0.0f, -_gururinBase.jumpPower);
                    _GururinRb.AddForce(downForce, ForceMode.VelocityChange);
                    break;

                case GravityType.Left:
                    var rightForce = new Vector3(_gururinBase.jumpPower, 0.0f);
                    _GururinRb.AddForce(rightForce, ForceMode.VelocityChange);
                    break;

                case GravityType.Right:
                    var leftForce = new Vector3(-_gururinBase.jumpPower, 0.0f);
                    _GururinRb.AddForce(leftForce, ForceMode.VelocityChange);
                    break;
            }
        }
    }
}
