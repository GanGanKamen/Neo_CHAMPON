using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBossMove : MonoBehaviour
{

    private Vector2 _nowBossPos, _recodePos;
    [SerializeField] private float _bossSpeed;  //ボスの移動速度
    [SerializeField] private float _waitBiting; //噛むまでの待機時間(ボスの移動時間)

    [SerializeField] private Animator mouthAnim;
    public bool isMove, animPlay;

    private MaskActive _maskActive;

    private GameObject _gururin;
    private float _distance;

    private Cinemachine.CinemachineImpulseSource _impulse;

    // Start is called before the first frame update
    void Start()
    {
        _maskActive = GameObject.Find("BossMouth").GetComponent<MaskActive>();
        _impulse = GetComponent<Cinemachine.CinemachineImpulseSource>();

        _gururin = GameObject.Find("Gururin");

        //mouthAnim.Play("Idle");
        _nowBossPos = transform.position;
        _recodePos = _nowBossPos;
    }

    // Update is called once per frame
    void Update()
    {
        var gururinPos = _gururin.transform.position;

        //ぐるりんとの距離を取得
        _distance = Vector3.Distance(gururinPos, transform.position);

        //距離に応じてボスの速度を変化
        if (30.0f >= _distance)
        {
            _bossSpeed = 0.05f;
            _waitBiting = 5.0f;
        }
        else if(_distance >= 50.0f)
        {
            _bossSpeed = 0.25f;
            _waitBiting = 15.0f;
        }
        else if(_distance >= 40.0f)
        {
            _bossSpeed = 0.15f;
            _waitBiting = 10.0f;
        }

        //距離が離れているときに振動を弱める
        if (25.0f >= _distance)
        {
            _impulse.m_ImpulseDefinition.m_AmplitudeGain = 0.2f;
        }
        else if (32.5f >= _distance)
        {
            _impulse.m_ImpulseDefinition.m_AmplitudeGain = 0.1f;
        }
        else
        {
            _impulse.m_ImpulseDefinition.m_AmplitudeGain = 0.0f;
        }

        //口の動きのアニメーションを実行
        if (animPlay && _maskActive.animEnd == false)
        {
            _nowBossPos.x += 0.0f;
            //mouthAnim.Play("MouthMove");
            //mouthAnim.SetTrigger("MouthMove");
            mouthAnim.SetBool("MouthMove", true);
            //カメラを揺らす
            if (_maskActive.startImpulse)
            {
                _impulse.GenerateImpulse();
            }
        }
        //Bossを移動させる
        else if (isMove)
        {
            animPlay = false;
            //mouthAnim.Play("Idle");
            //mouthAnim.SetTrigger("BossMove");
            mouthAnim.SetBool("MouthMove", false);
            //ボスをX軸方向へ一定速度で移動
            _nowBossPos.x += _bossSpeed;
            transform.position = _nowBossPos;
        }

        //一定以上ボスが動いたら口のアニメーションを実行
        if(_nowBossPos.x >= _recodePos.x + _waitBiting)
        {
            animPlay = true;
            _recodePos = _nowBossPos;
        }
    }
}
