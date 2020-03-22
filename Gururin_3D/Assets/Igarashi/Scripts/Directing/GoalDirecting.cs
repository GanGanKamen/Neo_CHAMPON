using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// ゴール演出の動作処理
/// </summary>

public class GoalDirecting : MonoBehaviour
{
    public bool ReachesGoal { get { return _reachesGoal; } } // ゴールしたかどうかの判定

    [SerializeField] private GameObject stageClearPrefab;
    [SerializeField] [Header("カメラが近づく速度 1.0~30.0")] [Range(_limitLowerSpeed, 30.0f)] private float zoomInSpeed;

    private GameObject _Gururin;
    private GameObject _goalDirecting;
    private GameObject _stageClearImage;
    private CanvasGroup _stageClearCanvasGroup;
    private CameraManager _cameraManager;
    private const float _limitLowerSpeed = 1.0f;
    private bool _reachesGoal;

    // Start is called before the first frame update
    void Start()
    {
        _Gururin = GameObject.FindWithTag("Player");
        _goalDirecting = GameObject.Find("StartGoalDirectingCanvas/GoalDirecting");
        _cameraManager = GameObject.Find("CameraSet").GetComponent<CameraManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GanGanKamen.PlayerCtrl>())
        {
            _reachesGoal = true;

            _stageClearCanvasGroup = ImageSetting(stageClearPrefab);

            var mainCameraCVC = _cameraManager.mainVCamera.GetComponent<CinemachineVirtualCamera>();
            var goalCameraCVC = _cameraManager.CameraSetting(_cameraManager.goalCamera);

            StartCoroutine(Goal(mainCameraCVC, goalCameraCVC));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ゴール後、「3 !」でゴール演出削除
        if (Input.GetKeyDown(KeyCode.Alpha3) && _stageClearImage != null)
        {
            _reachesGoal = false;

            var goalCameraCVC = _cameraManager.goalCamera.GetComponent<CinemachineVirtualCamera>();
            goalCameraCVC.m_Priority = 0;

            Destroy(_stageClearImage);
        }
    }

    IEnumerator Goal(CinemachineVirtualCamera mainCameraCVC, CinemachineVirtualCamera goalCameraCVC)
    {
        // ☆ぐるりんの表情を「ニコニコ」に変更

        var GururinRb = _Gururin.GetComponent<Rigidbody>();
        // 移動停止
        GururinRb.velocity = Vector3.zero;
        GururinRb.angularVelocity = Vector3.zero;

        var playerCtrl = _Gururin.GetComponent<GanGanKamen.PlayerCtrl>();
        // 操作不許可(リスタート時どこかでPlayerCtrl.PermitControll()を呼ぶ必要がある)
        playerCtrl.ProhibitControll();

        var mainCameraHalfView = mainCameraCVC.m_Lens.FieldOfView / 2.0f;
        // カメラをズームイン
        while (goalCameraCVC.m_Lens.FieldOfView > mainCameraHalfView)
        {
            goalCameraCVC.m_Lens.FieldOfView -= Time.deltaTime * zoomInSpeed;
            yield return null;
        }

        yield return null;

        // StageClear画像を表示
        _stageClearCanvasGroup.alpha = 1.0f;

        yield return new WaitForSeconds(2.0f);

        // リザルト画面表示
        if (GameObject.FindGameObjectWithTag("System") != null)
        {
            var stageMng = GameObject.FindGameObjectWithTag("System").GetComponent<GanGanKamen.StageManager>();
            stageMng.StageClear();
        }
        yield break;
    }

    // 画像の設定
    private CanvasGroup ImageSetting(GameObject imagePrefab)
    {
        var image = Instantiate(imagePrefab);
        image.transform.SetParent(_goalDirecting.transform);
        image.transform.localPosition = Vector3.zero;
        image.transform.localScale = Vector3.one;
        _stageClearImage = image;

        var canvasGroup = image.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;

        return canvasGroup;
    }
}