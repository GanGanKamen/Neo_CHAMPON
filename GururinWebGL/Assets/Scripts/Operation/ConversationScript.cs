using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConversationScript : MonoBehaviour
{
    [SerializeField] ConversationController conversationController;
    private CanvasGroup canvasGroup;

    public bool IsStart;
    public bool IsDisplay;

    public Font font0, font1;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        IsStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (LanguageSwitch.language)
        {
            case LanguageSwitch.Language.Japanese:
                text.font = font0;
                break;
            case LanguageSwitch.Language.English:
                text.font = font0;
                break;
            default:
                text.font = font1;
                break;
        }

        if (conversationController.IsConversation || conversationController.IsDescription)
        {
            /*this.GetComponent<Text>().text =
                conversationController.sentences[conversationController.currentSentenceNum].TextOutPut();*/

            if (IsStart && conversationController.IsConversation)
            {
                conversationController.feedin = true;
                IsStart = false;
            }
        }

        if (conversationController.feedout)
        {
            conversationController.textFeed
                [conversationController.currentSentenceNum] = true;


            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= 0.05f;
            }
            else
            {
                if (conversationController.IsConversation)
                {
                    if (conversationController.sentences.Length - 1 > conversationController.currentSentenceNum)
                    {
                        conversationController.currentSentenceNum++;
                        conversationController.feedout = false;
                        conversationController.feedin = true;
                        Debug.Log("a");
                    }
                    else if (conversationController.sentences.Length - 1 == conversationController.currentSentenceNum)
                    {
                        conversationController.feedout = false;

                        conversationController.IsConversation = false;
                    }
                }
                else
                {
                    if (conversationController.sentences.Length - 1 > conversationController.currentSentenceNum)
                    {
                        //conversationController.StopAll();
                        if (SceneManager.GetActiveScene().name == "Tutorial-1")
                        {
                            if (conversationController.currentSentenceNum != 2)
                            {
                                conversationController.currentSentenceNum++;
                            }
                            else
                            {
                                conversationController.feedout = false;
                            }
                            
                            /*
                             if(conversationController.currentSentenceNum != 2)
                            {
                                conversationController.currentSentenceNum++;
                            }
                            else
                            {
                                conversationController.feedout = false;
                                conversationController.feedin = true;
                            }*/
                        }

                        else conversationController.feedout = false;
                        Debug.Log("b");
                    }
                    else if (conversationController.sentences.Length - 1 == conversationController.currentSentenceNum)
                    {
                        conversationController.feedout = false;
                    }
                }
            }
        }

        if (conversationController.feedin)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += 0.05f;
            }
            else
            {
                conversationController.feedin = false;
            }
        }
        if (!conversationController.IsConversation)
        {
            IsStart = true;
        }
    }
}
