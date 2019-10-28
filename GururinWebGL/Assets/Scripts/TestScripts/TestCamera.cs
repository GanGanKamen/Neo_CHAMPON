using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{

    private Transform player;
    private Vector3 offset;

    void Start()
    {
        player = GameObject.Find("Gururin").transform;

        // カメラとターゲットの最初の位置関係（距離）を取得する。
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
