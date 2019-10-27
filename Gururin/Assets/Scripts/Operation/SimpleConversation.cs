using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleConversation : MonoBehaviour
{
    public LanguageText[] sentences; // 文章を格納する
    public Text Text;   // uiTextへの参照
    private IEnumerator nowNobel;
    public Image hakaseFace;
    public int currentSentenceNum = 0; //現在表示している文章番号
    public int preSentenceNum = 0;

    public Font font0, font1;
    // Start is called before the first frame update
    void Start()
    {
        nowNobel = NovelText();
    }

    // Update is called once per frame
    void Update()
    {
        TextSwitch();
    }

    private void TextSwitch()
    {

        switch (LanguageSwitch.language)
        {
            case LanguageSwitch.Language.Japanese:
                Text.font = font0;
                break;
            case LanguageSwitch.Language.English:
                Text.font = font0;
                break;
            default:
                Text.font = font1;
                break;
        }

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
                SoundManager.PlayS(gameObject,"Operation", "SE_hakaseTalk");
            }

            wordCound++;
            yield return new WaitForSeconds(NeoConfig.textWaitTime);
        }
        nowNobel = null; nowNobel = NovelText();
        yield break;
    }

    public void OnClick()
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
        }
    }
}
