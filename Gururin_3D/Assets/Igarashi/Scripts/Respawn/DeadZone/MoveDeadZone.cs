using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 即死ゾーンの移動処理
/// </summary>

public class MoveDeadZone : MonoBehaviour
{
    [SerializeField] private GoalDirecting goalDirecting;
    public enum MoveType
    {
        Straight,
        Roop
    }
    [Header("即死ゾーンの動き方")] public MoveType moveType;
    [SerializeField] [Header("MoveType:Roop時の移動限界地点")] private Transform moveLimitPos;
    [SerializeField] [Header("即死ゾーンの移動速度 0.1~2.0")] [Range(0.1f, 2.0f)] private float moveSpeed;
    [SerializeField] [Header("即死ゾーンの移動を停止")] private bool canStop;

    private Rigidbody _rigidbody;
    private StartCall _startCall;
    private RespawnZone _respawnZone;
    private Vector3 _startPos;
    private float _roopTimer;
    private bool _collidesLimitPos;

    // Start is called before the first frame update
    void Start()
    {
        _startCall = GameObject.Find("StartGoalDirectingCanvas/StartCall").GetComponent<StartCall>();

        if (moveType == MoveType.Roop)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _respawnZone = GetComponent<RespawnZone>();
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
        if (canStop || _respawnZone.CanStop) return;

        switch (moveType)
        {
            case MoveType.Straight:
                // スタートコール中またはゴールした時は移動しない
                if (_startCall.HasStartCalled || goalDirecting.ReachesGoal) return;
                transform.Translate(moveSpeed * 0.1f, 0.0f, 0.0f);
                break;

            case MoveType.Roop:
                // ループ移動
                switch (_collidesLimitPos)
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
    void RoopMove(Vector3 pistonPos, Vector3 targetPos)
    {
        var movePos = Vector3.Lerp(pistonPos, targetPos, moveSpeed * _roopTimer);
        _rigidbody.MovePosition(movePos);

        // 即死ゾーンがtargetPosに着いたらタイマーを初期化
        if (transform.position == targetPos)
        {
            _roopTimer = 0.0f;
            // 移動方向を反転
            switch (_collidesLimitPos)
            {
                case true:
                    _collidesLimitPos = false;
                    break;

                case false:
                    _collidesLimitPos = true;
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
}
