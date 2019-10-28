using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller1Sentence : MonoBehaviour
{
    [SerializeField] ConversationController conversationController;
    public string[] sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences[0] = "わぁ、大きなプロペラ！";
        sentences[1] = "もしかして向こうのプロペラに 風を渡すと仕掛けが動くのかも？";
        sentences[2] = "ふむふむ、こっちの扇風機に 歯車が付いているってことは もうやることは一つだね！";
        sentences[3] = "どうやら歯車が回る力で扇風機 から風が起こせるみたい";
        sentences[4] = "この仕掛けはここだと色んなこと に使えそうだね！ おぼえておこう！";

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
