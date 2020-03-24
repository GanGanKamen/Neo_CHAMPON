using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ラックギミックの動作処理
/// </summary>

namespace Igarashi
{
    public class Rack : MonoBehaviour
    {
        public enum GravityType
        {
            Up,
            Left,
            Right
        }
        [SerializeField] [Header("重力の方向")] GravityType gravityType;
        [SerializeField] [Header("減速値 0.1~1.0")] [Range(_lowerSpeedLimit, 1.0f)] private float deceleration;

        private GameObject _Gururin;
        private Rigidbody _GururinRb;
        private PlayerFace _playerFace;
        private GanGanKamen.GururinBase _gururinBase;
        private GanGanKamen.GameController _gameController;
        private float _moveAngle;
        private const float _lowerSpeedLimit = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
            _gameController = GameObject.Find("GameController").GetComponent<GanGanKamen.GameController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
            {
                CollisionSettings(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>() && _Gururin != null)
            {
                _GururinRb.useGravity = true;
                _gururinBase.SeparateGimmick();
                _gururinBase = null;
                _Gururin = null;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_Gururin == null) return;

            if (_gameController.InputAngle != 0.0f)
            {
                _moveAngle += -_gameController.InputAngle / 10.0f;

                if (gravityType != GravityType.Up)
                {
                    _GururinRb.useGravity = false;
                }
            }
            else
            {
                _moveAngle = 0.0f;
                // GravityTypeがLeft or Rightの時
                if (gravityType != GravityType.Up)
                {
                    _GururinRb.useGravity = _gameController.InputLongPress ? false : true;
                    switch (_gameController.InputLongPress)
                    {
                        // 踏ん張りで停止
                        case true:
                            _playerFace.Angry();
                            _GururinRb.velocity = Vector3.zero;
                            _GururinRb.angularVelocity = Vector3.zero;
                            break;

                        // 踏ん張らなければ落下
                        case false:
                            _playerFace.Nomal();
                            break;
                    }
                    return;
                }
                _GururinRb.velocity = Vector3.zero;
            }
        }

        private void FixedUpdate()
        {
            if (_Gururin != null)
            {
                RackGururinMove();
                GravityChange();
            }
        }

        private void LateUpdate()
        {
            if (_Gururin == null) return;

            // ジャンプ(歯車から離れる)時の処理
            if (_gameController.InputFlick)
            {
                RackJump();
            }
        }

        // 接触時にぐるりんのコンポーネント取得等あれこれ
        void CollisionSettings(GameObject colObj)
        {
            _Gururin = colObj.gameObject;
            _playerFace = _Gururin.GetComponentInChildren<PlayerFace>();

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
            var realSpeed = rotSpeed * deceleration;
            if (realSpeed >= _gururinBase.maxSpeed)
            {
                realSpeed = _gururinBase.maxSpeed;
            }
            else if (realSpeed <= -_gururinBase.maxSpeed)
            {
                realSpeed = -_gururinBase.maxSpeed;
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
                    var rightForce = new Vector3(_gururinBase.jumpPower, _gururinBase.jumpPower);
                    _GururinRb.AddForce(rightForce, ForceMode.VelocityChange);
                    break;

                case GravityType.Right:
                    var leftForce = new Vector3(-_gururinBase.jumpPower, _gururinBase.jumpPower);
                    _GururinRb.AddForce(leftForce, ForceMode.VelocityChange);
                    break;
            }
        }
    }
}
