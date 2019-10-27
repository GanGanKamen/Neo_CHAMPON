using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 扇風機から風を出すためのカウント
/// </summary>

public class RotationCounter : MonoBehaviour
{

    public int count; //風を出すためのカウント
    public int _maxCount; //カウントの限界値
    public float timer; //カウントをマイナスするまでのタイマー
    public bool countPlus; //カウントがプラスされたとき
    public bool minusCount; //カウントをマイナスする
    public bool fixedCount; //風が出るカウント
    public bool fanRot;
    public GameObject Wind;

    private CriAtomSource _source;
    private bool _sourcePlay;
    private float _sourceVolume;
    [SerializeField] Gamecontroller gameController;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<CriAtomSource>();
       gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();

        _sourcePlay = false;
        _sourceVolume = _source.volume;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //歯車が触れるたびにカウントを1増加
        if (other.CompareTag("Gimmick"))
        {
            timer = 0.0f;
            count ++;
            //カウントの増加を感知
            countPlus = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Gimmick"))
        {
            countPlus = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Windがアクティブ化したときにSEを鳴らす
        if (Wind.activeInHierarchy && _sourcePlay == false)
        {
            _source.Play();
            _sourcePlay = true;
        }
        else if (Wind.activeInHierarchy == false && _sourcePlay)
        {
            _source.Stop();
            _sourcePlay = false;
            _source.volume = _sourceVolume;
        }

        if (_source.volume > _sourceVolume)
        {
            _source.volume = _sourceVolume;
        }

        //minusCountがtrueの時、1秒ごとにカウントを-1
        if (gameController.isPress == false && count > 0 && minusCount)
        {
            timer += Time.deltaTime;

            if (timer > 1)
            {
                if (_sourcePlay)
                {
                    _source.volume -= 0.2f;
                }
                timer = 0.0f;
                count--;
            }
        }

        //カウントが10を超えたら風を出現させる
        if (count >= _maxCount)
        {
            Wind.SetActive(true);
            fixedCount = true;
        }
        else if(count == 0)
        {
            Wind.SetActive(false);
            fixedCount = false;
        }

        //風が出ているときに歯車を回すと風のSEの音量を上げる
        if (minusCount == false && fixedCount)
        {
            _source.volume += 0.01f;

            if (_source.volume > _sourceVolume)
            {
                _source.volume = _sourceVolume;
            }
        }

        //limitCountを超えたらカウントストップ
        if (count >= _maxCount)
        {
            count = _maxCount;
        }
    }
}
