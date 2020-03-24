using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ピストンギミック
/// </summary>

namespace Igarashi
{
    public class Piston : MonoBehaviour
    {
        public bool HasPushed { get { return _hasPushed; } }

        [SerializeField] [Header("押し出し限界地点")] private Transform pushLimitPos;
        [SerializeField] [Header("ピストンの押し出し速度 0.1~2.0")] [Range(_lowerSpeedLimit, 2.0f)] private float pushSpeed;
        [SerializeField] [Header("ピストンの戻り速度 0.1~2.0")] [Range(_lowerSpeedLimit, 2.0f)] private float pullSpeed;
        [SerializeField] [Header("押し出し時の停止時間 0.1~20.0")] [Range(_lowerSpeedLimit, 20.0f)] private float pushStopTime;
        [SerializeField] [Header("戻り時の停止時間 0.1~20.0")] [Range(_lowerSpeedLimit, 20.0f)] private float pullStopTime;
        [SerializeField] [Header("初動遅延時間 0.0~20.0")] [Range(0.0f, 20.0f)] private float startDelayTime;
        [SerializeField] [Header("ピストンの動作を停止")] private bool canStop;

        private GameObject _Gururin;
        private Rigidbody _rigidbody;
        private Vector3 _startPos;
        private float _moveTimer; // 移動所要時間
        private float _stopTimer; // 停止時間用タイマー
        private float _delayTimer; // 初動遅延用タイマー
        private const float _lowerSpeedLimit = 0.1f; // 各速度の下限値
        private bool _canMove; // 移動許可
        private bool _canDelayed;
        private bool _hasPushed;

        private void Awake()
        {
            _startPos = transform.position;
            _canMove = true;
             _hasPushed = true;
            if(startDelayTime > 0.0f)
            {
                _canDelayed = true;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (canStop) return;

            // 遅延時間分停止
            if (_canDelayed)
            {
                _delayTimer += Time.deltaTime;

                if(_delayTimer >= startDelayTime)
                {
                    _canDelayed = false;
                }
                return;
            }

            switch (_canMove)
            {
                // ピストン移動
                case true:
                    var pistonPos = _hasPushed ? _startPos : pushLimitPos.position;
                    var targetPos = _hasPushed ? pushLimitPos.position : _startPos;
                    var moveSpeed = _hasPushed ? pushSpeed : pullSpeed;
                    MovePiston(pistonPos, targetPos, moveSpeed);
                    break;

                // ピストン停止
                case false:
                    _stopTimer += Time.deltaTime;
                    var pistonStopTime = _hasPushed ? pushStopTime : pullStopTime;
                    if (_stopTimer >= pistonStopTime)
                    {
                        _stopTimer = 0.0f;
                        // 移動方向を反転
                        _hasPushed = !_hasPushed;
                        // 移動再開
                        _canMove = true;
                    }
                    break;
            }
        }

        // ピストンの移動あれこれ
        private void MovePiston(Vector3 pistonPos, Vector3 targetPos, float moveSpeed)
        {
            var movePos = Vector3.Lerp(pistonPos, targetPos, Mathf.Abs(moveSpeed * _moveTimer));
            _rigidbody.MovePosition(movePos);

            // ピストンがtargetPosに着いたらタイマーを初期化、ピストンを一時停止
            if (transform.position == targetPos)
            {
                _moveTimer = 0.0f;
                _canMove = false;
            }
            else
            {
                _moveTimer += Time.deltaTime;
            }
        }
    }
}
