using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentenceScript : MonoBehaviour
{
    [SerializeField] ConversationController conversationController;
    public string[] sentences;


    // Start is called before the first frame update
    void Start()
    {
        sentences[0] = "こんにちは！東京工科大学のオープンキャンパスへようこそ！";
        sentences[1] = "このゲームは今学生たちが開発中の「ぐるりんと不思議な箱」というゲームだよ";
        sentences[2] = "主人公は歯車のぐるりん！私はぐるりんをサポートするハカセです！";
        sentences[3] = "さっそくゲームをプレイしてみましょう";
    }

    // Update is called once per frame
    void Update()
    {
        /*
        for (int i = 0; i < sentences.Length; i++)
        {
            conversationController.sentences[i] = sentences[i];
        }*/
    }
}
