using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2Sentence : MonoBehaviour
{
    [SerializeField] ConversationController conversationController;
    public string[] sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences[0] = "むむっ！前方に高い壁を発見！";
        sentences[1] = "でもよかったよ！ 足場があるね！この足場は下から すり抜けられるんだ！";
        sentences[2] = "さあ！ジャンプで飛び越えて いこう！";
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
