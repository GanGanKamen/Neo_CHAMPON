using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller1Description : MonoBehaviour
{

    public GameObject[] vcam;
    public bool vcamChange;
    private bool _reDescription;

    public GameObject wind;

    private FlagManager flagManager;
    public ConversationController conversationController;

    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        
        vcamChange = false;
        boxCollider = GetComponent<BoxCollider2D>();
        if (RemainingLife.life < RemainingLife.maxLife)
        {
            _reDescription = true;
        }
        else
        {
            _reDescription = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Stop();

            vcamChange = true;
            conversationController.preSentenceNum = -1;
            boxCollider.enabled = false;
        }

    }

    void Stop()
    {
        flagManager.velXFixed = true;
        //ぐるりんの動きを止める
        flagManager.moveStop = true;
        //GameControllerを非表示にする
        flagManager.pressParm = false;
        //ナビゲーションを表示する
        conversationController.IsConversation = true;
    }

    private void Move()
    {
        //カメラの位置を元に戻す
        for (int i = 0; i < vcam.Length; i++)
        {
            vcam[i].SetActive(false);
        }

        //ナビゲーションを非表示にする(中断)
        conversationController.IsConversation = false;

        flagManager.velXFixed = false;
        //ぐるりんの移動を許可
        flagManager.moveStop = false;
        //GameControllerを表示する
        flagManager.pressParm = true;

        //このオブジェクトを非表示にする
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //残機が減っていたらナビゲーションをしない
        if (_reDescription)
        {
            gameObject.SetActive(false);
        }

        if (vcamChange)
        {
            //テキストが送られたとき
            if (conversationController.textFeed[0])
            {
                //カメラを移動
                vcam[0].SetActive(true);
            }

            //さらにテキストが送られたとき
            else if (conversationController.textFeed[1])
            {
                //カメラをさらに移動
                vcam[1].SetActive(true);
            }

            if (conversationController.textFeed[2])
            {
                Move();

                vcamChange = false;
            }
        }

        //風が出たときにテキスト再表示
        else if (wind.activeInHierarchy)
        {
            vcam[2].SetActive(true);
            Stop();

            if (conversationController.textFeed[4])
            {
                Move();
            }
        }
    }
}