using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 巻き上げオブジェクトの限界値判定用空スクリプト
/// </summary>

public class HoistLimit : MonoBehaviour
{
    [SerializeField] [Multiline(2)] private string memo = "巻き上げオブジェクトの限界値に設定したい\n" +
                                                                                    "場所にこのオブジェクトを配置してください";

    // Start is called before the first frame update
    void Start()
    {
        var limitPosMesh = GetComponent<MeshRenderer>();
        limitPosMesh.enabled = false;
    }
}
