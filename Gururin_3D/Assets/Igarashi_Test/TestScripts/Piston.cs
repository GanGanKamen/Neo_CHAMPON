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
        [SerializeField] [Range(0.0f, 2.0f)] [Header("ピストンの押し出し速度 0.0 ~ 2.0")] private float pushSpeed;
        [SerializeField] [Range(0.0f, 2.0f)] [Header("ピストンの戻り速度 0.0 ~ 2.0")] private float pullSpeed;
        [SerializeField] [Range(0.0f, 20.0f)] [Header("ピストンの停止時間 0.0 ~ 20.0")] private float pistonStopTime;
        [SerializeField] [Header("押し出し限界地点")] private Transform pushLimitPos;

        private GameObject _Gururin;
        private Rigidbody _rigidbody;
        private Vector3 _startPos;
        private float _moveTimer; // 移動所要時間
        private float _stopTimer; // 停止時間用タイマー
        private bool _moveApproved; // 移動許可
        private bool _pushing;
        private bool _stopping;

        [SerializeField] private bool masterStop;

        private void Awake()
        {
            // 数値入力チェック
            if (pushSpeed <= 0.0f || pullSpeed <= 0.0f || pistonStopTime <= 0.0f)
            {
                Debug.LogError("数値が0です 入力されているか確認してください");
            }

            // 以下初期化
            _startPos = transform.position;
            _moveApproved = true;
            _pushing = true;
            _stopping = false;
            _moveTimer = 0.0f;
            _stopTimer = 0.0f;
        }

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (masterStop) return;
            // ピストン移動
            if (_moveApproved)
            {
                switch (_pushing)
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
            if (_stopping)
            {
                _stopTimer += Time.deltaTime;

                if (_stopTimer >= pistonStopTime)
                {
                    _stopTimer = 0.0f;
                    //移動方向を反転
                    switch (_pushing)
                    {
                        case true:
                            _pushing = false;
                            break;

                        case false:
                            _pushing = true;
                            break;
                    }
                    // 移動再開
                    _moveApproved = true;
                    _stopping = false;
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
                _stopping = true;
                _moveApproved = false;
                _moveTimer = 0.0f;
            }
            else
            {
                _moveTimer += Time.deltaTime;
            }
        }
    }
}
