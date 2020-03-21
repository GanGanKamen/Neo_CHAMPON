using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動オブジェクトの移動処理
/// </summary>

public class MoveObject : MonoBehaviour
{
    [SerializeField] [Header("ゴール")] private GoalDirecting goalDirecting;
    public enum MoveType
    {
        Straight,
        Roop
    }
    [Header("オブジェクトの動き方")] public MoveType moveType;
    [SerializeField] [Header("オブジェクトの移動限界地点")] private Transform moveLimitPos;
    [SerializeField] [Header("オブジェクトの移動速度 0.1~2.0")] [Range(0.1f, 2.0f)] private float moveSpeed;
    [SerializeField] [Header("オブジェクトの移動を停止")] private bool canStop;

    private StartCall _startCall;
    private RespawnZone _respawnZone;
    private Vector3 _startPos;
    private float _moveTimer;
    private bool _reachesLimitPos;

    // Start is called before the first frame update
    void Start()
    {
        _startCall = GameObject.Find("StartGoalDirectingCanvas/StartCall").GetComponent<StartCall>();
        _startPos = transform.position;
        _respawnZone = GetComponent<RespawnZone>() ?? null;
        if (moveLimitPos != null)
        {
            var moveLimitPosMesh = moveLimitPos.GetComponent<MeshRenderer>();
            moveLimitPosMesh.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 即死ゾーンにアタッチされているときぐるりんが即死ゾーンと接触したら移動を停止
        var deadZoneStop = _respawnZone != null ? _respawnZone.CanStop : false;
        if (canStop || deadZoneStop) return;

        switch (moveType)
        {
            case MoveType.Straight:
                // スタートコール中またはゴールした時は移動しない
                if (_startCall.HasStartCalled || goalDirecting.ReachesGoal) return;
                if (transform.position != moveLimitPos.position)
                {
                    _moveTimer += Time.deltaTime;
                }
                // moveLimitPosに向けて移動
                var movePos = Vector3.Lerp(_startPos, moveLimitPos.position, moveSpeed * 0.1f * _moveTimer);
                transform.position = movePos;
                break;

            case MoveType.Roop:
                // ループ移動
                switch (_reachesLimitPos)
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

    // ループ移動処理
    void RoopMove(Vector3 startPos, Vector3 targetPos)
    {
        var movePos = Vector3.Lerp(startPos, targetPos, moveSpeed * _moveTimer);
        transform.position = movePos;

        // 即死ゾーンがtargetPosに着いたらタイマーを初期化
        if (transform.position == targetPos)
        {
            _moveTimer = 0.0f;
            // 移動方向を反転
            _reachesLimitPos = !_reachesLimitPos;
        }
        else
        {
            _moveTimer += Time.deltaTime;
        }
    }

    // 外部から速度を変化させたいときに呼ぶ
    public void SpeedSetting(float resetSpeed)
    {
        moveSpeed = resetSpeed;
    }
}
