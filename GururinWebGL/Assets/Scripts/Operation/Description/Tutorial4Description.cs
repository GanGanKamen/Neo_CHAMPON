using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class Tutorial4Description : MonoBehaviour
{
    public GameObject[] vcam;
    private bool vcamChange;
    
    private FlagManager flagManager;

    public ConversationController conversationController;

    public VideoPlayer video;
    // Start is called before the first frame update
    void Start()
    {
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();


        vcamChange = false;
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
            vcam[0].SetActive(true);

            //テキストが送られたとき
            if (conversationController.textFeed[1])
            {

                //カメラをさらに移動
                vcam[1].SetActive(true);
            }

            //さらにテキストが送られたとき
            //if(テキストが送られたときに出すフラグ2 == true)
            if (conversationController.textFeed[2])
            {
                //カメラの位置を元に戻す
                for (int i = 0; i < vcam.Length; i++)
                {
                    vcam[i].SetActive(false);
                }
                conversationController.IsConversation = false;
                
                vcamChange = false;
                flagManager.velXFixed = false;
                //ぐるりんの移動を許可
                flagManager.moveStop = false;
                //GameControllerを表示する
                flagManager.pressParm = true;

                if (!video.isPlaying) video.Play();
                //このオブジェクトを非表示にする
                gameObject.SetActive(false);
            }
        }
    }
}
