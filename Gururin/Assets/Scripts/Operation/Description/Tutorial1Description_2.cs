using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class Tutorial1Description_2 : MonoBehaviour
{
    [SerializeField] ConversationController conversationController;
    [SerializeField] VideoPlayer video;
    private FlagManager flagManager;
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

            conversationController.feedin = true;
            conversationController.IsConversation = true;
            if(conversationController.currentSentenceNum <= 1)conversationController.currentSentenceNum++;
            conversationController.colorMode = false;
            if(!video.isPlaying) video.Play();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            conversationController.feedout = true;
            conversationController.IsDescription = false;
            conversationController.colorMode = false;
            this.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(conversationController.IsConversation == false && flagManager.velXFixed&& flagManager.moveStop)
        {
            flagManager.velXFixed = false;
            //ぐるりんの動きを止める
            flagManager.moveStop = false;
        }
    }
}
