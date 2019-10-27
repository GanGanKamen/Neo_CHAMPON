using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public ConversationController conversationController;
    public FlagManager flagManager;
    public GameClear gameClear;
    public Data data;
    public CanvasGroup canvasGroup;

    public float scenetime;
    public bool check = false, result = false;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("ConversationController")!=null) conversationController = GameObject.Find("ConversationController").GetComponent<ConversationController>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        gameClear = GameObject.Find("NextStage").GetComponent<GameClear>();
        if (GameObject.Find("Data") != null) data = GameObject.Find("Data").GetComponent<Data>();
        canvasGroup = GetComponent<CanvasGroup>();

        scenetime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(result == false)
        {
            if (!(flagManager.velXFixed && !flagManager.pressParm) && !gameClear.playSE[1])
            {
                scenetime += Time.deltaTime;
                canvasGroup.alpha = 1;
            }
            this.GetComponent<Text>().text = "Time : " + scenetime.ToString("f2");
        }
        else if(result == true)
        {
            canvasGroup.alpha = 0;
        }

        if (gameClear.goal)
        {
            check = true;
        }

        if(check)
        {
            gameClear.goal = false;
            if(data != null) data.scenetime[data.checkcount] = scenetime;
            if (data != null) data.checkcount++;
            check = false;
        }

    }
    
}
