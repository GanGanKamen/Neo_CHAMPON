using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ぐるりんの残機制御
/// </summary>

public class RemainingLife : MonoBehaviour
{

    public static int life;
    public static int maxLife;
    public static int beforeBossLife; //ボスステージに入る前のプレイヤーのライフ
    public static int bossLife;
    //中間地点の設定
    public static bool waypoint;
    //中間地点のスタート位置
    public static Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        //ぐるりんの残機 タイトル画面に戻ってきたら全機回復
        //難易度に応じて適宜変更
        life = 6;
        maxLife = life;
        waypoint = false;
    }
}
