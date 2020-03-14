using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class StageInfo : MonoBehaviour
    {
        [Header("ステージ番号(0～8)")]public int stageNumber;
        [Header("理想的なクリア時間")]public float clearTimeGoal;
        [Header("合計アイテム数")] public int allItemNum;
        public string sceneName; 
        [SerializeField] GameObject lockObj;
        // Start is called before the first frame update
        void Start()
        {
            var stageManager = GameObject.FindGameObjectWithTag("System").GetComponent<StageManager>();
            if (stageManager.saveData.ClearStageNum >= stageNumber)
            {
                lockObj.SetActive(false);
                GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

