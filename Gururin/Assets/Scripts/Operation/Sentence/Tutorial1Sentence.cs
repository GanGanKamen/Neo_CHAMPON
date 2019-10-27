using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1Sentence : MonoBehaviour
{

    [SerializeField] ConversationController conversationController;
    public string[] sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences[0] = "オープンキャンパスへようこそ！";
        sentences[1] = "これは「ぐるりんと不思議な箱」 というゲームだよ！";
        sentences[2] = "主人公は歯車のぐるりん！ 私はぐるりんをサポートする ハカセです！";
        sentences[3] = "さっそくゲームをプレイして いこう！";
        sentences[4] = "まずは移動してみよう！ 画面を指でぐるぐる回せば 進めるよ！";
        sentences[5] = "時計回りにぐるぐるすると右に 反時計回りにぐるぐるすると左に進むよ！";
        sentences[6] = "上手！...ん？前を見て！ 段差があるね！上にフリックして ジャンプで進もう！";
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
