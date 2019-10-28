using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create ScoreData")]
public class ScoreData : ScriptableObject
{
    //タイマー
    public float timer;
    //ステージのクリアタイム
    public float clearTime;
    public bool restartTime = false;
}
