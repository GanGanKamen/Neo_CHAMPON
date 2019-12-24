using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalWatch : MonoBehaviour
{
    public GameObject[] dititalNumbers;
    public Transform[] numberPos;
    private GameObject[] nowNumber;
    [SerializeField] private WatchGimick watch;
    [SerializeField]private int[] preTimeNums;

    public int[] timeNumList;
    // Start is called before the first frame update
    void Start()
    {
        timeNumList = new int[4];
        DigitalDisplay();
        
        preTimeNums = new int[4];
        nowNumber = new GameObject[4];
        var newNumObj = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            preTimeNums[i] = timeNumList[i];
            newNumObj[i] = Instantiate(dititalNumbers[timeNumList[i]], numberPos[i].position, numberPos[i].rotation);
            nowNumber[i] = newNumObj[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        DigitalDisplay();
        NumberChange();
    }

    private void NumberChange()
    {
        for(int i = 0; i < 4; i++)
        {
            if(preTimeNums[i] != timeNumList[i])
            {
                Destroy(nowNumber[i]);
                nowNumber[i] = null;
                GameObject newNumObj = Instantiate(dititalNumbers[timeNumList[i]], numberPos[i].position, numberPos[i].rotation);
                nowNumber[i] = newNumObj;
                preTimeNums[i] = timeNumList[i];
            }
        }
    }

    private void DigitalDisplay()
    {
        var hoursArray = new int[2];
        if (watch.hours >= 10)
        {
            hoursArray[0] = 1;
            hoursArray[1] = watch.hours - 10;
        }
        else
        {
            hoursArray[0] = 0;
            hoursArray[1] = watch.hours;
        }

        var minminutesArray = new int[2];
        minminutesArray[0] = watch.minminutes / 10;
        minminutesArray[1] = watch.minminutes % 10;

        timeNumList[0] = hoursArray[0];
        timeNumList[1] = hoursArray[1];
        timeNumList[2] = minminutesArray[0];
        timeNumList[3] = minminutesArray[1];
    }
}
