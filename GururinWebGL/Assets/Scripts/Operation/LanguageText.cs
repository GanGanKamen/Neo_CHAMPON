using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageText : MonoBehaviour
{
    public string[] messages;
    public Sprite hakaseFace;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public string TextOutPut()
    {
        switch (LanguageSwitch.language)
        {
            case LanguageSwitch.Language.Japanese:
                return messages[0];
            case LanguageSwitch.Language.English:
                return messages[1];
            case LanguageSwitch.Language.ChineseHans:
                return messages[2];
            case LanguageSwitch.Language.ChineseHant:
                return messages[3];
            default:
                return "null";

        }
    }
}
