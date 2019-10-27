using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial5Sentence : MonoBehaviour
{
    [SerializeField] ConversationController conversationController;
    public string[] sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences[0] = "壁にデコボコがあるね！";
        sentences[1] = "ぐるりんはデコボコの壁を上る ことが出来るんだ！壁を上って 壁からジャンプをしてみよう！";
        sentences[2] = "くっついている壁と逆方向に フリックするとジャンプ 出来るんだ！";
        sentences[3] = "これで扉までたどり着けるね！";
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
