using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class Tutorial1Description_2 : MonoBehaviour
{
    [SerializeField] ConversationController conversationController;
    [SerializeField] VideoPlayer video;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            conversationController.feedin = true;
            conversationController.IsDescription = true;
            conversationController.currentSentenceNum++;
            if(!video.isPlaying) video.Play();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
