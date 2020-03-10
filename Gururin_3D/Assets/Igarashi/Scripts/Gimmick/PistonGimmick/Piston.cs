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
        [SerializeField] [Range(_lowerSpeedLimit, 2.0f)] [Header("ピストンの押し出し速度 0.1~2.0")] private float pushSpeed;
        [SerializeField] [Range(_lowerSpeedLimit, 2.0f)] [Header("ピストンの戻り速度 0.1~2.0")] private float pullSpeed;
        [SerializeField] [Range(_lowerSpeedLimit, 20.0f)] [Header("ピストンの停止時間 0.1~20.0")] private float pistonStopTime;
        [SerializeField] [Header("ピストンの動作を停止")] private bool canStop;

        private GameObject _Gururin;
        private Rigidbody _rigidbody;
        private Vector3 _startPos;
        private float _moveTimer; // 移動所要時間
        private float _stopTimer; // 停止時間用タイマー
        private const float _lowerSpeedLimit = 0.1f; // 各速度の下限値
        private bool _canMove; // 移動許可
        private bool _hasPushed;
        private bool  _hasStopped;

        private void Awake()
        {
            _startPos = transform.position;
            _canMove = true;
             _hasPushed = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (canStop) return;

            // ピストン移動
            if (_canMove)
            {
                switch ( _hasPushed)
                {
                    case true:
                        MovePiston(_startPos, pushLimitPos.position, pushSpeed);
                        break;

                    case false:
                        MovePiston(pushLimitPos.position, _startPos, pullSpeed);
                        break;
                }
            }

            // ピストン停止
            if ( _hasStopped)
            {
                _stopTimer += Time.deltaTime;

                if (_stopTimer >= pistonStopTime)
                {
                    _stopTimer = 0.0f;
                    // 移動方向を反転
                    switch ( _hasPushed)
                    {
                        case true:
                             _hasPushed = false;
                            break;

                        case false:
                             _hasPushed = true;
                            break;
                    }
                    // 移動再開
                    _canMove = true;
                     _hasStopped = false;
                }
            }
        }

        // ピストンの移動あれこれ
        void MovePiston(Vector3 pistonPos, Vector3 targetPos, float moveSpeed)
        {
            var movePos = Vector3.Lerp(pistonPos, targetPos, Mathf.Abs(moveSpeed * _moveTimer));
            _rigidbody.MovePosition(movePos);

            // ピストンがtargetPosに着いたらタイマーを初期化、ピストンを一時停止
            if (transform.position == targetPos)
            {
                 _hasStopped = true;
                _canMove = false;
                _moveTimer = 0.0f;
            }
            else
            {
                _moveTimer += Time.deltaTime;
            }
        }
    }
}
