using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBossMove : MonoBehaviour
{
    private Vector2 _nowBossPos, _recodePos;
    [SerializeField] Animator mouthAnim;
    public bool isMove, animPlay;

    private MaskActive _maskActive;

    // Start is called before the first frame update
    void Start()
    {
        _maskActive = GameObject.Find("BossMouth").GetComponent<MaskActive>();

        mouthAnim.Play("Idle");
        _nowBossPos = transform.position;
        _recodePos = _nowBossPos;
    }

    // Update is called once per frame
    void Update()
    {
        //口の動きのアニメーションを実行
        if (animPlay && _maskActive.animEnd == false)
        {
            _nowBossPos.x += 0.0f;
            mouthAnim.Play("MouthMove");
        }
        //Bossを移動させる
        else if (isMove)
        {
            animPlay = false;
            mouthAnim.Play("Idle");
            _nowBossPos.x += 0.02f;
            transform.position = _nowBossPos;
        }

        //一定以上ボスが動いたら口のアニメーションを実行
        if(_nowBossPos.x >= _recodePos.x + 3.0f)
        {
            animPlay = true;
            _recodePos = _nowBossPos;
        }
    }
}
