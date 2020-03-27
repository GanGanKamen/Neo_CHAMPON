using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class StageManager : MonoBehaviour
    {
        public bool Medal { get { return getMedal; } }
        public float ElapsedTime { get { return elapsedTime; } }
        public int ItemNum { get { return itemNum; } }
        public float ClearTimeGoal { get { return clearTimeGoal; } }
        public int NowStageNumber { get { return nowStageNum; } }
        public SaveData saveData;

        [SerializeField] private GameObject result;

        private bool getMedal = false;
        private float elapsedTime = 0;
        private int itemNum = 0;
        private bool timerOn = false;
        private bool stageMode = false;
        private float clearTimeGoal;
        private int allItemNum;
        private int nowStageNum;

        public void CheckSaveData()
        {
            if (saveData == null)
            {
                SaveData newSaveData = new SaveData();
                newSaveData.Init();
                saveData = newSaveData;
                saveData.Load();
            }
            else
            {
                saveData.Load();
            }
        }

        public void GoToStage(float _clearTimeGoal,int stageNumber)
        {
            stageMode = true;
            getMedal = false;
            itemNum = 0;
            clearTimeGoal = _clearTimeGoal;
            nowStageNum = stageNumber;
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
            GameObject resultObj = Instantiate(result);
            var resultManager = resultObj.GetComponent<ResultManagaer>();
            resultManager.Init(this);
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

