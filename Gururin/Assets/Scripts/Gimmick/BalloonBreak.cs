using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 風船が割れたとき
/// </summary>

public class BalloonBreak : MonoBehaviour
{

    private GameObject _balloonCol, _ballonSpr;

    private CriAtomSource _source;
    private FlagManager flagManager;

    // Start is called before the first frame update
    void Start()
    {
        _balloonCol = GameObject.Find("BalloonGimmick");
        _ballonSpr = GameObject.Find("BallonSprite");

        _source = GetComponent<CriAtomSource>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //風船に当たったら割れるようにする
        if (other.CompareTag("Balloon"))
        {
            if (_ballonSpr.GetComponent<SpriteRenderer>().enabled == true)
            {
                _source.Play();
                //ぐるりんの顔を驚き顔にする
                StartCoroutine("SurpriseFace");
            }

            flagManager.activeBalloon = false;
            //BallonGImmickのGravityScaleを0にする
            _balloonCol.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            //BallonGImmickのColliderを非表示
            _balloonCol.GetComponent<CapsuleCollider2D>().enabled = false;
            //BallonSpriteのSpriteを非表示
            _ballonSpr.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator SurpriseFace()
    {
        flagManager.surprise_Face = true;

        yield return new WaitForSeconds(0.5f);

        flagManager.surprise_Face = false;

        yield break;
    }
}
