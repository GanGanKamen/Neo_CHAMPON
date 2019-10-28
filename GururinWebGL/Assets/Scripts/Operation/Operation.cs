using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Operation : MonoBehaviour
{
    public LanguageText[] languageTexts;

    public int num = 0;
    private int preNum;
    public Text windowText;
    public Animator windowAnim;

    public GameObject Doctor;
    public GameObject TextUI;
    public GameObject WhiteBack;
    // Start is called before the first frame update
    void Start()
    {
        preNum = num;
    }

    // Update is called once per frame
    void Update()
    {
        windowText.text = languageTexts[num].TextOutPut();
    }
}
