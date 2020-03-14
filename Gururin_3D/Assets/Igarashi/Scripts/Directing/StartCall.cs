using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// スタートコールの動作処理
/// </summary>

// ☆は未実装の処理部分

public class StartCall : MonoBehaviour
{
    public bool HasStartCalled { get { return _hasStartCalled; } } // スタートコール中かどうかの判定

    [SerializeField] private GameObject readyPrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] [Header("スタートコールの表示待機時間")] private float waitDisplayTime;
    [SerializeField] [Header("スタートまでの時間")] private float startTime;
    [SerializeField] [Header("スタートコールの表示終了時間")] private float endDisplayTime;
    [SerializeField] [Header("カメラが引く速度 1.0~30.0")] [Range(_limitLowerSpeed, 30.0f)] private float zoomOutSpeed;
    [SerializeField] [Header("スタートコール演出をスキップ")] private bool canSkip;

    private GameObject _Gururin;
    private CameraManager _cameraManager;
    private List<GameObject> _imageList = new List<GameObject>(2); // 画像を一括で削除するために格納
    private const float _limitLowerSpeed = 1.0f;
    private bool _hasStartCalled;

    // Start is called before the first frame update
    void Start()
    {
        _Gururin = GameObject.FindWithTag("Player");
        _cameraManager = GameObject.Find("CameraSet").GetComponent<CameraManager>();

        // 即死ゾーンに触れてシーンがリロードされたらスタートコールをスキップ
        if(Respawn.beforeSceneName == Respawn.nowSceneName)
        {
            canSkip = true;
        }

        switch (canSkip)
        {
            case true:
                _hasStartCalled = false;

                var playerCtrl = _Gururin.GetComponent<GanGanKamen.PlayerCtrl>();
                playerCtrl.PermitControll();

                _cameraManager.CameraInit(_cameraManager.startCamera);
                break;

            case false:
                _hasStartCalled = true;

                var readyCanvasGroup = ImageSetting(readyPrefab);
                var startCanvasGroup = ImageSetting(startPrefab);

                var mainCameraCVC = _cameraManager.mainVCamera.GetComponent<CinemachineVirtualCamera>();
                var startCameraCVC = _cameraManager.CameraSetting(_cameraManager.startCamera);

                // ☆フェードイン終了通知を受ける

                // (終了通知を受けたら)コルーチン起動
                StartCoroutine(StartCalling(readyCanvasGroup, startCanvasGroup, mainCameraCVC, startCameraCVC));
                break;
        }

        if (GameObject.FindGameObjectWithTag("System") != null)
        {
            var stageMng = GameObject.FindGameObjectWithTag("System").GetComponent<GanGanKamen.StageManager>();
            stageMng.StageGameStart();
        }
    }

    IEnumerator StartCalling(CanvasGroup readyCanvasGroup, CanvasGroup startCanvasGroup,
                                            CinemachineVirtualCamera mainCameraCVC, CinemachineVirtualCamera startCameraCVC)
    {
        var playerCtrl = _Gururin.GetComponent<GanGanKamen.PlayerCtrl>();
        // 操作不許可
        playerCtrl.ProhibitControll();

        // フェードイン後n秒待つ(待つ必要が無ければ消してretune)
        yield return new WaitForSeconds(waitDisplayTime);

        // Ready画像を表示
        readyCanvasGroup.alpha = 1.0f;

        var mainCameraView = mainCameraCVC.m_Lens.FieldOfView;
        // カメラをズームアウト
        while (startCameraCVC.m_Lens.FieldOfView < mainCameraView)
        {
            startCameraCVC.m_Lens.FieldOfView += Time.deltaTime * zoomOutSpeed;
            yield return null;
        }
        if (startCameraCVC.m_Lens.FieldOfView >= mainCameraView)
        {
            _cameraManager.startCamera.SetActive(false);
        }

        // カメラが引き終わってからn秒待つ(待つ必要が無ければ消してretune)
        yield return new WaitForSeconds(startTime);

        // 操作許可
        playerCtrl.PermitControll();
        _hasStartCalled = false;

        // Start画像を表示
        readyCanvasGroup.alpha = 0.0f;
        startCanvasGroup.alpha = 1.0f;

        yield return new WaitForSeconds(endDisplayTime);

        // 画像を削除
        for(int i = 0; i < _imageList.Count; i++)
        {
            Destroy(_imageList[i]);
        }
    }

    // 画像の設定
    private CanvasGroup ImageSetting(GameObject imagePrefab)
    {
        var image = Instantiate(imagePrefab);
        image.transform.SetParent(transform);
        image.transform.localPosition = Vector3.zero;
        image.transform.localScale = Vector3.one;
        _imageList.Add(image);

        var canvasGroup = image.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;

        return canvasGroup;
    }
}