﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リフトギミック改良案
/// </summary>

public class LiftEx : MonoBehaviour
{
    /// <summary>
    /// リフトの移動方法をVector3.Lerpを使った形に変更
    /// 斜め方向にもリフトが移動できるように修正
    /// </summary>

    [SerializeField] private GanGanKamen.GururinBase gururinBase;
    [SerializeField] private GameObject player;
    [SerializeField] [Header("リフトの移動速度")] private float speed;

    private Vector3 firstpos;

    // 以下追加変数
    [SerializeField] private GanGanKamen.GameController gameController;
    [SerializeField] [Header("移動限界地点")] private Transform moveLimitPos; // ここのPositionに向かってリフトが移動する
    [SerializeField] [Header("移動停止時間 0.1~20.0")] [Range(0.1f, 20.0f)] private float moveStopTime;
    [SerializeField] [Header("リフトの移動を完全停止")] private bool canStop;

    private Rigidbody _rigidbody;
    private float _moveTimer;
    private float _stopTimer;
    private bool _reachesLimitPos; // 移動限界地点にたどり着いたかどうか
    private bool _canMove;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gururinBase = player.GetComponent<GanGanKamen.GururinBase>();
        // 移動限界地点のMeshを非表示にする
        var limitPosMesh = moveLimitPos.GetComponent<MeshRenderer>();
        limitPosMesh.enabled = false;

        _rigidbody = GetComponent<Rigidbody>();
        firstpos = transform.position;
        _canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canStop) return;

        switch (_canMove)
        {
            case true:
                switch (_reachesLimitPos)
                {
                    // 移動限界地点から初期地点へ移動
                    case true:
                        LiftMove(moveLimitPos.position, firstpos);
                        break;

                    // 移動限界地点から移動限界点へ移動
                    case false:
                        LiftMove(firstpos, moveLimitPos.position);
                        break;
                }
                break;

            // リフトを一時停止
            case false:
                _stopTimer += Time.deltaTime;

                if (_stopTimer >= moveStopTime)
                {
                    _stopTimer = 0.0f;
                    // 移動方向を反転
                    _reachesLimitPos = !_reachesLimitPos;
                    // リフトの移動を再開
                    _canMove = true;
                }
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var playerRb = player.GetComponent<Rigidbody>();
        // ブレーキ操作時
        if (other.CompareTag("Player") && gameController.InputLongPress)
        {
            // ぐるりんとリフトの速度を同じにする(リフトに追従する)
            // speedが高いとカメラがガクガクするので要修正
            playerRb.velocity = _rigidbody.velocity;
        }
    }

    // リフトの移動処理
    void LiftMove(Vector3 startPos, Vector3 targetPos)
    {
        // Vector3.LerpでstartPosからtargetPosへspeedで移動させる
        var movePos = Vector3.Lerp(startPos, targetPos, speed * _moveTimer);
        _rigidbody.MovePosition(movePos);

        // リフトが移動限界点に着いたらタイマーを初期化
        if (transform.position == targetPos)
        {
            _moveTimer = 0.0f;
            // リフトを停止
            _canMove = false;
        }
        else
        {
            _moveTimer += Time.deltaTime;
        }
    }
}