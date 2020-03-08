﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDeadZone : MonoBehaviour
{
    public enum MoveType
    {
        Straight,
        Roop
    }
    [Header("即死ゾーンの動き方")] public MoveType moveType;
    [SerializeField] [Header("即死ゾーンの移動速度 0.1~2.0")] [Range(0.1f, 2.0f)] private float moveSpeed;
    [SerializeField] [Header("MoveType:Roop時の移動限界地点")] private Transform moveLimitPos;
    [SerializeField] private StartCall startCall;
    [SerializeField] private GoalDirecting goalDirecting;

    [SerializeField] [Header("即死ゾーンの移動を停止")] private bool _isDeadZoneStop;

    private Rigidbody _rigidbody;
    private Vector3 _startPos;
    private float _roopTimer;
    private bool _isLimitPosCol;

    // Start is called before the first frame update
    void Start()
    {
        if (moveType == MoveType.Roop)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _startPos = transform.position;

            if (moveLimitPos != null)
            {
                var moveLimitPosMesh = moveLimitPos.GetComponent<MeshRenderer>();
                moveLimitPosMesh.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDeadZoneStop == false)
        {
            switch (moveType)
            {
                case MoveType.Straight:
                    // スタートコール中またはゴールした時は移動しない
                    if (startCall.IsStartCall || goalDirecting.IsGoal) return;
                    transform.Translate(moveSpeed * 0.1f, 0.0f, 0.0f);
                    break;

                case MoveType.Roop:
                    // ループ移動
                    switch (_isLimitPosCol)
                    {
                        case true:
                            RoopMove(moveLimitPos.position, _startPos);
                            break;

                        case false:
                            RoopMove(_startPos, moveLimitPos.position);
                            break;
                    }
                    break;
            }
        }
    }

    // ループ移動処理
    void RoopMove(Vector3 pistonPos, Vector3 targetPos)
    {
        var movePos = Vector3.Lerp(pistonPos, targetPos, moveSpeed * _roopTimer);
        _rigidbody.MovePosition(movePos);

        // 即死ゾーンがtargetPosに着いたらタイマーを初期化
        if (transform.position == targetPos)
        {
            _roopTimer = 0.0f;
            // 移動方向を反転
            switch (_isLimitPosCol)
            {
                case true:
                    _isLimitPosCol = false;
                    break;

                case false:
                    _isLimitPosCol = true;
                    break;
            }
        }
        else
        {
            _roopTimer += Time.deltaTime;
        }
    }

    // 外部から速度を変化させたいときに呼ぶ
    public void SpeedSetting(float resetSpeed)
    {
        moveSpeed = resetSpeed;
    }

    // 即死ゾーンの動きを止める
    public void DeadZoneStop()
    {
        _isDeadZoneStop = true;
    }

    // 再度即死ゾーンを動かす
    public void DeadZoneReboot()
    {
        _isDeadZoneStop = false;
    }
}