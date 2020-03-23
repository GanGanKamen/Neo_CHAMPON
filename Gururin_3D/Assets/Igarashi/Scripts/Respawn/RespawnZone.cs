using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

/// <summary>
/// リスポーン&即死ゾーン
/// </summary>

public class RespawnZone : MonoBehaviour
{
    public bool CanStop { get { return _canStop; } }

    public enum ObjectType
    {
        Respawn,
        DeadZone
    }
    [SerializeField] [Header("オブジェクトのタイプ")] ObjectType objectType;

    private GameObject _Gururin;
    private CameraManager _cameraManager;
    private StartCall _startCall;
    private Respawn _respawn;
    private bool _canStop;

    // Start is called before the first frame update
    void Start()
    {
        switch (objectType)
        {
            case ObjectType.Respawn:
                _respawn = GameObject.Find("RespawnManager").GetComponent<Respawn>();
                break;

            case ObjectType.DeadZone:
                _cameraManager = GameObject.Find("CameraSet").GetComponent<CameraManager>();
                _startCall = GameObject.Find("StartGoalDirectingCanvas/StartCall").GetComponent<StartCall>();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
        {
            switch (objectType)
            {
                case ObjectType.Respawn:
                    // リスポーン地点にリスポーン
                    _respawn.RespawnSetting();
                    break;

                case ObjectType.DeadZone:
                    _canStop = true;

                    _Gururin = other.gameObject;
                    var playerFace = _Gururin.GetComponentInChildren<PlayerFace>();
                    // 驚き顔に変更
                    playerFace.Surprise();

                    var deadCameraCVC = _cameraManager.CameraSetting(_cameraManager.deadCamera);
                    // 死亡演出
                    StartCoroutine(DeadDirecting(deadCameraCVC));
                    break;
            }
        }
    }

    IEnumerator DeadDirecting(CinemachineVirtualCamera deadCameraCVC)
    {
        var GururinRb = _Gururin.GetComponent<Rigidbody>();
        // 移動停止
        GururinRb.velocity = Vector3.zero;
        GururinRb.angularVelocity = Vector3.zero;
        GururinRb.isKinematic = true;

        var playerCtrl = _Gururin.GetComponent<GanGanKamen.PlayerCtrl>();
        // 操作不許可(リスタート時どこかでPlayerCtrl.PermitControll()を呼ぶ必要がある)
        playerCtrl.ProhibitControll();

        // カメラをズームイン
        while (deadCameraCVC.m_Lens.FieldOfView > _cameraManager.deadCameraView)
        {
            deadCameraCVC.m_Lens.FieldOfView -= Time.deltaTime * _cameraManager.deadCameraZoomInSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);

        // 即死ゾーンと接触したシーン名を格納
        GanGanKamen.GameSystem.beforeSceneName = SceneManager.GetActiveScene().name;
        // シーンをリロード
        SceneManager.LoadScene(GanGanKamen.GameSystem.nowSceneName);
    }
}