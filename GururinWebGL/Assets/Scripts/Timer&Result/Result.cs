using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Data data;
    
    public float[] time;
    public bool first;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("Data").GetComponent<Data>();
        first = true;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= data.checkcount; i++)
        {
            time[i] = data.scenetime[i];
            if (i <= data.checkcount)
            {
                time[data.checkcount] += time[i];
            }
            Debug.Log(time[data.checkcount]);
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

        if(first)
        {
            Debug.Log("Result" +
            "\n1 : " + time[0].ToString("f2") + "秒" +
            "\n2 : " + time[1].ToString("f2") + "秒" +
            "\n3 : " + time[2].ToString("f2") + "秒" +
            "\n4 : " + time[3].ToString("f2") + "秒" +
            "\n5 : " + time[4].ToString("f2") + "秒" +
            "\n6 : " + time[5].ToString("f2") + "秒" +
            "\nTotal : " + time[6].ToString("f2") + "秒");
        }
    }

    
}