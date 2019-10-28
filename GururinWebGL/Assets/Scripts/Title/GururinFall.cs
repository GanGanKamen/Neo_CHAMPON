using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GururinFall : MonoBehaviour
{

    private Rigidbody2D _rb2d;
    private bool _SEPlay;
    private Titlemove _titleMove;
    private CriAtomSource _fallSE, _jumpSE;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _titleMove = GetComponent<Titlemove>();
        _fallSE = GameObject.Find("SE_otiru(CriAtomSource)").GetComponent<CriAtomSource>();
        _jumpSE = GameObject.Find("SE_jump(CriAtomSource)").GetComponent<CriAtomSource>();

        _SEPlay = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //地面と接触したときにSEを止める
        if (other.gameObject.CompareTag("Ground"))
        {
            _fallSE.Stop();
        }
    }

    //非アクティブ時にSEを鳴らす許可をする
    private void OnDisable()
    {
        _SEPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        //落下時にSEを鳴らす
        if (_SEPlay == false)
        {
            //横からの出現時
            if (_titleMove.terms1 == 2 && _rb2d.velocity.x > 3.0f)
            {
                _jumpSE.Play();
                _SEPlay = true;
            }
            //上からの出現時
            else if ((_titleMove.terms1 == 0 || _titleMove.terms1 == 1) && _rb2d.velocity.y < -0.01f)
            {
                _fallSE.Play();
                _SEPlay = true;
            }
        }
    }
}
