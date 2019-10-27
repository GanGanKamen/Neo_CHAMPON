using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial6Sentence : MonoBehaviour
{
    [SerializeField] ConversationController conversationController;
    public string[] sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences[0] = "さぁこれが最後のステージだよ！";
        sentences[1] = "今までのことを思い出してゴールまでたどり着いてみてね！";
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < sentences.Length; i++)
        {
           // conversationController.sentences[i] = sentences[i];
        }
    }
}
