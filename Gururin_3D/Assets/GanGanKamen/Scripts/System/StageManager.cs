using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class StageManager : MonoBehaviour
    {
        public SaveData saveData;

        private bool getMedal = false;
        private float elapsedTime = 0;
        private int itemNum = 0;
        private bool timerOn = false;
        private bool stageMode = false;


        public void CheckSaveData()
        {
            if (saveData == null)
            {
                SaveData newSaveData = new SaveData();
                newSaveData.Init();
                saveData = newSaveData;
            }
            else
            {
                saveData.Load();
            }
        }

        public void GoToStage()
        {
            stageMode = true;
            getMedal = false;
            itemNum = 0;
        }

        public void StageGameStart()
        {
            timerOn = true;
        }

        public void GetItem()
        {
            if (stageMode == false) return;
            itemNum += 1;
        }

        public void GetMedal()
        {
            if (stageMode == false || getMedal) return;
            getMedal = true;
        }

        public void StageClear()
        {
            stageMode = false;
            timerOn = false;
            
        }

        private void Update()
        {
            if (timerOn)
            {
                elapsedTime += Time.deltaTime;
            }
        }
    }
}

