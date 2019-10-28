using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject _flag, _animFlag;

    public bool animPlay;

    private PlayerMove _playerMove;
    private CriAtomSource _source;
    private Animator _flagAnim;

    // Start is called before the first frame update
    void Awake()
    {
        _playerMove = GameObject.Find("Gururin").GetComponent<PlayerMove>();
        _source = GetComponent<CriAtomSource>();
        _flagAnim = _animFlag.GetComponent<Animator>();

        _source.volume = 0.5f;

        animPlay = false;

        if(_flag != null)
        {
            _flag.SetActive(false);
        }

        _animFlag.SetActive(false);

        //ぐるりんの残機が減った時かつ中間地点に到達していた時にスタート位置を変更
        if (RemainingLife.life != RemainingLife.beforeBossLife && RemainingLife.waypoint)
        {
            RemainingLife.startPos = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (RemainingLife.waypoint == false)
            {
                animPlay = true;
                _source.Play();

                //アニメーション再生
                if (animPlay)
                {
                    _animFlag.SetActive(true);
                    _flagAnim.Play("Flag");
                }
            }

            //中間地点を設定
            RemainingLife.waypoint = true;
        }
    }

    private void Update()
    {
        //中間地点に到達して残機が減った時、アニメーションしない旗を出現
        if (_flag != null && RemainingLife.waypoint && animPlay == false)
        {
            _flag.SetActive(true);
        }
    }
}
