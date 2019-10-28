using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3Sentence : MonoBehaviour
{

    [SerializeField] ConversationController conversationController;
    public string[] sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences[0] = "むっ！前方に壁発見！ これじゃあ通れないなぁ...";
        sentences[1] = "あっ！ちょっと待って！ 下の歯車、壁のデコボコと歯が 噛み合ってるよ！";
        sentences[2] = "こういう時は歯車にくっついて ぐるぐる回せば歯車も回るよ！";
        sentences[3] = "やった！ これで先に進めるように なったね！";
        sentences[4] = "歯車から離れるならジャンプ してね！";
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
