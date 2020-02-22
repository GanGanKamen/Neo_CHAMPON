using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ピストンギミック
/// </summary>

public class Piston : MonoBehaviour
{
    private GameObject _Gururin;

    private Rigidbody _rigidbody;
    [SerializeField, Range(0.0f, 2.0f), Header("ピストンの押し出し速度 0.0 ~ 2.0")] private float _pushSpeed;
    [SerializeField, Range(0.0f, 2.0f), Header("ピストンの戻り速度 0.0 ~ 2.0")] private float _pullSpeed;

    private Vector3 _startPos;
    [SerializeField, Header("押し出し限界地点")] private Transform _pushLimitPos;
    private float _moveTimer;
    private bool _moveApproved;

    // pistonRangeとぐるりんの接触判定
    public bool rangeHit;
    private bool _pushing;
    private float _stopTimer;
    [SerializeField, Range(0.0f, 5.0f), Header("ピストンの停止時間 0.0 ~ 5.0")] private float _pistonStopTime;
    private bool _stopping;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        // 数値入力チェック
        if(_pushSpeed <= 0.0f || _pullSpeed <= 0.0f  || _pistonStopTime <= 0.0f)
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

    // ぐるりんがPistonRangeと接触かつPistonと接触した場合に押し出し
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>() && rangeHit)
        {
            _Gururin = other.gameObject;

            var _GururinRb = other.gameObject.GetComponent<Rigidbody>();
            _GururinRb.velocity = Vector3.zero;
            _GururinRb.angularVelocity = Vector3.zero;
            //押し出しされるようにする
            _GururinRb.constraints = RigidbodyConstraints.FreezeRotationX;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
        {
            if(_Gururin != null)
            {
                var _GururinRb = other.gameObject.GetComponent<Rigidbody>();
                _GururinRb.constraints = RigidbodyConstraints.FreezePositionZ | 
                                                         RigidbodyConstraints.FreezeRotationX |
                                                         RigidbodyConstraints.FreezeRotationY;

                _Gururin = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ピストン移動
        if (_moveApproved)
        {
            switch (_pushing)
            {
                case true:
                    MovePiston(_startPos, _pushLimitPos.position, _pushSpeed);
                    break;

                case false:
                    MovePiston(_pushLimitPos.position, _startPos, _pullSpeed);
                    break;
            }
        }

        // ピストン停止
        if (_stopping)
        {
            _stopTimer += Time.deltaTime;

            if (_stopTimer >= _pistonStopTime)
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

    // PistonRangeにぐるりんがいるかどうかの判定
    public void RangeHit(bool hit)
    {
        switch (hit)
        {
            case true:
                rangeHit = true;
                break;

            case false:
                rangeHit = false;
                break;
        }
    }
}
