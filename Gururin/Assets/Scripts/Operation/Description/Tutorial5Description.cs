using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Video;
public class Tutorial5Description : MonoBehaviour
{
    public GameObject vcam;
    private bool vcamChange;

    private FlagManager flagManager;

    public ConversationController conversationController;

    public VideoPlayer video;
    // Start is called before the first frame update
    void Start()
    {
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            flagManager.velXFixed = true;
            //ぐるりんの動きを止める
            flagManager.moveStop = true;
            //GameControllerを非表示にする
            flagManager.pressParm = false;

            conversationController.IsConversation = true;
            conversationController.preSentenceNum--;
            vcamChange = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vcamChange)
        {
            //カメラを移動
            vcam.SetActive(true);

            //テキストが送られたとき
            if (conversationController.textFeed[2])
            {
                //カメラの位置を元に戻す
                vcam.SetActive(false);
                conversationController.IsConversation = false;

                vcamChange = false;
                flagManager.velXFixed = false;
                //ぐるりんの移動を許可
                flagManager.moveStop = false;
                //GameControllerを表示する
                flagManager.pressParm = true;

                conversationController.IsConversation = false;
                //このオブジェクトを非表示にする
                if (!video.isPlaying) video.Play();
                gameObject.SetActive(false);
            }
        }
    }
}
