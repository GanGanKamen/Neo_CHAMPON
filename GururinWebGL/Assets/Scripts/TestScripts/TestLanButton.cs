using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLanButton : MonoBehaviour
{
    public LanguageSwitch.Language thisLan;
    private Image image;   
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(thisLan == LanguageSwitch.language)
        {
            image.color = Color.yellow;
        }
        else
        {
            image.color = Color.white;
        }
    }


    public void LanSwitchText(int lan)
    {
        switch (lan)
        {
            case 0:
                LanguageSwitch.language = LanguageSwitch.Language.Japanese;
                break;
            case 1:
                LanguageSwitch.language = LanguageSwitch.Language.English;
                break;
            case 2:
                LanguageSwitch.language = LanguageSwitch.Language.ChineseHans;
                break;
            case 3:
                LanguageSwitch.language = LanguageSwitch.Language.ChineseHant;
                break;
        }
    }
}
