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
    [SerializeField] private GameObject readyPrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject mainVCam;
    [SerializeField] private GameObject startCamera;
    [SerializeField] [Header("スタートコールの表示待機時間")] private float waitDisplayTime;
    [SerializeField] [Header("スタートまでの時間")] private float startTime;
    [SerializeField] [Header("スタートコールの表示終了時間")] private float endDisplayTime;
    [SerializeField] [Header("カメラが引く速度 1.0~30.0")] [Range(_limitLowerSpeed, 30.0f)] private float zoomOutSpeed;

    private GameObject _Gururin;
    private GameObject[] _images;
    private const float _limitLowerSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _Gururin = GameObject.FindWithTag("Player");

        var readyImage = Instantiate(readyPrefab);
        ImageSetting(readyImage);
        var startImage = Instantiate(startPrefab);
        ImageSetting(startImage);
        _images = new GameObject[] { readyImage, startImage };

        var readyCanvasGroup = readyImage.GetComponent<CanvasGroup>();
        readyCanvasGroup.alpha = 0.0f;
        var startCanvasGroup = startImage.GetComponent<CanvasGroup>();
        startCanvasGroup.alpha = 0.0f;

        var mainCameraCVC = mainVCam.GetComponent<CinemachineVirtualCamera>();
        var startCameraCVC = StartCameraSetting(mainCameraCVC);

        // ☆フェードイン終了通知を受ける

        // (終了通知を受けたら)コルーチン起動
        StartCoroutine(StartCalling(readyCanvasGroup, startCanvasGroup, mainCameraCVC, startCameraCVC));
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
            startCamera.SetActive(false);
        }

        // カメラが引き終わってからn秒待つ(待つ必要が無ければ消してretune)
        yield return new WaitForSeconds(startTime);

        // 操作許可
        playerCtrl.PermitControll();

        // Start画像を表示
        readyCanvasGroup.alpha = 0.0f;
        startCanvasGroup.alpha = 1.0f;

        yield return new WaitForSeconds(endDisplayTime);

        // 画像を削除
        for(int i = 0; i < _images.Length; i++)
        {
            Destroy(_images[i]);
        }
    }

    // 画像の設定
    private void ImageSetting(GameObject image)
    {
        image.transform.SetParent(transform);
        image.transform.localPosition = Vector3.zero;
        image.transform.localScale = Vector3.one;
    }

    // スタートカメラの設定
    private CinemachineVirtualCamera StartCameraSetting(CinemachineVirtualCamera mainCameraCVC)
    {
        var startCameraCVC = startCamera.GetComponent<CinemachineVirtualCamera>();
        startCameraCVC.m_Follow = _Gururin.transform;
        // startCameraのPriorityをmainCameraより1高くする
        startCameraCVC.m_Priority = mainCameraCVC.m_Priority + 1;
        startCameraCVC.m_Lens.FieldOfView = mainCameraCVC.m_Lens.FieldOfView / 2.0f;

        return startCameraCVC;
    }
}