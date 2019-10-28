using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 風船と接触したときに風船を装備させる
/// </summary>

public class ActiveBalloon : MonoBehaviour
{

    private GameObject _balloonCol, _ballonSpr;
    private CriAtomSource _source;
    private bool _actBal;

    private FlagManager flagManager;

    // Start is called before the first frame update
    void Start()
    {
        _balloonCol = GameObject.Find("BalloonGimmick");
        _ballonSpr = GameObject.Find("BallonSprite");
        _source = GetComponent<CriAtomSource>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();

        _actBal = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //風船が装備されていないとき
            if (_ballonSpr.GetComponent<SpriteRenderer>().enabled == false)
            {
                _source.Play();
            }

            _actBal = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_actBal)
        {
            flagManager.activeBalloon = true;
        }
    }

    private void LateUpdate()
    {
        if (_actBal)
        {
            //BallonGImmickのGravityScaleを-3にする
            _balloonCol.GetComponent<Rigidbody2D>().gravityScale = -3.0f;
            //BallonGImmickのColliderを出現
            _balloonCol.GetComponent<CapsuleCollider2D>().enabled = true;
            //BallonSpriteのSpriteを表示
            _ballonSpr.GetComponent<SpriteRenderer>().enabled = true;

            _actBal = false;
        }
    }
}
