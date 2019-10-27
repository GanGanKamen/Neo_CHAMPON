using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSwitch : Configuration
{
    static public int LanguageNum = 4;

    public enum Language
    {
        Japanese,
        English,
        ChineseHans,
        ChineseHant

    }
    static public Language language = Language.Japanese;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
