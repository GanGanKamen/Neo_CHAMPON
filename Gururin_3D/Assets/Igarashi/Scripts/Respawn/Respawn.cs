using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

/// <summary>
/// リスポーン関係の設定
/// </summary>

public class Respawn : MonoBehaviour
{
    public static string beforeSceneName;
    public static string nowSceneName;

    private GameObject _Gururin;
    [SerializeField] private CameraManager _cameraManager;
    private Vector3 _respawnPoint;

    private void Awake()
    {
        if (nowSceneName != null)
        {
            beforeSceneName = nowSceneName;
        }
        nowSceneName = SceneManager.GetActiveScene().name;
    }

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
        var gururinBase = _Gururin.GetComponent<GanGanKamen.GururinBase>();
        // 移動操作が停止されていたら再開メソッドを呼ぶ
        if (gururinBase.IsAttachGimmick)
        {
            gururinBase.SeparateGimmick();
        }

        var playerFace = _Gururin.GetComponentInChildren<PlayerFace>();
        playerFace.Nomal();

        // 押し出し時に掛かるAddforceを停止(カメラのブレ防止)
        var GururinRb = _Gururin.GetComponent<Rigidbody>();
        GururinRb.velocity = Vector3.zero;

        // 角度と位置を初期化
        _Gururin.transform.rotation = Quaternion.Euler(Vector3.zero);
        _Gururin.transform.position = _respawnPoint;

        _cameraManager.MainCameraInit(_Gururin);
    }
}
