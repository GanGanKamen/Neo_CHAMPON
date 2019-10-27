using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial4Sentence : MonoBehaviour
{
    [SerializeField] ConversationController conversationController;
    public string[] sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences[0] = "あんなに高い所に扉が！";
        sentences[1] = "足場もないし、どうしたら いいかなぁ...";
        sentences[2] = "あっ！ よく見たら天井にデコボコが あるね！";
        sentences[3] = "ぐるりんは天井のデコボコに くっつけるんだ！";
        sentences[4] = "まずはデコボコにくっついて そこから画面をぐるぐるすれば 進めるよ！";
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            //conversationController.sentences[i] = sentences[i];
        }
    }
}
