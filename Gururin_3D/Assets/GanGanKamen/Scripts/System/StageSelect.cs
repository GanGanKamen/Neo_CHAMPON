using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GanGanKamen
{
    public class StageSelect : MonoBehaviour
    {
        static public int ClearStageNum = 0;
        static public int PreClearStageNum = 0;
        [SerializeField] private GameController gameController;
        [SerializeField] private RectTransform images;
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool isMove = false;
        [SerializeField] private int nowArea = 1;
        [SerializeField] private int preArea = 1;
        [SerializeField] private Button[] stageButtons;
        [SerializeField] private ParticleSystem[] lockOpenEffect;
        private StageManager stageManager;
        private bool canctrl = false;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(StatusProcess());
        }

        private IEnumerator StatusProcess()
        {
            for (int i = 0; i < stageButtons.Length; i++)
            {
                int j = i;
                stageButtons[j].onClick.AddListener(() => StartStage(stageButtons[j].gameObject));
            }
            stageManager = GameObject.FindGameObjectWithTag("System").GetComponent<StageManager>();
            ClearStageNum = stageManager.saveData.ClearStageNum;
            Debug.Log(ClearStageNum);
            if(ClearStageNum >= 0 && ClearStageNum < 3)
            {
                nowArea = 1;
                preArea = nowArea;
                images.localPosition = Vector3.zero;
            }
            else if(ClearStageNum >= 3 && ClearStageNum < 6)
            {
                nowArea = 2;
                preArea = nowArea;
                images.localPosition = new Vector3(0, -1080, 0);
            }
            else
            {
                nowArea = 3;
                preArea = nowArea;
                images.localPosition = new Vector3(0, -2160, 0);
            }
            if (PreClearStageNum < ClearStageNum)
            {
                for (int i = 0; i < ClearStageNum; i++)
                {
                    stageButtons[i].GetComponent<StageInfo>().UnLock();
                }
                int target = (ClearStageNum + 1) % 3;
                Debug.Log("target " + target);
                switch (target)
                {
                    case 0:
                        lockOpenEffect[2].Play();
                        break;
                    case 1:
                        lockOpenEffect[0].Play();
                        break;
                    case 2:
                        lockOpenEffect[1].Play();
                        break;
                }                
                yield return new WaitForSeconds(1f);
                stageButtons[ClearStageNum].GetComponent<StageInfo>().UnLock();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                for (int i = 0; i < ClearStageNum + 1; i++)
                {
                    Debug.Log("unlock" + i);
                    stageButtons[i].GetComponent<StageInfo>().UnLock();
                }
            }
            PreClearStageNum = ClearStageNum;
            canctrl = true;
            yield break;
        }

        public void MoveButton(int nextArea)
        {
            if (nextArea > 3 || nextArea < 1 || isMove || canctrl == false) return;
            nowArea = nextArea;
        }

        public void StartStage(GameObject targetButton)
        {
            if (isMove || canctrl == false) return;
            var stageInfo = targetButton.GetComponent<StageInfo>();
            if (GameObject.FindGameObjectWithTag("System") != null)
            {
                var stageMng = GameObject.FindGameObjectWithTag("System").GetComponent<GanGanKamen.StageManager>();
                stageMng.GoToStage(stageInfo.clearTimeGoal, stageInfo.allItemNum, stageInfo.stageNumber);
            }
            Fader.FadeIn(3f, stageInfo.sceneName);
        }

        private void FlickAction()
        {
            if (isMove || canctrl == false) return;
            if (gameController.InputFlick)
            {
                switch (gameController.InputFlickDirection)
                {
                    case FlickDirection.Up:
                        if (nowArea < 3)
                        {
                            nowArea += 1;
                        }
                        break;
                    case FlickDirection.Down:
                        if (nowArea > 1)
                        {
                            nowArea -= 1;
                        }
                        break;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            FlickAction();
            if(canctrl) SwitchArea();
        }

        private void SwitchArea()
        {
            if (nowArea != preArea)
            {
                isMove = true;
                switch (nowArea)
                {
                    case 1:
                        if (images.localPosition.y < 0)
                        {
                            images.localPosition += new Vector3(0, moveSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            images.localPosition = Vector3.zero;
                            preArea = nowArea;
                            isMove = false;
                        }
                        break;
                    case 2:
                        if (preArea > nowArea)
                        {
                            if (images.localPosition.y < -1080)
                            {
                                images.localPosition += new Vector3(0, moveSpeed * Time.deltaTime, 0);
                            }
                            else
                            {
                                images.localPosition = new Vector3(0, -1080, 0);
                                preArea = nowArea;
                                isMove = false;
                            }
                        }
                        else if (preArea < nowArea)
                        {
                            if (images.localPosition.y > -1080)
                            {
                                images.localPosition -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
                            }
                            else
                            {
                                images.localPosition = new Vector3(0, -1080, 0);
                                preArea = nowArea;
                                isMove = false;
                            }
                        }
                        break;
                    case 3:
                        if (images.localPosition.y > -2160)
                        {
                            images.localPosition -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            images.localPosition = new Vector3(0, -2160, 0);
                            preArea = nowArea;
                            isMove = false;
                        }
                        break;
                }
            }
        }
    }
}

