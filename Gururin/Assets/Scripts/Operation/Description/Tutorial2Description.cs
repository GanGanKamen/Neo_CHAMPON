using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tutorial2Description : MonoBehaviour
{
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (conversationController.textFeed[1])
        {
            conversationController.IsConversation = false;

            flagManager.velXFixed = false;
            //ぐるりんの移動を許可
            flagManager.moveStop = false;
            //GameControllerを表示する
            flagManager.pressParm = true;

            if (!video.isPlaying) video.Play();
            //このオブジェクトを非表示にする
            this.gameObject.SetActive(false);
        }
    }
}
