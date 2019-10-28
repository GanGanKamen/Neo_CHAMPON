using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTimeCount : MonoBehaviour
{

    [SerializeField] ScoreData _scoreData;
    private FlagManager _flagManager;

    private void Awake()
    {
        //このシーンに遷移されたときタイマーをリセット
        _scoreData.timer = 0.0f;
        _scoreData.restartTime = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreData.timer += Time.deltaTime;

        if (_flagManager.stageClear)
        {
            if(_scoreData.restartTime == false)
            {
                //時間を止める
                Time.timeScale = 0.0f;
                //タイマーの計測時間をクリアタイムに追加
                _scoreData.clearTime = _scoreData.timer;
                //トータルスコアにステージごとのクリアタイムを加算
                TotalClearTime.totalTime += _scoreData.timer;
                _scoreData.restartTime = true;
            }
            else
            {
                //時間を動かす
                Time.timeScale = 1.0f;
            }

            //Debug.Log(TotalClearTime.totalTime);
        }
    }
}
