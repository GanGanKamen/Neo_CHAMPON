using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoStartCon : MonoBehaviour
{
    private FlagManager flagManager;
    [SerializeField] private ConversationController conversationController;
    [SerializeField] private int[] camChange; 
    [SerializeField] private GameObject[] vcams;
    [SerializeField] private float startWaitTime;
    // Start is called before the first frame update
    private void Awake()
    {
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    void Start()
    {
        StartCoroutine(Conversation());
    }

    // Update is called once per frame
    private IEnumerator Conversation()
    {
        flagManager.velXFixed = true;
        flagManager.moveStop = true;
        flagManager.pressParm = false;

        yield return new WaitForSeconds(startWaitTime);
        conversationController.IsConversation = true;
        conversationController.feedin = true;
        conversationController.preSentenceNum = -1;
        for (int i =0; i < camChange.Length; i++)
        {
            while(conversationController.currentSentenceNum < camChange[i])
            {
                yield return null;
            }
            vcams[i].SetActive(true);
        }
        while (conversationController.IsConversation)
        {
            yield return null;
        }
        for(int i = 0; i < vcams.Length; i++)
        {
            vcams[i].SetActive(false);
        }
        conversationController.IsConversation = false;
        conversationController.feedout = true;

        flagManager.velXFixed = false;
        //ぐるりんの移動を許可
        flagManager.moveStop = false;
        //GameControllerを表示する
        flagManager.pressParm = true;
        //このオブジェクトを非表示にする
        this.gameObject.SetActive(false);
        yield break;
    }

    void Update()
    {
        
    }
}
