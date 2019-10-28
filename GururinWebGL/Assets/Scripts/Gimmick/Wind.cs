using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 風の当たり判定の表示切替
/// </summary>

public class Wind : MonoBehaviour
{
    //SetActiveがTrueになった時
    private void OnEnable()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    //SetActiveがfalseになった時
    private void OnDisable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
