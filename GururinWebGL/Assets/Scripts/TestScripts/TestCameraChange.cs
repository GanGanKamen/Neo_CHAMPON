using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class TestCameraChange : MonoBehaviour
{

    public GameObject[] vcam;
    private bool vcamChange;

    

    private FlagManager flagManager;

    private CanvasGroup P1;
    private CanvasGroup P2;

    public ConversationController conversationController;

    // Start is called before the first frame update
    void Start()
    {
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        P1 = GameObject.Find("P1").GetComponent<CanvasGroup>();
        P2 = GameObject.Find("P2").GetComponent<CanvasGroup>();

        
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
            vcamChange = true;

            conversationController.IsConversation = true;

            P1.alpha = 1.0f;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vcamChange)
        {
            //カメラを移動
            vcam[0].SetActive(true);

            //テキスト起動フラグ

            //テキストが送られたとき
            //テキスト関係のスクリプトを参照する?
            //if(テキストが送られたときに出すフラグ1 == true)
            if (conversationController.textFeed[0])
            {
                P1.alpha = 0.0f;
                P2.alpha = 1.0f;
                
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


                P2.alpha = 0.0f;

                vcamChange = false;
                flagManager.velXFixed = false;
                //ぐるりんの移動を許可
                flagManager.moveStop = false;
                //GameControllerを表示する
                flagManager.pressParm = true;

                //このオブジェクトを非表示にする
                this.gameObject.SetActive(false);
            }
        }
    }
}
