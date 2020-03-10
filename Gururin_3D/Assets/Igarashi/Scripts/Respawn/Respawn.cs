using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リスポーン関係の設定
/// </summary>

public class Respawn : MonoBehaviour
{
    private GameObject _Gururin;
    private Vector3 _respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _Gururin = GameObject.FindWithTag("Player");

        // 初期リスポーン地点を設定
        RespawnPointSetting(_Gururin.transform);
    }

    // リスポーン地点を設定
    public void RespawnPointSetting(Transform respawnPoint)
    {
        if (respawnPoint.position != _respawnPoint)
        {
            _respawnPoint = respawnPoint.position;
            Debug.Log("現在のリスポーン地点" + _respawnPoint);
        }
    }

    // ぐるりんをリスポーンしたいときに呼ぶ
    public void RespawnSetting()
    {
        var gururinBase = _Gururin.gameObject.GetComponent<GanGanKamen.GururinBase>();
        // 移動操作が停止されていたら再開メソッドを呼ぶ
        if (gururinBase.IsAttachGimmick)
        {
            gururinBase.SeparateGimmick();
        }

        // 角度と位置を初期化
        _Gururin.transform.rotation = Quaternion.Euler(Vector3.zero);
        _Gururin.transform.position = _respawnPoint;
    }
}
