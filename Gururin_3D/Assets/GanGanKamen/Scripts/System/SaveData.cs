﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class SaveData
    {
        public int ClearStageNum { get { return clearStageNum; } }
        public int[] Assessment { get { return assessment; } }

        private int allStageNum = 9;
        private int clearStageNum; //No.0 ~ No.8、全9ステージ.最大値9
        private int[] assessment;

        // Start is called before the first frame update
        public void Init()
        {
            clearStageNum = 0;
            assessment = new int[allStageNum];
            for(int i = 0;i< assessment.Length; i++)
            {
                assessment[i] = 0;
            }
        }

        public void Save(int nowStageNum,int thisAssessment)
        {
            if(nowStageNum > allStageNum)
            {
                Debug.LogError("不適切なステージ、セーブできない");
                return;
            }
            if(nowStageNum >= ClearStageNum) ES3.Save<int>("ClearStageNum", nowStageNum+1);
            if(assessment[nowStageNum] < thisAssessment) ES3.Save<int>("Assessment" + nowStageNum.ToString(), thisAssessment);
            Load();
        }

        public void Load()
        {
            clearStageNum = ES3.Load<int>("ClearStageNum");
            for(int i = 0; i < clearStageNum + 1; i++)
            {
                assessment[i] = ES3.Load<int>("Assessment" + i.ToString());
            }
        }
    }
}


