using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// ゴール演出の動作処理
/// </summary>

// ☆は未実装の処理部分

public class GoalDirecting : MonoBehaviour
{
    [SerializeField] private GameObject stageClear;
    [SerializeField] private GameObject mainVCam;
    [SerializeField] private GameObject goalCamera;
    [SerializeField] [Header("カメラが近づく速度 1.0~30.0")] [Range(_limitLowerSpeed, 30.0f)] private float zoomInSpeed;

    private GameObject _Gururin;
    private CanvasGroup _stageClearCanvasGroup;
    private const float _limitLowerSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _Gururin = GameObject.FindWithTag("Player");

        _stageClearCanvasGroup = stageClear.GetComponent<CanvasGroup>();
        _stageClearCanvasGroup.alpha = 0.0f;

        var goalCameraCVC = goalCamera.GetComponent<CinemachineVirtualCamera>();
        goalCameraCVC.m_Priority = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ☆もしメインカメラが切り替わっていたらどうするかは後で考える
        var mainCameraCVC = mainVCam.GetComponent<CinemachineVirtualCamera>();
        var mainCameraView = mainCameraCVC.m_Lens.FieldOfView;
        var goalCameraCVC = goalCamera.GetComponent<CinemachineVirtualCamera>();
        goalCameraCVC.m_Follow = _Gururin.transform;
        // goalCameraのPriorityをmainCameraより1高くする
        goalCameraCVC.m_Priority = mainCameraCVC.m_Priority + 1;
        goalCameraCVC.m_Lens.FieldOfView = mainCameraView;

        StartCoroutine(Goal(mainCameraCVC, goalCameraCVC));
    }

    IEnumerator Goal(CinemachineVirtualCamera mainCameraCVC, CinemachineVirtualCamera goalCameraCVC)
    {
        // ☆ぐるりんの表情を「ニコニコ」に変更

        var GururinRb = _Gururin.GetComponent<Rigidbody>();
        // 移動停止
        GururinRb.velocity = Vector3.zero;
        GururinRb.angularVelocity = Vector3.zero;

        var gururinBase = _Gururin.GetComponent<GanGanKamen.GururinBase>();
        // 操作不許可(リスタート時どこかでSeparateGimmickを呼ぶ必要がある)
        gururinBase.AttackToGimmick();

        var mainCameraHalfView = mainCameraCVC.m_Lens.FieldOfView / 2.0f;
        // カメラをズームイン
        while (goalCameraCVC.m_Lens.FieldOfView > mainCameraHalfView)
        {
            goalCameraCVC.m_Lens.FieldOfView -= Time.deltaTime * zoomInSpeed;
            yield return null;
        }

        yield return null;

        _stageClearCanvasGroup.alpha = 1.0f;

        // ☆リザルト画面表示
        Debug.Log("リザルト画面表示");
    }
}
