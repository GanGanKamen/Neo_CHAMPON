using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    public Data data;
    public Timer timer;

    //public CanvasGroup canvasGroup;

    public float[] time;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Data") != null) data = GameObject.Find("Data").GetComponent<Data>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        //canvasGroup = GetComponent<CanvasGroup>();
        timer.result = true;
        for (int i = 0; i < data.checkcount; i++)
        {
            time[i] = data.scenetime[i];
            time[data.checkcount] += data.scenetime[i];
        }

        this.GetComponent<Text>().text =
            "Result" +
            "\n1 : " + time[0].ToString("f2") + "秒" +
            "\n2 : " + time[1].ToString("f2") + "秒" +
            "\n3 : " + time[2].ToString("f2") + "秒" +
            "\n4 : " + time[3].ToString("f2") + "秒" +
            "\n5 : " + time[4].ToString("f2") + "秒" +
            "\n6 : " + time[5].ToString("f2") + "秒" +
            "\nTotal : " + time[6].ToString("f2") + "秒";
    }

    // Update is called once per frame
    void Update()
    {
        /*this.GetComponent<Text>().text =
            "Result" +
            "\n1 : " + time[0].ToString("f2") + "秒" +
            "\n2 : " + time[1].ToString("f2") + "秒" +
            "\n3 : " + time[2].ToString("f2") + "秒" +
            "\n4 : " + time[3].ToString("f2") + "秒" +
            "\n5 : " + time[4].ToString("f2") + "秒" +
            "\n6 : " + time[5].ToString("f2") + "秒" +
            "\nTotal : " + time[6].ToString("f2") + "秒";
        */
    }
}
