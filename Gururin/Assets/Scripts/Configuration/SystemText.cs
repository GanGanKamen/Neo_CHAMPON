using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemText : MonoBehaviour
{
    private LanguageText languageText;
    public LanguageSwitch.Language nowlanguage;
    private Text text;
    public Font font0, font1;
    // Start is called before the first frame update
    void Start()
    {
        languageText = GetComponent<LanguageText>();
        text = GetComponent<Text>();
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
        text.text = languageText.TextOutPut().Replace("　", "\n");
        nowlanguage = LanguageSwitch.language;
    }

    // Update is called once per frame
    void Update()
    {
        if(nowlanguage != LanguageSwitch.language)
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
            text.text = languageText.TextOutPut().Replace("　", "\n");
            nowlanguage = LanguageSwitch.language;
        }
    }
}
