using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConversationController : MonoBehaviour
{
    public LanguageText[] sentences; // 文章を格納する
    public Text Text;   // uiTextへの参照

    public GameObject Doctor;
    public Image hakaseFace;
    public GameObject TextUI;
    public GameObject WhiteBack;
    public Animator windowAnim;

    public bool IsConversation, IsDescription;
    public bool sendtext = false;
    public bool feedin = false, feedout = false;
    public int currentSentenceNum = 0; //現在表示している文章番号
    public int preSentenceNum = 0;
    private int displaycount = 0;

    public Vector2 mousePosition;

    public bool[] textFeed;

    public Configuration config;
    public Gamecontroller gameController;

    private IEnumerator nowNobel;
    void Start()
    {
        config = GameObject.Find("ConfigCanvas").GetComponent<Configuration>();
        gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();

        IsConversation = false;
        IsDescription = false;
        sendtext = false;
        feedin = false;
        feedout = false;
        Doctor.SetActive(false);
        TextUI.SetActive(false);
        WhiteBack.SetActive(false);

        nowNobel = NovelText();
        //StartCoroutine(nowNobel);
    }

    void Update()
    {

        //Text.text = sentences[currentSentenceNum].TextOutPut();
        TextSwitch();
        if (IsConversation)
        {
            gameController.isCon = true;
            Doctor.SetActive(true);
            TextUI.SetActive(true);
            windowAnim.SetBool("Open", true);
            windowAnim.SetBool("Close", false);
            WhiteBack.SetActive(true);
            if (Input.GetMouseButtonDown(0) && config.configbutton == false)
            {


                mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                if (!(mousePosition.x > 0.94f && mousePosition.y > 0.91f ||
                    mousePosition.x < 0.19f && mousePosition.y < 0.09f))
                {
                    SoundManager.PlayS(gameObject, "SE_tap");
                    OnClick();
                }
            }
        }
        else
        {
            if (WhiteBack.activeSelf == true) Invoke("PlayStartCtrl", 0.5f);
            WhiteBack.SetActive(false);
        }

        if (IsDescription)
        {
            gameController.isDes = true;
            Doctor.SetActive(true);
            TextUI.SetActive(true);
            windowAnim.SetBool("Open", true);
            windowAnim.SetBool("Close", false);

            if (sendtext)
            {
                if (!feedin && !feedout)
                {
                    if (sentences.Length - 1 >= currentSentenceNum)
                    {
                        feedin = true;
                        sendtext = false;
                    }
                }

            }

        }
        else
        {
            gameController.isDes = false;
        }

        if (!IsConversation && !IsDescription)
        {
            displaycount++;
            if (displaycount >= 30 && !feedin && !feedout)
            {
                windowAnim.SetBool("Open", false);
                windowAnim.SetBool("Close", true);
                StopAll();
                displaycount = 0;
            }
        }
        if (currentSentenceNum > 0) textFeed[currentSentenceNum - 1] = false;
        
        /*if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "Tutorial-1"
            &&( currentSentenceNum == 2|| currentSentenceNum == 3))
        {
            OnClick();
        }*/
    }

    private void PlayStartCtrl()
    {
        gameController.isCon = false;
    }

    private void TextSwitch()
    {
        if (preSentenceNum != currentSentenceNum)
        {
            preSentenceNum = currentSentenceNum;
            //Text.text = sentences[currentSentenceNum].TextOutPut();
            StartCoroutine(nowNobel);
        }
    }

    private IEnumerator NovelText()
    {
        hakaseFace.sprite = sentences[currentSentenceNum].hakaseFace;
        int wordCound = 0;
        Text.text = "";
        while (wordCound < sentences[currentSentenceNum].TextOutPut().Length)
        {
            if (sentences[currentSentenceNum].TextOutPut()[wordCound] == '　')
            {
                if (LanguageSwitch.language == LanguageSwitch.Language.Japanese || LanguageSwitch.language == LanguageSwitch.Language.English)
                {
                    Text.text += "\n\n";
                }
                else
                {
                    Text.text += "\n";
                }
            }
            else
            {
                Text.text += sentences[currentSentenceNum].TextOutPut()[wordCound];
                SoundManager.PlayS(gameObject, "SE_hakaseTalk");
            }

            wordCound++;
            yield return new WaitForSeconds(NeoConfig.textWaitTime);
        }
        nowNobel = null; nowNobel = NovelText();
        yield break;
    }

    public void OnClick()
    {
        if (!feedin && !feedout)
        {
            if (sentences.Length - 1 >= currentSentenceNum)
            {
                string finalText = "";
                if (LanguageSwitch.language == LanguageSwitch.Language.Japanese || LanguageSwitch.language == LanguageSwitch.Language.English)
                {
                    finalText = sentences[currentSentenceNum].TextOutPut().Replace("　", "\n\n");
                }
                else
                {
                    finalText = sentences[currentSentenceNum].TextOutPut().Replace("　", "\n");
                }
                if (Text.text != finalText)
                {
                    StopCoroutine(nowNobel);
                    nowNobel = null; nowNobel = NovelText();
                    Text.text = finalText;
                }
                else feedout = true;
            }
        }
    }

    public void StopAll()
    {
        Debug.Log("stop");
        StopCoroutine(nowNobel);
        nowNobel = null; nowNobel = NovelText();
        Text.text = "";
    }
}
