using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tutorial1Description : MonoBehaviour
{

    [SerializeField] ConversationController conversationController;
    private bool start = false;
    [Range(1, 2)] public int num;
    public VideoPlayer[] videos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            start = true;
            if (num == 1)
            {
                videos[0].Play();
            }
            else
            {
                StartCoroutine(nextText());
            }
        }
           
    }

    private IEnumerator nextText()
    {
        videos[1].Play();
        
        string finalText = "";
        if (LanguageSwitch.language == LanguageSwitch.Language.Japanese || LanguageSwitch.language == LanguageSwitch.Language.English)
        {
            finalText = conversationController.sentences[conversationController.currentSentenceNum].TextOutPut().Replace("　", "\n\n");
        }
        else
        {
            finalText = conversationController.sentences[conversationController.currentSentenceNum].TextOutPut().Replace("　", "\n");
        }
        while (conversationController.Text.text != finalText)
        {
            yield return null;
        }
        conversationController.StopAll();
        conversationController.currentSentenceNum = 3;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            conversationController.IsDescription = true;
            if (conversationController.IsConversation == false)
            {
                if(start)
                {
                    conversationController.sendtext = true;
                    start = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            conversationController.feedout = true;
            conversationController.IsDescription = false;
            this.gameObject.SetActive(false);

        }
    }

            // Update is called once per frame
    void Update()
    {
    }
}
