using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleLogo : MonoBehaviour
{
    public LanguageSwitch.Language nowlanguage;
    private Image logoImage;
    public Sprite[] sourceImages;
    // Start is called before the first frame update
    void Start()
    {
        logoImage = GetComponent<Image>();
        switch (LanguageSwitch.language)
        {
            case LanguageSwitch.Language.Japanese:
                logoImage.sprite = sourceImages[0];
                logoImage.rectTransform.sizeDelta = new Vector2(1536, 960);
                break;
            case LanguageSwitch.Language.English:
                logoImage.sprite = sourceImages[1];
                logoImage.rectTransform.sizeDelta = new Vector2(1280, 800);
                break;
            case LanguageSwitch.Language.ChineseHans:
                logoImage.sprite = sourceImages[2];
                logoImage.rectTransform.sizeDelta = new Vector2(1536, 960);
                break;
            case LanguageSwitch.Language.ChineseHant:
                logoImage.sprite = sourceImages[3];
                logoImage.rectTransform.sizeDelta = new Vector2(1536, 960);
                break;
        }
        nowlanguage = LanguageSwitch.language;
    }

    // Update is called once per frame
    void Update()
    {
        if (nowlanguage != LanguageSwitch.language)
        {
            switch (LanguageSwitch.language)
            {
                case LanguageSwitch.Language.Japanese:
                    logoImage.sprite = sourceImages[0];
                    logoImage.rectTransform.sizeDelta = new Vector2(1536, 960);
                    break;
                case LanguageSwitch.Language.English:
                    logoImage.sprite = sourceImages[1];
                    logoImage.rectTransform.sizeDelta = new Vector2(1280, 800);
                    break;
                case LanguageSwitch.Language.ChineseHans:
                    logoImage.sprite = sourceImages[2];
                    logoImage.rectTransform.sizeDelta = new Vector2(1536, 960);
                    break;
                case LanguageSwitch.Language.ChineseHant:
                    logoImage.sprite = sourceImages[3];
                    logoImage.rectTransform.sizeDelta = new Vector2(1536, 960);
                    break;
            }
            nowlanguage = LanguageSwitch.language;
        }
    }
}
