using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ☆は未実装の処理部分

public class StartCall : MonoBehaviour
{
    [SerializeField] private GameObject ready;
    [SerializeField] private GameObject start;
    [SerializeField] [Header("スタートコールの表示待機時間")] private float waitDisplayTime;
    [SerializeField] [Header("スタートまでの時間")] private float startTime;
    [SerializeField] [Header("スタートコールの表示終了時間")] private float endDisplayTime;

    // Start is called before the first frame update
    void Start()
    {
        var readyCanvasGroup = ready.GetComponent<CanvasGroup>();
        readyCanvasGroup.alpha = 0.0f;
        var startCanvasGroup = start.GetComponent<CanvasGroup>();
        startCanvasGroup.alpha = 0.0f;

        // ☆フェードイン終了通知を受ける

        // (終了通知を受けたら)コルーチン起動
        StartCoroutine(StartCalling(readyCanvasGroup, startCanvasGroup));
    }

    IEnumerator StartCalling(CanvasGroup readyCanvasGroup, CanvasGroup startCanvasGroup)
    {
        // フェードイン後n秒待つ(待つ必要が無ければ消す)
        yield return new WaitForSeconds(waitDisplayTime);

        // ready画像をフェードイン(する必要が無ければ消す)
        while(readyCanvasGroup.alpha < 1.0f)
        {
            readyCanvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
        //readyCanvasGroup.alpha = 1.0f;

        // ☆カメラが引く処理

        yield return new WaitForSeconds(startTime);

        readyCanvasGroup.alpha = 0.0f;
        startCanvasGroup.alpha = 1.0f;

        yield return new WaitForSeconds(endDisplayTime);

        Destroy(gameObject);
    }
}
