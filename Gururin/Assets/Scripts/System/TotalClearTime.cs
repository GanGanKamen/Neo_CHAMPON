using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalClearTime : MonoBehaviour
{
    /*
    public static float totalTime;
    //totalTimeを格納するstaticなList
    public static List<string> totalTimeList = new List<string>();
    public static List<float> totalTimeFloatList = new List<float>(totalTimeList.Count);
    //もしListに入っているtotalTimeより新しく格納されたtotalTimeのほうが速かったらハイスコア更新(仮)
    public GameObject text;
    private Text total = null; 

    // Start is called before the first frame update
    void Start()
    {
        total = text.GetComponent<Text>();

        total.text = totalTime.ToString("f2");
        totalTimeList.Add(total.text);

        for (int i = 0; i < totalTimeFloatList.Count; i++)
        {
            Debug.Log("A");
            totalTimeFloatList[i] = float.Parse(totalTimeList[i]);

            if(i > totalTimeFloatList.Count - 1)
            {
                total.text = totalTime.ToString("f2");
            }
            else
            {
                total.text = totalTime.ToString("f2");
            }
        }
        //Debug.Log(totalTimeList.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }
    */
    public static float totalTime;

    public Text highScoreText; //ハイスコアを表示するText
    private float highScore; //ハイスコア用変数
    private string key = "HIGH SCORE"; //ハイスコアの保存先キー

    private void Start()
    {
        highScore = PlayerPrefs.GetFloat(key, 0.0f); //保存しておいたハイスコアをキーで呼び出し取得し保存されていなければ0になる

        highScoreText.text = "HighScore: " + highScore.ToString(); //ハイスコアを表示
    }

    private void Update()
    {
        if (totalTime > highScore)
        {

            highScore = totalTime;
            //ハイスコア更新

            PlayerPrefs.SetFloat(key, highScore);
            //ハイスコアを保存

            PlayerPrefs.Save();

            highScoreText.text = "HighScore: " + highScore.ToString();
            //ハイスコアを表示
        }
    }
}
