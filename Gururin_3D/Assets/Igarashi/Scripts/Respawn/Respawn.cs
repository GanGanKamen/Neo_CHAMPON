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
    [SerializeField] private int blinkNum; // 点滅回数
    [SerializeField] private float interval;  // 点滅周期

    private GameObject _Gururin;
    private List<Renderer> _rendererList = new List<Renderer>();
    private CameraManager _cameraManager;
    private Vector3 _respawnPoint;
    [SerializeField] private int count;
    private float nextTime;
    [SerializeField] private bool _canBlink;

    // Start is called before the first frame update
    void Start()
    {
        nextTime = Time.time;

        _Gururin = GameObject.FindWithTag("Player");
        // 初期リスポーン地点を設定
        RespawnPointSetting(_Gururin.transform);

        _cameraManager = GameObject.Find("CameraSet").GetComponent<CameraManager>();

        var GururinGear = _Gururin.transform.Find("gururin").gameObject; 
        var GururinFaces = _Gururin.transform.Find("FaceManager").gameObject;
        var GururinRenderer = GururinGear.GetComponent<Renderer>();
        _rendererList.Add(GururinRenderer);
        foreach (Transform faceTransform in GururinFaces.transform)
        {
            _rendererList.Add(faceTransform.gameObject.GetComponent<Renderer>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_canBlink)
        {
            Blink();
        }
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

        count = 0;
        _canBlink = true;

        _cameraManager.MainCameraInit(_Gururin);
    }

    private void Blink()
    {
        if (Time.time > nextTime)
        {
            for (int i = 0; i < _rendererList.Count; i++)
            {
                _rendererList[i].enabled = !_rendererList[i].enabled;
                if (count == blinkNum - 1)
                {
                    _rendererList[i].enabled = true;
                    _canBlink = false;
                }
            }

            nextTime += interval;
            count++;
        }
    }
}
